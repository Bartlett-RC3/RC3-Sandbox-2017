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
    public class SharedDigraph<V> : ScriptableObject
        where V : VertexObject
    {
        private Digraph _graph;
        private List<V> _vertexObjs;


        /// <summary>
        /// 
        /// </summary>
        public Digraph Graph
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
        public void Initialize(Digraph graph)
        {
            _graph = graph;
            _vertexObjs = new List<V>(_graph.VertexCount);
        }
    }
}