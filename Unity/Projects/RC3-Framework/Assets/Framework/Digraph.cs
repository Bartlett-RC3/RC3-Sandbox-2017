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
    /// Simple adjaceny list representation of a directed graph.
    /// </summary>
    public class Digraph : IDigraph
    {
        #region Static
        
        public static readonly DigraphFactory Factory = new DigraphFactory();
        private const int _defaultCapacity = 4;

        #endregion


        private List<List<int>> _vertices;


        /// <summary>
        /// 
        /// </summary>
        public Digraph(int vertexCapacity = _defaultCapacity)
        {
            _vertices = new List<List<int>>(vertexCapacity);
        }


        /// <summary>
        /// Returns the number of vertices in the graph.
        /// </summary>
        public int VertexCount
        {
            get { return _vertices.Count; }
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
            _vertices[v0].Add(v1);
            _vertices[v1].Add(v0);
        }


        /// <summary>
        /// 
        /// </summary>
        public int GetConnectedVertex(int vertex, int index)
        {
            return _vertices[vertex][index];
        }


        /// <summary>
        /// Returns all vertices connected to the given vertex.
        /// </summary>
        public IEnumerable<int> GetConnectedVertices(int vertex)
        {
            return _vertices[vertex];
        }
    }
}
