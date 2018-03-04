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


        private List<List<int>> _vertices;
        private List<int> _edges;


        /// <summary>
        /// 
        /// </summary>
        public EdgeDigraph(int vertexCapacity =  _defaultCapacity, int edgeCapacity = _defaultCapacity)
        {
            _vertices = new List<List<int>>(vertexCapacity);
            _edges = new List<int>(edgeCapacity << 1);
        }


        /// <summary>
        /// Returns the number of vertices in the graph.
        /// </summary>
        public int VertexCount
        {
            get { return _vertices.Count; }
        }


        /// <summary>
        /// Returns the number of edges in the graph.
        /// </summary>
        public int EdgeCount
        {
            get { return _edges.Count >> 1; }
        }


        /// <summary>
        /// Returns the degree of the given vertex.
        /// </summary>
        public int GetDegree(int vertex)
        {
            return _vertices[vertex].Count;
        }


        /// <summary>
        /// Adds a new vertex to the graph.
        /// </summary>
        public void AddVertex(int capacity = _defaultCapacity)
        {
            _vertices.Add(new List<int>(capacity));
        }


        /// <summary>
        /// Adds an edge from the first given vertex to the second.
        /// </summary>
        public void AddEdge(int v0, int v1)
        {
            _vertices[v0].Add(_edges.Count >> 1);
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
        public int GetIncidentEdge(int vertex, int index)
        {
            return _vertices[vertex][index];
        }


        /// <summary>
        /// Returns all edges incident to the given vertex.
        /// </summary>
        public IEnumerable<int> GetIncidentEdges(int vertex)
        {
            return _vertices[vertex];
        }


        /// <summary>
        /// 
        /// </summary>
        public int GetConnectedVertex(int vertex, int index)
        {
            return GetEndVertex(_vertices[vertex][index]);
        }


        /// <summary>
        /// Returns all vertices connected to the given vertex.
        /// </summary>
        public IEnumerable<int> GetConnectedVertices(int vertex)
        {
            foreach (var e in _vertices[vertex])
                yield return GetEndVertex(e);
        }
    }
}
