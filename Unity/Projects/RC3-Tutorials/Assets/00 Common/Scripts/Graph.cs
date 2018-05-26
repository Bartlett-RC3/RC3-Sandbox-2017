using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Notes
 */ 

namespace RC3
{
    /// <summary>
    /// Simple adjacency representation of a graph
    /// </summary>
    public class Graph : IGraph
    {
        #region Static
        
        public static readonly GraphFactory Factory = new GraphFactory();
        private const int _defaultCapacity = 4;

        #endregion


        private List<List<int>> _adj;


        /// <summary>
        /// 
        /// </summary>
        public Graph(int vertexCapacity = _defaultCapacity)
        {
            _adj = new List<List<int>>(vertexCapacity);
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
        /// Adds an edge between the two given vertices.
        /// </summary>
        public void AddEdge(int v0, int v1)
        {
            _adj[v0].Add(v1);
            _adj[v1].Add(v0);
        }


        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<int> GetConnectedVertices(int vertex)
        {
            return _adj[vertex];
        }
    }
}
