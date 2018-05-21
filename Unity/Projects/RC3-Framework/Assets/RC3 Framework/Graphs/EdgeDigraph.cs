using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Notes 
 */
 
namespace RC3.Graphs
{
    /// <summary>
    /// Simple edge list representation of a directed graph.
    /// </summary>
    public class EdgeDigraph : IEdgeDigraph
    {
        #region Static

        public static readonly EdgeDigraphFactory Factory = new EdgeDigraphFactory();
        private const int _defaultCapacity = 4;

        #endregion


        private List<List<int>> _adjOut;
        private List<List<int>> _adjIn;
        private List<int> _edges;


        /// <summary>
        /// 
        /// </summary>
        public EdgeDigraph(int vertexCapacity =  _defaultCapacity, int edgeCapacity = _defaultCapacity)
        {
            _adjOut = new List<List<int>>(vertexCapacity);
            _adjIn = new List<List<int>>(vertexCapacity);
            _edges = new List<int>(edgeCapacity << 1);
        }


        /// <summary>
        /// Returns the number of vertices in the graph.
        /// </summary>
        public int VertexCount
        {
            get { return _adjOut.Count; }
        }


        /// <summary>
        /// Returns the number of edges in the graph.
        /// </summary>
        public int EdgeCount
        {
            get { return _edges.Count >> 1; }
        }


        /// <summary>
        /// Returns the number of outgoing edges at the given vertex.
        /// </summary>
        public int GetDegreeOut(int vertex)
        {
            return _adjOut[vertex].Count;
        }


        /// <summary>
        /// Returns the number of incoming edges at the given vertex.
        /// </summary>
        public int GetDegreeIn(int vertex)
        {
            return _adjIn[vertex].Count;
        }


        /// <summary>
        /// Adds a new vertex to the graph.
        /// </summary>
        public void AddVertex()
        {
            AddVertex(_defaultCapacity, _defaultCapacity);
        }


        /// <summary>
        /// 
        /// </summary>
        public void AddVertex(int capacityOut, int capacityIn)
        {
            _adjOut.Add(new List<int>(capacityOut));
            _adjIn.Add(new List<int>(capacityIn));
        }


        /// <summary>
        /// Adds an edge from the first given vertex to the second.
        /// </summary>
        public void AddEdge(int v0, int v1)
        {
            var e = _edges.Count >> 1;
            _adjOut[v0].Add(e);
            _adjIn[v1].Add(e);

            _edges.Add(v0);
            _edges.Add(v1);
        }


        /// <summary>
        /// Returns the vertex at the start of the given edge.
        /// </summary>
        public int GetStartVertex(int edge)
        {
            return _edges[edge << 1];
        }


        /// <summary>
        /// Returns the vertex at the end of the given edge.
        /// </summary>
        public int GetEndVertex(int edge)
        {
            return _edges[(edge << 1) + 1];
        }


        /// <summary>
        /// 
        /// </summary>
        public bool HasEdge(int v0, int v1)
        {
            return FindEdge(v0, v1) != -1;
        }


        /// <summary>
        /// 
        /// </summary>
        public int FindEdge(int v0, int v1)
        {
            foreach (var e in _adjOut[v0])
            {
                if (GetEndVertex(e) == v1)
                    return e;
            }

            return -1;
        }


        /// <summary>
        /// 
        /// </summary>
        public int GetOutgoingEdge(int vertex, int index)
        {
            return _adjOut[vertex][index];
        }


        /// <summary>
        /// Returns all edges that start at the given vertex.
        /// </summary>
        public IEnumerable<int> GetOutgoingEdges(int vertex)
        {
            return _adjOut[vertex];
        }


        /// <summary>
        /// 
        /// </summary>
        public int GetIncomingEdge(int vertex, int index)
        {
            return _adjIn[vertex][index];
        }


        /// <summary>
        /// Returns all edges that end at the given vertex.
        /// </summary>
        public IEnumerable<int> GetIncomingEdges(int vertex)
        {
            return _adjIn[vertex];
        }


        /// <summary>
        /// 
        /// </summary>
        public int GetVertexNeighborOut(int vertex, int index)
        {
            return GetEndVertex(_adjOut[vertex][index]);
        }


        /// <summary>
        /// Returns all vertices that the given vertex connects to.
        /// </summary>
        public IEnumerable<int> GetVertexNeighborsOut(int vertex)
        {
            foreach (var e in _adjOut[vertex])
                yield return GetEndVertex(e);
        }


        /// <summary>
        /// 
        /// </summary>
        public int GetVertexNeighborIn(int vertex, int index)
        {
            return GetStartVertex(_adjIn[vertex][index]);
        }


        /// <summary>
        /// Returns all vertices that connect to the given vertex.
        /// </summary>
        public IEnumerable<int> GetVertexNeighborsIn(int vertex)
        {
            foreach (var e in _adjIn[vertex])
                yield return GetStartVertex(e);
        }
    }
}
