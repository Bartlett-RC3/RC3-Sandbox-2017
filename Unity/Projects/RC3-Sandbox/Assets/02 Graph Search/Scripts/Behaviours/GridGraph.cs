using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Notes
 */ 

namespace RC3.Unity
{
    /// <summary>
    /// 
    /// </summary>
    public class GridGraph : MonoBehaviour
    {
        [SerializeField] private SharedSelection _sources;
        [SerializeField] private SharedSelection _ignored;
        [SerializeField] private VertexObject _vertexObject;
        [SerializeField, Range(0.0f, 1.0f)] private float _depthScale = 0.1f;
        [SerializeField] private int _countX = 5;
        [SerializeField] private int _countY = 5;

        private Graph _graph;
        private VertexObject[] _vertices;
        private int[] _depths;


        /// <summary>
        /// 
        /// </summary>
        void Start()
        {
            _graph = Graph.Factory.CreateGrid(_countX, _countY);
            InitializeVertexObjects();
            _depths = new int[_graph.VertexCount];
        }


        /// <summary>
        /// 
        /// </summary>
        private void InitializeVertexObjects()
        {
            _vertices = new VertexObject[_graph.VertexCount];
            int index = 0;

            for (int y = 0; y < _countY; y++)
            {
                for (int x = 0; x < _countX; x++)
                {
                    // create vertex
                    var vObj = Instantiate(_vertexObject, transform);

                    // set vertex attributes
                    vObj.transform.localPosition = new Vector3(x, 0, y);
                    vObj.Index = index;

                    // cache it
                    _vertices[index++] = vObj;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && _sources.Indices.Count > 0)
                UpdateVertexDepths();
        }


        /// <summary>
        /// 
        /// </summary>
        private void UpdateVertexDepths()
        {
            GraphUtil.GetVertexDepths(_graph, _sources.Indices, _depths, _ignored.Indices);

            // map vertex depth to position.y
            for (int i = 0; i < _vertices.Length; i++)
            {
                var vt = _vertices[i].transform;
                var p = vt.localPosition;

                p.y = _depths[i] * _depthScale;
                vt.localPosition = p;
            }
        }
    }
}
