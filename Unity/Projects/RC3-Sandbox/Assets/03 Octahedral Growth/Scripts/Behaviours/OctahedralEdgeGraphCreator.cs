using System;
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
    public class OctahedralEdgeGraphCreator : MonoBehaviour
    {
        [SerializeField] private SharedSelection _sources;


        [SerializeField] private SharedGraph _graph;
        [SerializeField] private VertexObject _vertexObject;
        [SerializeField] private EdgeObject _edgeObject;
        [SerializeField] private int _countX = 5;
        [SerializeField] private int _countY = 5;
        [SerializeField] private int _countZ = 5;

        private EdgeGraph _graph;
        private VertexObject[] _vertices;
        private EdgeObject[] _edges;


        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            _graph = EdgeGraph.Factory.CreateTruncatedOctahedronGrid(_countX, _countY, _countZ);

            // initialize game objects
            InitVertexObjects();
            InitEdgeObjects();
        }


        /// <summary>
        /// 
        /// </summary>
        private void InitVertexObjects()
        {
            _vertices = new VertexObject[_graph.VertexCount];

            var dp = new Vector3(0.5f, 0.5f, 0.5f);
            int di = _countX * _countY * _countZ;
            int i = 0;

            for (int z = 0; z < _countZ; z++)
            {
                for (int y = 0; y < _countY; y++)
                {
                    for (int x = 0; x < _countX; x++)
                    {
                        var p = new Vector3(x, y, z);
                        CreateVertexObject(p, i); // primal vertex
                        CreateVertexObject(p + dp, i + di); // dual vertex
                        i++;
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void CreateVertexObject(Vector3 postion, int index)
        {
            var vObj = Instantiate(_vertexObject, transform);
            vObj.Index = index;
            
            // set position
            vObj.transform.localPosition = postion;

            // cache it
            _vertices[index] = vObj;
        }
        

        /// <summary>
        /// 
        /// </summary>
        private void InitEdgeObjects()
        {
            _edges = new EdgeObject[_graph.EdgeCount];

            for (int i = 0; i < _graph.EdgeCount; i++)
            {
                var e = _graph.GetEdge(i);

                var p0 = _vertices[e.Start].transform.position;
                var p1 = _vertices[e.End].transform.position;

                var eObj = Instantiate(_edgeObject, transform);
                eObj.Index = i;

                var xform = eObj.transform;
                var dir = p1 - p0;
                var mag = dir.magnitude;

                // scale to edge length
                xform.localScale = new Vector3(0.1f, mag * 0.5f, 0.1f);

                // translate to edge mid point
                xform.localPosition = (p0 + p1) * 0.5f; //Vector3.Lerp(p0, p1, 0.5f);

                // align up with edge direction
                xform.localRotation = Quaternion.FromToRotation(xform.up, dir);

                // cache it
                _edges[i] = eObj;
            }
        }
    }
}
