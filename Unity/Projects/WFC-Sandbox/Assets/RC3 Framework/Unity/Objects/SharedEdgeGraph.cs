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
    public class SharedEdgeGraph<V, E> : ScriptableObject
        where V : VertexObject
        where E : EdgeObject
    {
        private EdgeGraph _graph;
        private List<V> _vertexObjs;
        private List<E> _edgeObjs;


        /// <summary>
        /// 
        /// </summary>
        public EdgeGraph Graph
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
        public void Initialize(EdgeGraph graph)
        {
            _graph = graph;
            _vertexObjs = new List<V>(_graph.VertexCount);
            _edgeObjs = new List<E>(_graph.EdgeCount);
        }
    }
}