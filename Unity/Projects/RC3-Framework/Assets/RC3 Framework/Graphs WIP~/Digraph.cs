using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Notes
 */ 

namespace RC3.Graphs
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


        private List<List<int>> _adjOut;
        private List<List<int>> _adjIn;


        /// <summary>
        /// 
        /// </summary>
        public Digraph(int vertexCapacity = _defaultCapacity)
        {
            _adjOut = new List<List<int>>(vertexCapacity);
            _adjIn = new List<List<int>>(vertexCapacity);
        }


        /// <summary>
        /// Returns the number of vertices in the graph.
        /// </summary>
        public int VertexCount
        {
            get { return _adjOut.Count; }
        }


        /// <summary>
        /// Returns the number of vertices that the given vertex connects to.
        /// </summary>
        public int GetDegreeOut(int vertex)
        {
            return _adjOut[vertex].Count;
        }


        /// <summary>
        /// Returns the the number of vertices that connect to the given vertex.
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
            _adjOut[v0].Add(v1);
            _adjIn[v1].Add(v0);
        }


        /// <summary>
        /// 
        /// </summary>
        public bool HasEdge(int v0, int v1)
        {
            return _adjOut[v0].Contains(v1);
        }


        /// <summary>
        /// 
        /// </summary>
        public int GetVertexNeighborOut(int vertex, int index)
        {
            return _adjOut[vertex][index];
        }


        /// <summary>
        /// Returns all vertices that the given vertex connects to.
        /// </summary>
        public IEnumerable<int> GetVertexNeighborsOut(int vertex)
        {
            return _adjOut[vertex];
        }


        /// <summary>
        /// 
        /// </summary>
        public int GetVertexNeighborIn(int vertex, int index)
        {
            return _adjIn[vertex][index];
        }


        /// <summary>
        /// Returns all vertices that connect to the given vertex.
        /// </summary>
        public IEnumerable<int> GetVertexNeighborsIn(int vertex)
        {
            return _adjIn[vertex];
        }
    }
}
