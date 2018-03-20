using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RC3.Graphs;

/*
 * Notes
 */

namespace RC3.Unity
{
    /// <summary>
    /// 
    /// </summary>
    public class SharedEdgeDigraph<V, E> : ScriptableObject
        where V : VertexObject
        where E : EdgeObject
    {
        private EdgeDigraph _graph;
        private List<V> _vertexObjs;
        private List<E> _edgeObjs;


        /// <summary>
        /// 
        /// </summary>
        public EdgeDigraph Graph
        {
            get { return _graph; }
        }


        /// <summary>
        /// 
        /// </summary>
        public List<V> VertexObjects
        {
            get { return _vertexObjs; }
        }


        /// <summary>
        /// 
        /// </summary>
        public List<E> EdgeObjects
        {
            get { return _edgeObjs; }
        }


        /// <summary>
        /// 
        /// </summary>
        public void Initialize(EdgeDigraph graph)
        {
            _graph = graph;
            _vertexObjs = new List<V>(_graph.VertexCount);
            _edgeObjs = new List<E>(_graph.EdgeCount);
        }
    }
}