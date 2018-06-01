using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SpatialSlur.Core;

/*
 * Notes
 */

namespace RC3.Graphs
{
    /// <summary>
    /// Simple adjacency list representation of an undirected graph.
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
        /// Returns the number of vertices in the graph.
        /// </summary>
        public int VertexCount
        {
            get { return _adj.Count; }
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
        /// Adds an edge between the given vertices.
        /// </summary>
        public void AddEdge(int v0, int v1)
        {
            _adj[v0].Add(v1);

            // don't add again if edge is a loop
            if(v1 != v0)
                _adj[v1].Add(v0);
        }


        /// <summary>
        /// Returns true if there is an edge between the given vertices.
        /// </summary>
        public bool HasEdge(int v0, int v1)
        {
            return _adj[v0].Contains(v1);
        }


        /// <summary>
        /// Returns all vertices connected to the given vertex.
        /// </summary>
        public ReadOnlyListView<int> GetVertexNeighbors(int vertex)
        {
            var adj = _adj[vertex];
            return adj.GetReadOnlyView(adj.Count);
        }
    }
}
