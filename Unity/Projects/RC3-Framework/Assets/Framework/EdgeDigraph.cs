using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Notes 
 */
 
namespace RC3
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


        private List<List<int>> _vertsOut;
        private List<List<int>> _vertsIn;
        private List<int> _edges;


        /// <summary>
        /// 
        /// </summary>
        public EdgeDigraph(int vertexCapacity =  _defaultCapacity, int edgeCapacity = _defaultCapacity)
        {
            _vertsOut = new List<List<int>>(vertexCapacity);
            _vertsIn = new List<List<int>>(vertexCapacity);
            _edges = new List<int>(edgeCapacity << 1);
        }


        /// <summary>
        /// Returns the number of vertices in the graph.
        /// </summary>
        public int VertexCount
        {
            get { return _vertsOut.Count; }
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
            return _vertsOut[vertex].Count;
        }


        /// <summary>
        /// Returns the number of incoming edges at the given vertex.
        /// </summary>
        public int GetDegreeIn(int vertex)
        {
            return _vertsIn[vertex].Count;
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
            _vertsOut.Add(new List<int>(capacityOut));
            _vertsIn.Add(new List<int>(capacityIn));
        }


        /// <summary>
        /// Adds an edge from the first given vertex to the second.
        /// </summary>
        public void AddEdge(int v0, int v1)
        {
            var e = _edges.Count >> 1;
            _vertsOut[v0].Add(e);
            _vertsIn[v1].Add(e);

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
        public int GetOutgoingEdge(int vertex, int index)
        {
            return _vertsOut[vertex][index];
        }


        /// <summary>
        /// Returns all edges that start at the given vertex.
        /// </summary>
        public IEnumerable<int> GetOutgoingEdges(int vertex)
        {
            return _vertsOut[vertex];
        }


        /// <summary>
        /// 
        /// </summary>
        public int GetIncomingEdge(int vertex, int index)
        {
            return _vertsIn[vertex][index];
        }


        /// <summary>
        /// Returns all edges that end at the given vertex.
        /// </summary>
        public IEnumerable<int> GetIncomingEdges(int vertex)
        {
            return _vertsIn[vertex];
        }


        /// <summary>
        /// 
        /// </summary>
        public int GetVertexNeighborOut(int vertex, int index)
        {
            return GetEndVertex(_vertsOut[vertex][index]);
        }


        /// <summary>
        /// Returns all vertices that the given vertex connects to.
        /// </summary>
        public IEnumerable<int> GetVertexNeighborsOut(int vertex)
        {
            foreach (var e in _vertsOut[vertex])
                yield return GetEndVertex(e);
        }


        /// <summary>
        /// 
        /// </summary>
        public int GetVertexNeighborIn(int vertex, int index)
        {
            return GetStartVertex(_vertsIn[vertex][index]);
        }


        /// <summary>
        /// Returns all vertices that connect to the given vertex.
        /// </summary>
        public IEnumerable<int> GetVertexNeighborsIn(int vertex)
        {
            foreach (var e in _vertsIn[vertex])
                yield return GetStartVertex(e);
        }
    }
}
