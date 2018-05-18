//using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SpatialSlur.Core;
using RC3.Graphs;

/*
 * Notes
 */

namespace RC3.Unity.GraphGrowth
{
    /// <summary>
    /// Manages the growth process
    /// </summary>
    public partial class GraphGrowthManager : MonoBehaviour
    {
        [SerializeField] private SharedEdgeGraph _sharedGraph;
        [SerializeField] private Transform _target;

        private EdgeGraph _graph;
        private List<VertexObject> _vertices;
        private List<EdgeObject> _edges;

        PriorityQueue<float, Triangle> _queue;
        private List<Vector3> _positions;


        /// <summary>
        /// 
        /// </summary>
        void Start()
        {
            _graph = _sharedGraph.Graph;
            _vertices = _sharedGraph.VertexObjects;
            _edges = _sharedGraph.EdgeObjects;

            _queue = new PriorityQueue<float, Triangle>();
            _positions = new List<Vector3>();
     
            InitSeed();
        }


        /// <summary>
        /// Adds first triangle to the graph
        /// </summary>
        private void InitSeed()
        {
            AddVertex(new Vector3(0,0,0));
            AddVertex(new Vector3(1,0,0));
            AddVertex(new Vector3(0,1,0));

            _graph.AddEdge(0, 1);
            _graph.AddEdge(1, 2);
            _graph.AddEdge(2, 0);

            var t = new Triangle(0, 1, 2);
            _queue.Insert(GetScore(t), t);
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="triangle"></param>
        /// <returns></returns>
        private float GetScore(Triangle triangle)
        {
            return Vector3.Distance(_target.position, GetCenter(triangle));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tri"></param>
        /// <returns></returns>
        private Vector3 GetCenter(Triangle tri)
        {
            const float inv3 = 1.0f / 3.0f;
            return (_positions[tri.Vertex0] + _positions[tri.Vertex1] + _positions[tri.Vertex2]) * inv3;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tri"></param>
        /// <returns></returns>
        private Vector3 GetNormal(Triangle tri)
        {
            var d0 = _positions[tri.Vertex1] - _positions[tri.Vertex0];
            var d1 = _positions[tri.Vertex2] - _positions[tri.Vertex1];
            return Vector3.Cross(d0, d1);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        private void AddVertex(Vector3 position)
        {
            _graph.AddVertex();
            _positions.Add(position);
        }


        /// <summary>
        /// 
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                UpdateGrowth();

            DrawDebug();
        }


        /// <summary>
        /// 
        /// </summary>
        private void UpdateGrowth()
        {
            Triangle t;
            float d;
            _queue.RemoveMin(out d, out t);

            AddTriangles(t);

            Debug.Log("Update growth!");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tri"></param>
        private void AddTriangles(Triangle tri)
        {
            var n = GetNormal(tri);
            var c = GetCenter(tri);
            n.Normalize();

            // add new vertex to the graph
            var v = _graph.VertexCount;
            AddVertex(c + n);

            // add new edges to the graph
            _graph.AddEdge(v, tri.Vertex0);
            _graph.AddEdge(v, tri.Vertex1);
            _graph.AddEdge(v, tri.Vertex2);

            // add new triangles to the queue
            var t0 = new Triangle(v, tri.Vertex0, tri.Vertex1);
            _queue.Insert(GetScore(t0), t0);

            var t1 = new Triangle(v, tri.Vertex1, tri.Vertex2);
            _queue.Insert(GetScore(t1), t1);

            var t2 = new Triangle(v, tri.Vertex2, tri.Vertex0);
            _queue.Insert(GetScore(t2), t2);
        }


        /// <summary>
        /// 
        /// </summary>
        private void DrawDebug()
        {
            for (int i = 0; i < _graph.EdgeCount; i++)
            {
                var v0 = _graph.GetStartVertex(i);
                var v1 = _graph.GetEndVertex(i);
                Debug.DrawLine(_positions[v0], _positions[v1]);
            }
        }
    }
}
