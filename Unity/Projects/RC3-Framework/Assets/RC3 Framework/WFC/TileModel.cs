
/*
 * Notes
 * 
 * TODO handle public domain modifications
 * 
 * impl ref
 * https://adamsmith.as/papers/wfc_is_constraint_solving_in_the_wild.pdf
 */

using System;
using System.Linq;
using System.Collections.Generic;

using SpatialSlur.Core;
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
        /// <param name="size"></param>
        public delegate void DomainChangedHandler(int position, ReadOnlySet<int> domain);

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private delegate int GetAdjacent(int position, int direction);

        #endregion


        #region Static

        /// <summary>
        /// 
        /// </summary>
        public static TileModel CreateFromGraph<T>(TileMap<T> tiles, IGraph graph, int seed = 0)
        {
            int degree = tiles.TileDegree;

            for(int i = 0; i < graph.VertexCount; i++)
            {
                if (graph.GetDegree(i) != degree)
                    throw new ArgumentException($"Vertex {i} is not compatible with the given tile set.");
            }

            return new TileModel(tiles.CreateConstraints(), graph.GetVertexNeighbor, graph.VertexCount, tiles.TileCount, seed);
        }


        /// <summary>
        /// 
        /// </summary>
        public static TileModel CreateFromGraph<T>(TileMap<T> tiles, IDigraph graph, int seed = 0)
        {
            int degree = tiles.TileDegree;

            for (int i = 0; i < graph.VertexCount; i++)
            {
                if (graph.GetDegreeOut(i) != degree)
                    throw new ArgumentException($"Vertex {i} is not compatible with the given tile map.");
            }
            
            return new TileModel(tiles.CreateConstraints(), graph.GetVertexNeighborOut, graph.VertexCount, tiles.TileCount, seed);
        }

        #endregion

        
        private Constraint<int>[] _constraints; // constraints for each direction
        private GetAdjacent _getAdjacent;
        private int _tileCount; // number of distinct tiles in the model

        private HashSet<int>[] _domains; // domain of each position
        private HashSet<int> _remaining; // remaining positions to collapse
        private QueueSet<int> _queue;

        private TileSelector _selector;
        private List<int> _buffer;

        
        /// <summary>
        /// 
        /// </summary>
        public event DomainChangedHandler DomainChanged;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="constraints"></param>
        /// <param name="getAdjacent"></param>
        /// <param name="positionCount"></param>
        /// <param name="tileCount"></param>
        /// <param name="seed"></param>
        private TileModel(IEnumerable<Constraint<int>> constraints, GetAdjacent getAdjacent, int positionCount, int tileCount, int seed = 0)
        {
            _constraints = constraints.ToArray();
            _getAdjacent = getAdjacent;
            _tileCount = tileCount;

            _domains = new HashSet<int>[positionCount];
            for (int i = 0; i < _domains.Length; i++)
                _domains[i] = new HashSet<int>(DefaultDomain);

            _remaining = new HashSet<int>(Enumerable.Range(0, positionCount));
            _queue = new QueueSet<int>();

            _selector = new RandomTileSelector(this, seed);
            _buffer = new List<int>(tileCount);
        }

        
        /// <summary>
        /// Returns the number of positions in this model.
        /// </summary>
        public int PositionCount
        {
            get { return _domains.Length; }
        }


        /// <summary>
        /// Returns the number of tiles in this model.
        /// </summary>
        public int TileCount
        {
            get { return _tileCount; }
        }


        /// <summary>
        /// 
        /// </summary>
        public TileSelector Selector
        {
            get { return _selector; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                _selector = value;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<int> DefaultDomain
        {
            get { return Enumerable.Range(0, _tileCount); }
        }


        /// <summary>
        /// Returns the tile assigned to the given position or -1 if a tile hasn't been assigned yet.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public int GetAssigned(int position)
        {
            var d = _domains[position];
            return d.Count == 1 ? d.First() : -1;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public ReadOnlySet<int> GetDomain(int position)
        {
            return _domains[position];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="tiles"></param>
        public void SetDomain(int position, IEnumerable<int> tiles)
        {
            Validate(tiles);
            SetDomainImpl(position, tiles);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="tiles"></param>
        public void SetDomainImpl(int position, IEnumerable<int> tiles)
        {
            var d = _domains[position];

            // if domain has been modified, need to reset and expand first
            if (d.Count != _tileCount)
            {
                d.Clear();
                d.UnionWith(DefaultDomain);
                ExpandFrom(position);
            }

            d.Clear();
            d.UnionWith(tiles);
            _remaining.Add(position);

            OnDomainChanged(position);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="tiles"></param>
        public void ReduceDomain(int position, IEnumerable<int> tiles)
        {
            _domains[position].ExceptWith(tiles);
            OnDomainChanged(position);
        }


        /// <summary>
        /// Resets the domain at the given position
        /// </summary>
        /// <param name="position"></param>
        public void ResetDomain(int position)
        {
            var d = _domains[position];

            // check if domain is already reset
            if (d.Count == _tileCount)
                return;

            d.Clear();
            d.UnionWith(DefaultDomain);
            _remaining.Add(position);

            OnDomainChanged(position);
            ExpandFrom(position);
        }


        /// <summary>
        /// Resets all domains in the tile model.
        /// </summary>
        public void ResetAllDomains()
        {
            _remaining.Clear();

            for (int i = 0; i < _domains.Length; i++)
            {
                var d = _domains[i];

                d.Clear();
                d.UnionWith(DefaultDomain);
                _remaining.Add(i);

                OnDomainChanged(i);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        private void OnDomainChanged(int position)
        {
            DomainChanged?.Invoke(position, _domains[position]);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tile"></param>
        private void Validate(int tile)
        {
            if ((uint)tile >= (uint)_tileCount)
                throw new IndexOutOfRangeException("The given tile is not valid.");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tiles"></param>
        private void Validate(IEnumerable<int> tiles)
        {
            foreach (var tile in tiles)
                Validate(tile);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CollapseStatus Step()
        {
            if (_remaining.Count == 0)
                return CollapseStatus.Complete;
            
            var pmin = _remaining.SelectMin(p => _domains[p].Count);
            var d = _domains[pmin];
       
            // contradiction if no variabes remaining
            if (d.Count == 0)
                return CollapseStatus.Contradiction;

            // select tile & validate
            var tile = _selector.Select(pmin);
            Validate(tile);

            // assign selected tile @ min pos
            d.Clear();
            d.Add(tile);
            _remaining.Remove(pmin);

            OnDomainChanged(pmin);
            CollapseFrom(pmin);

            return CollapseStatus.Incomplete;
        }


        /// <summary>
        /// 
        /// </summary>
        private void CollapseFrom(int position)
        {
            _queue.Enqueue(position);

            while (_queue.Count > 0)
            {
                var p0 = _queue.Dequeue();
                var d0 = _domains[p0];

                // reduce the domain of each neighbor
                for(int i = 0; i < _constraints.Length; i++)
                {
                    var p1 = _getAdjacent(p0, i);
                    if (p1 == p0) continue; // skip if boundary (neighbor is self)

                    // collect inconsistent variables in d1
                    var d1 = _domains[p1];
                    _buffer.AddRange(_constraints[i].GetInconsistent(d1, d0));

                    // reduce d1 if necessary
                    if(_buffer.Count > 0)
                    {
                        d1.ExceptWith(_buffer);
                        OnDomainChanged(p1);

                        _queue.Enqueue(p1);
                        _buffer.Clear();
                    }
                }
            }
        }


        /// <summary>
        /// Called whenever the domain is reset
        /// </summary>
        /// <param name="position"></param>
        private void ExpandFrom(int position)
        {
            // TODO
        }


        /*
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetDebugReport()
        {
            return "";
        }
        */
    }
}
