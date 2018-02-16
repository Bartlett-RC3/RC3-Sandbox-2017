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
    /// 
    /// </summary>
    public class EdgeGraph : IEdgeGraph
    {
        #region Static

        public static readonly EdgeGraphFactory Factory = new EdgeGraphFactory();
        private const int _defaultCapacity = 4;

        #endregion


        private List<List<int>> _adj;
        private List<Edge> _edges;


        /// <summary>
        /// 
        /// </summary>
        public EdgeGraph(int vertexCapacity =  _defaultCapacity, int edgeCapacity = _defaultCapacity)
        {
            _adj = new List<List<int>>(vertexCapacity);
            _edges = new List<Edge>(edgeCapacity);
        }


        /// <summary>
        /// 
        /// </summary>
        public int VertexCount
        {
            get { return _adj.Count; }
        }


        /// <summary>
        /// 
        /// </summary>
        public int EdgeCount
        {
            get { return _edges.Count; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public int GetDegree(int vertex)
        {
            return _adj[vertex].Count;
        }


        /// <summary>
        /// 
        /// </summary>
        public void AddVertex(int capacity = _defaultCapacity)
        {
            _adj.Add(new List<int>(capacity));
        }


        /// <summary>
        /// Adds an edge between start and end
        /// </summary>
        public void AddEdge(int start, int end)
        {
            // add index of new edge to vertex lists
            var ei = _edges.Count;
            _adj[start].Add(ei);
            _adj[end].Add(ei);

            // add new edge to edge list
            _edges.Add(new Edge(start, end));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Edge GetEdge(int index)
        {
            return _edges[index];
        }


        /// <summary>
        /// Returns all edges incident to the given vertex.
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public IEnumerable<int> GetIncidentEdges(int vertex)
        {
            return _adj[vertex];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public IEnumerable<int> GetConnectedVertices(int vertex)
        {
            foreach (var ei in _adj[vertex])
                yield return _edges[ei].Other(vertex);
        }
    }
}
