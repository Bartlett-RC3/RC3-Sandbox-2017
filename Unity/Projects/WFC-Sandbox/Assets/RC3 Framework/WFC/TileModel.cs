
/*
 * Notes
 * 
 * TODO 
 * test stack implementation instead of queue
 * 
 * impl ref
 * https://adamsmith.as/papers/wfc_is_constraint_solving_in_the_wild.pdf
 */

using System;
using System.Collections.Generic;
using RC3.Graphs;

namespace RC3.WFC
{
    /// <summary>
    /// 
    /// </summary>
    public class TileModel
    {
        #region Nested types

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        delegate int GetNeighbor(int position, int direction);

        #endregion


        #region Static

        /// <summary>
        /// 
        /// </summary>
        public static TileModel CreateFromGraph(TileMap map, IGraph graph, int seed = 0)
        {
            for(int i = 0; i < graph.VertexCount; i++)
            {
                if (graph.GetDegree(i) != map.TileDegree)
                    throw new ArgumentException($"Vertex {i} is not compatible with the given tile map.");
            }

            return new TileModel(map, graph.GetVertexNeighbor, graph.VertexCount, seed);
        }


        /// <summary>
        /// 
        /// </summary>
        public static TileModel CreateFromGraph(TileMap map, IDigraph graph, int seed = 0)
        {
            for (int i = 0; i < graph.VertexCount; i++)
            {
                if (graph.GetDegreeOut(i) != map.TileDegree)
                    throw new ArgumentException($"Vertex {i} is not compatible with the given tile map.");
            }
            
            return new TileModel(map, graph.GetVertexNeighborOut, graph.VertexCount, seed);
        }

        #endregion


        private TileMap _map;
        private GetNeighbor _getNeighbor;
     
        private bool[][] _domains; // for each position, boolean values indicating the validity of each tile
        private int[] _sizes; // the remaining size of each domain
        private int[] _assigned; // for each position, the assigned tile or -1 if not yet assigned

        private int[] _positions;
        private int _remaining; // the number of unassigned positions left in the model

        private Queue<int> _queue; // queue used for propagation
        private bool[] _queued; // for each position, true if in the queue

        private int[] _buffer; // pre-allocated buffer used during tile selection
        private Random _random;

        private AssignedCallback _assignedCallback;
        private ReducedCallback _reducedCallback;


        /// <summary>
        /// 
        /// </summary>
        private TileModel(TileMap map, GetNeighbor getNeighbor, int count, int seed = 0)
        {
            _map = map;
            _getNeighbor = getNeighbor;
            Initialize(count, seed);
            Reset();
        }


        /// <summary>
        /// 
        /// </summary>
        private void Initialize(int count, int seed)
        {
            int tileCount = _map.TileCount;

            _domains = new bool[count][];
            for (int i = 0; i < _domains.Length; i++)
                _domains[i] = new bool[tileCount];

            _sizes = new int[count];
            _assigned = new int[count];

            _positions = new int[count];
            _queue = new Queue<int>(count);
            _queued = new bool[count];

            _buffer = new int[tileCount];
            _random = new Random(seed);
        }


        /// <summary>
        /// Returns the number of positions in this model.
        /// </summary>
        public int Count
        {
            get { return _domains.Length; }
        }


        /// <summary>
        /// Returns the last observed position
        /// </summary>
        public int LastObserved
        {
            get { return _positions[_remaining]; }
        }


        /// <summary>
        /// Sets a function that is called when a new tile is assigned.
        /// </summary>
        /// <param name="callback"></param>
        public AssignedCallback AssignedCallback
        {
            set { _assignedCallback = value; }
        }


        /// <summary>
        /// Sets a function that is called when the domain of a tile is reduced.
        /// </summary>
        /// <param name="callback"></param>
        public ReducedCallback ReducedCallback
        {
            set { _reducedCallback = value; }
        }


