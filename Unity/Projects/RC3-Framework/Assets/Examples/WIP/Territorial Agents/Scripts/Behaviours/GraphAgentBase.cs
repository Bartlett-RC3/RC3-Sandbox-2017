using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RC3.Graphs;

namespace RC3.Unity.TerritorialAgents
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class GraphAgentBase : MonoBehaviour
    {
        [SerializeField] private SharedEdgeGraph _sharedGraph;

        protected EdgeGraph _graph;
        protected List<VertexObject> _vertices;
        protected List<EdgeObject> _edges;

        protected List<int> _myVertices;
        protected System.Random _random;


        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            _graph = _sharedGraph.Graph;
            _vertices = _sharedGraph.VertexObjects;
            _edges = _sharedGraph.EdgeObjects;

            _myVertices = new List<int>();
            _random = new System.Random(0);

            _myVertices.Add(_random.Next(_graph.VertexCount));
        }


        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            UpdateMyVertices();
        }


        /// <summary>
        /// 
        /// </summary>
        protected abstract void UpdateMyVertices();
    }
}
