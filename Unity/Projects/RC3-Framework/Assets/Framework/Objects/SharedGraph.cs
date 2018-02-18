using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Notes
 */

namespace RC3.Unity
{
    /// <summary>
    /// 
    /// </summary>
    public class SharedGraph<V> : ScriptableObject
        where V : VertexObject
    {
        private Graph _graph;
        private List<V> _vertexObjs;


        /// <summary>
        /// 
        /// </summary>
        public Graph Graph
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
        public void Initialize(Graph graph)
        {
            _graph = graph;
            _vertexObjs = new List<V>(_graph.VertexCount);
        }
    }
}