        /// <summary>
        /// Returns the tile assigned to the given position or -1 if no tile has been assigned yet.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public int GetAssignedTile(int position)
        {
            return _assigned[position];
        }


        /// <summary>
        /// Returns the current size of the domain at the given position.
        /// Initially, this is equal to the number of tiles in the model.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public int GetDomainSize(int position)
        {
            return _sizes[position];
        }

        
        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            for (int i = 0; i < _domains.Length; i++)
            {
                _domains[i].Set(true);
                _positions[i] = i;
            }

            _sizes.Set(_map.TileCount);
            _assigned.Set(-1);

            _positions.Shuffle(_random);
            _remaining = _positions.Length;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        private void ResetLastObserved(int count)
        {
            // TODO
            // resets the the n most recently observed positions
            // will allow for partial backtracking
        }


        /// <summary>
        /// Returns the status of the collapse.
        /// If complete, then no need to propagate.
        /// </summary>
        public CollapseStatus Observe()
        {
            int p, n;
            GetNextMin(out p, out n);

            if (n == 0)
               return CollapseStatus.Contradiction;

            if (n == 1)
                AssignLast(p);
            else
                AssignRandom(p);

            return _remaining == 0 ?
                CollapseStatus.Complete : CollapseStatus.Incomplete;
        }

   
        /// <summary>
        /// Returns the the most constrained position in the model i.e. the one with the fewest remaining values
        /// </summary>
        private void GetNextMin(out int position, out int size)
        {
            int index = 0;
            position = _positions[index];
            size = _sizes[position];

            for (int i = 1; i < _remaining; i++)
            {
                var p = _positions[i];
                var n = _sizes[p];

                if (n < size)
                {
                    index = i;
                    position = p;
                    size = n;
                }
            }

            _positions.Swap(index, --_remaining);
        }


        /// <summary>
        /// Assigns the last remaining tile at the given position
        /// </summary>
        private void AssignLast(int position)
        {
            var d = _domains[position];

            for(int i = 0; i < d.Length; i++)
            {
                if (d[i])
                {
                    _assigned[position] = i;
                    _assignedCallback?.Invoke(position, i);
                    return;
                }
            }
        }


        /// <summary>
        /// Assigns a random remaining tile at the given position
        /// </summary>
        /// <param name="position"></param>
        private void AssignRandom(int position)
        {
            var d = _domains[position];
            int n = 0;

            for (int i = 0; i < d.Length; i++)
            {
                if (d[i])
                    _buffer[n++] = i;
            }

            Assign(position, _buffer[_random.Next(n)]);
        }


        /// <summary>
        /// Assigns the given tile at the given position.
        /// </summary>
        public void Assign(int position, int tile)
        {
            var d = _domains[position];
            d.Set(false);
            d[tile] = true;
            _sizes[position] = 1;

            _assigned[position] = tile;
            _assignedCallback?.Invoke(position, tile);
        }


        /// <summary>
        /// 
        /// </summary>
        public void Propagate()
        {
            TryEnqueue(LastObserved);
            var degree = _map.TileDegree;
            
            while (_queue.Count > 0)
            {
                var p0 = Dequeue();
                var d0 = _domains[p0];

                // try reducing the domain of each neighbor
                for(int i = 0; i < degree; i++)
                {
                    var p1 = _getNeighbor(p0, i);
                    if (p1 == p0) continue; // skip if boundary (neighbor is self)

                    int d = _map.Reduce(i, d0, _domains[p1]);

                    // if domain was reduced, then continue search from p1
                    if (d > 0)
                    {
                        var n = _sizes[p1] -= d;
                        _reducedCallback?.Invoke(p1, n);
                        TryEnqueue(p1);
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void TryEnqueue(int position)
        {
            if (_queued[position]) return;
            _queue.Enqueue(position);
            _queued[position] = true;
        }


        /// <summary>
        /// 
        /// </summary>
        private int Dequeue()
        {
            var p = _queue.Dequeue();
            _queued[p] = false;
            return p;
        }
    }
}
