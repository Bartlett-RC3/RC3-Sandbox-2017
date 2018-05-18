using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SpatialSlur.Core;
using RC3.Graphs;
using RC3.Unity;


/*
 * Notes
 */

namespace RC3.Unity.DendriticGrowth
{
    /// <summary>
    /// Manages the growth process
    /// </summary>
    public partial class DirectedGrowthManager : MonoBehaviour
    {
        [SerializeField] private SharedSelection _sources;
        [SerializeField] private SharedGraph _grid;
        [SerializeField] private Transform[] _targets;

        private Graph _graph;
        private List<VertexObject> _vertices;
        private PriorityQueue<float, int> _queue;
        private bool _pause;

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        private float GetKey(int vertex)
        {
            var p = _vertices[vertex].transform.position;
            float sum = 0.0f;

            foreach (var target in _targets)
            {
                var d = target.position - p;
                sum += 1.0f / d.sqrMagnitude;
            }

            return -sum;
        }
        

        /// <summary>
        /// 
        /// </summary>
        void Start()
        {
            _graph = _grid.Graph;
            _vertices = _grid.VertexObjects;
            _queue = new PriorityQueue<float, int>();
        }


        /// <summary>
        /// 
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ResetGrowth();

            if (Input.GetKeyDown(KeyCode.P))
                _pause = !_pause;
               
            if (Input.GetKeyDown(KeyCode.C))
                ClearSources();

            if (!_pause)
                UpdateGrowth();
        }


        /// <summary>
        /// 
        /// </summary>
        private void ResetGrowth()
        {
            _queue = new PriorityQueue<float, int>();

            // reset visited vertices
            foreach (var v in _vertices)
            {
                if (v.Status == VertexStatus.Visited)
                    v.Status = VertexStatus.Default;
            }

            // enqueue sources
            foreach (int v in _sources.Indices)
                _queue.Insert(GetKey(v), v);
        }


        /// <summary>
        /// 
        /// </summary>
        private void ClearSources()
        {
            foreach (int v in _sources.Indices)
                _vertices[v].Status = VertexStatus.Default;

            _sources.Indices.Clear();
        }


        /// <summary>
        /// 
        /// </summary>
        private void UpdateGrowth()
        {
            if (_queue.Count == 0)
                return;

            float key;
            int vertex;
            _queue.RemoveMin(out key, out vertex);

            foreach (var vi in _graph.GetVertexNeighbors(vertex))
            {
                var vobj = _vertices[vi];

                if(CountVisitedNeighbours(vi) < 2)
                {
                    vobj.Status = VertexStatus.Visited;
                    _queue.Insert(GetKey(vi), vi);
                }
            }
        }


        /// <summary>
        /// Returns the number of visited or source neighbours
        /// </summary>
        private int CountVisitedNeighbours(int vertex)
        {
            int count = 0;

            foreach(var vi in _graph.GetVertexNeighbors(vertex))
            {
                if (_vertices[vi].Status != VertexStatus.Default)
                    count++;
            }

            return count;
        }
    }
}
