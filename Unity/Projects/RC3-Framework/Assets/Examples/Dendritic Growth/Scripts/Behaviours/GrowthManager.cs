using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public partial class GrowthManager : MonoBehaviour
    {
        [SerializeField] private SharedSelection _sources;
        [SerializeField] private SharedGraph _grid;

        private Graph _graph;
        private List<VertexObject> _vertices;
        private Queue<int> _queue;


        /// <summary>
        /// 
        /// </summary>
        void Start()
        {
            _graph = _grid.Graph;
            _vertices = _grid.VertexObjects;
            _queue = new Queue<int>();
        }


        /// <summary>
        /// 
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ResetGrowth();

            if (Input.GetKeyDown(KeyCode.C))
                ClearSources();

            UpdateGrowth();
        }


        /// <summary>
        /// 
        /// </summary>
        private void ResetGrowth()
        {
            _queue.Clear();

            // reset visited vertices
            foreach (var v in _vertices)
            {
                if (v.Status == VertexStatus.Visited)
                    v.Status = VertexStatus.Default;
            }

            // enqueue sources
            foreach (int v in _sources.Indices)
                _queue.Enqueue(v);
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
            
            foreach(var vi in _graph.GetVertexNeighbors(_queue.Dequeue()))
            {
                var v = _vertices[vi];
                if (v.Status != VertexStatus.Default) continue;

                if(CountVisitedNeighbours(vi) == 1)
                {
                    v.Status = VertexStatus.Visited;
                    _queue.Enqueue(vi);
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
