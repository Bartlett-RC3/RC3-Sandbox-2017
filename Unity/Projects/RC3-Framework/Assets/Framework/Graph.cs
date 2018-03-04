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
    /// Simple adjacency list representation of an undirected graph.
    /// </summary>
    public class Graph : IGraph
    {
        #region Static
        
        public static readonly GraphFactory Factory = new GraphFactory();
        private const int _defaultCapacity = 4;

        #endregion


        private List<List<int>> _verts;


        /// <summary>
        /// 
        /// </summary>
        public Graph(int vertexCapacity = _defaultCapacity)
        {
            _verts = new List<List<int>>(vertexCapacity);
        }


        /// <summary>
        /// Returns the number of vertices in the graph.
        /// </summary>
        public int VertexCount
        {
            get { return _verts.Count; }
        }


        /// <summary>
        /// Returns the degree of the given vertex.
        /// </summary>
        public int GetDegree(int vertex)
        {
            return _verts[vertex].Count;
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
            _verts.Add(new List<int>(capacity));
        }


        /// <summary>
        /// Adds an edge between the given vertices.
        /// </summary>
        public void AddEdge(int v0, int v1)
        {
            _verts[v0].Add(v1);
            _verts[v1].Add(v0);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GetVertexNeighbor(int vertex, int index)
        {
            return _verts[vertex][index];
        }


        /// <summary>
        /// Returns all vertices connected to the given vertex.
        /// </summary>
        public IEnumerable<int> GetVertexNeighbors(int vertex)
        {
            return _verts[vertex];
        }
    }
}
