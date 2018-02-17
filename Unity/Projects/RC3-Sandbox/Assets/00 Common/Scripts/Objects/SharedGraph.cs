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
    [CreateAssetMenu(menuName = "Objects/SharedGraph")]
    public class SharedGraph : ScriptableObject
    {
        private Graph _graph;
        private List<VertexObject> _vertexObjs;


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
        public List<VertexObject> VertexObjects
        {
            get { return _vertexObjs; }
        }


        /// <summary>
        /// 
        /// </summary>
        public void Initialize(Graph graph)
        {
            _graph = graph;
            _vertexObjs = new List<VertexObject>(_graph.VertexCount);
        }
    }
}