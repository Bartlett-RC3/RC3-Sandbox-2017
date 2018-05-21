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
    /// Simple edge list representation of an undirected graph.
    /// </summary>
    public class EdgeGraph: IEdgeGraph
    {
        #region Static

        public static readonly EdgeGraphFactory Factory = new EdgeGraphFactory();
        private const int _defaultCapacity = 4;

        #endregion


        private List<List<int>> _adj;
        private List<int> _edges;


        /// <summary>
        /// 
        /// </summary>
        public EdgeGraph(int vertexCapacity = _defaultCapacity, int edgeCapacity = _defaultCapacity)
        {
            _adj = new List<List<int>>(vertexCapacity);
            _edges = new List<int>(edgeCapacity << 1);
        }


        /// <summary>
        /// Returns the number of vertices in the graph.
        /// </summary>
        public int VertexCount
        {
            get { return _adj.Count; }
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
            return _adj[vertex].Count;
        }


        /// <summary>
        /// Adds a new vertex to the graph.
        /// </summary>
        public void AddVertex()
        {
            AddVertex(_defaultCapacity);
        }


        /// <summary>
        /// 
        /// </summary>
        public void AddVertex(int capacity = _defaultCapacity)
        {
            _adj.Add(new List<int>(capacity));
        }


        /// <summary>
        /// Adds an edge between the given vertex.
        /// </summary>
        public void AddEdge(int v0, int v1)
        {
            var e = _edges.Count >> 1;
            _adj[v0].Add(e);

            // don't add again if edge is a loop
            if (v1 != v0)
                _adj[v1].Add(e);

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
        /// Returns true if there is an edge between the given vertices.
        /// </summary>
        public bool HasEdge(int v0, int v1)
        {
            return FindEdge(v0, v1) != -1;
        }


        /// <summary>
        /// Returns the first edge between the given vertices or -1 if no edge exists.
        /// </summary>
        public int FindEdge(int v0, int v1)
        {
            foreach(var e in _adj[v0])
            {
                if (GetOppositeVertex(e, v0) == v1)
                    return e;
            }

            return -1;
        }


        /// <summary>
        /// 
        /// </summary>
        public int GetOppositeVertex(int edge, int vertex)
        {
            edge <<= 1;
            var v0 = _edges[edge];
            var v1 = _edges[edge + 1];
            return vertex == v0 ? v1 : vertex == v1 ? v0 : -1;
        }


        /// <summary>
        /// 
        /// </summary>
        public int GetIncidentEdge(int vertex, int index)
        {
            return _adj[vertex][index];
        }


        /// <summary>
        /// Returns all edges incident to the given vertex.
        /// </summary>
        public IEnumerable<int> GetIncidentEdges(int vertex)
        {
            return _adj[vertex];
        }


        /// <summary>
        /// 
        /// </summary>
        public int GetVertexNeighbor(int vertex, int index)
        {
            return GetOppositeVertex(_adj[vertex][index], vertex);
        }


        /// <summary>
        /// Returns all vertices connected to the given vertex.
        /// </summary>
        public IEnumerable<int> GetVertexNeighbors(int vertex)
        {
            foreach (var e in _adj[vertex])
                yield return GetOppositeVertex(e, vertex);
        }
    }
}
