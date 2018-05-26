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
    public class TriangleGridEdgeGraph : MonoBehaviour
    {
        [SerializeField] private SharedSelection _sources;
        [SerializeField] private VertexObject _vertexObject;
        [SerializeField] private EdgeObject _edgeObject;
        [SerializeField, Range(0.001f, 1.0f)] private float _trafficScale = 1.0f;
        [SerializeField, Range(0, 100)] private int _trafficMax = 100;
        [SerializeField] private int _countX = 5;
        [SerializeField] private int _countY = 5;

        private EdgeGraph _graph;
        private VertexObject[] _vertices;
        private EdgeObject[] _edges;

        private float[] _vertexDistances;
        private float[] _edgeLengths;
        private int[] _edgeTraffic;


        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            _graph = EdgeGraph.Factory.CreateTriangleGrid(_countX, _countY);

            // initialize game objects
            InitVertexObjects();
            InitEdgeObjects();

            // initialize additonal attributes
            _vertexDistances = new float[_graph.VertexCount];
            _edgeLengths = new float[_graph.EdgeCount];
            _edgeTraffic = new int[_graph.EdgeCount];

            // initialize edge lengths
            for (int i = 0; i < _graph.EdgeCount; i++)
            {
                var e = _graph.GetEdge(i);
                var p0 = _vertices[e.Start].transform.localPosition;
                var p1 = _vertices[e.End].transform.localPosition;
                _edgeLengths[i] = Vector3.Distance(p0, p1) + UnityEngine.Random.Range(0, 0.001f);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void InitVertexObjects()
        {
            _vertices = new VertexObject[_graph.VertexCount];
            int index = 0;

            for (int y = 0; y < _countY; y++)
            {
                float dx = (y % 2 == 0) ? 0.0f : 0.5f;

                for (int x = 0; x < _countX; x++)
                {
                    // create vertex
                    var vObj = Instantiate(_vertexObject, transform);
                    vObj.Index = index;

                    // set position
                    vObj.transform.localPosition = new Vector3(x + dx, 0, y);

                    // cache it
                    _vertices[index++] = vObj;
                }
            }
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


        /// <summary>
        /// 
        /// </summary>
        void Update()
        {
            // calculate distances when key is pressed
            if (Input.GetKeyDown(KeyCode.Space))
                UpdateEdgeTraffic();
        }


        /// <summary>
        /// 
        /// </summary>
        private void UpdateEdgeTraffic()
        {
            GraphUtil.GetVertexDistances(_graph, _edgeLengths, _sources.Indices, _vertexDistances);

            /*
            // negate distances for walk to max
            for (int i = 0; i < _vertexDistances.Length; i++)
                _vertexDistances[i] *= -1.0f;
            */

            // zero out all edge traffic
            for (int i = 0; i < _edgeLengths.Length; i++)
                _edgeTraffic[i] = 0;

            // shortes walk from each vertex
            for (int i = 0; i < _graph.VertexCount; i++)
            {
                var path = GraphUtil.WalkToMin(_graph, _vertexDistances, i);

                foreach (var ei in path)
                    _edgeTraffic[ei]++;
            }

            // scale cross section of each edge by amount of traffic
            for (int i = 0; i < _edges.Length; i++)
            {
                var xform = _edges[i].transform;
                var scale = xform.localScale;
                var xz = Math.Min(_edgeTraffic[i], _trafficMax) * _trafficScale;
                xform.localScale = new Vector3(xz, scale.y, xz);
            }
        }
    }
}
