using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Notes
 */ 

namespace RC3.Unity.Examples.DendriticGrowth
{
    /// <summary>
    /// Manages the growth process
    /// </summary>
    public class GrowthManager : MonoBehaviour
    {
        #region Nested Types

        /// <summary>
        /// 
        /// </summary>
        private enum VertexStatus
        {
            Default,
            Visited,
            Source
        }

        #endregion


        [SerializeField] private SharedSelection _sources;
        [SerializeField] private SharedGraph _grid;
        [SerializeField] private SharedMeshes _meshes;
        [SerializeField] private SharedMaterials _materials;
        [SerializeField] private SharedFloats _scales;

        private Graph _graph;
        private List<VertexObject> _vertices;

        private VertexStatus[] _status;
        private Queue<int> _queue;


        /// <summary>
        /// 
        /// </summary>
        void Start()
        {
            _graph = _grid.Graph;
            _vertices = _grid.VertexObjects;

            _status = new VertexStatus[_graph.VertexCount];
            _queue = new Queue<int>();
        }


        /// <summary>
        /// 
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ResetGrowth();

            UpdateGrowth();
        }


        /// <summary>
        /// 
        /// </summary>
        private void ResetGrowth()
        {
            for (int i = 0; i < _status.Length; i++)
            {
                if (_status[i] == VertexStatus.Visited)
                {
                    _status[i] = VertexStatus.Default;
                    SetDefault(_vertices[i]);
                }
            }

            _queue.Clear();
            foreach (int v in _sources.Indices)
            {
                _status[v] = VertexStatus.Source;
                _queue.Enqueue(v);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void UpdateGrowth()
        {
            if (_queue.Count == 0)
                return;
            
            var v0 = _queue.Dequeue();

            foreach(var v1 in _graph.GetConnectedVertices(v0))
            {
                if(_status[v1] == VertexStatus.Default && CountVisitedNeighbours(v1) == 1)
                {
                    _queue.Enqueue(v1);
                    _status[v1] = VertexStatus.Visited;
                    SetVisited(_vertices[v1]);
                }
            }
        }


        /// <summary>
        /// Returns the number of visited or source neighbours
        /// </summary>
        private int CountVisitedNeighbours(int vertex)
        {
            int count = 0;

            foreach(var v in _graph.GetConnectedVertices(vertex))
            {
                if (_status[v] != VertexStatus.Default)
                    count++;
            }

            return count;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        private void SetDefault(VertexObject vertex)
        {
            const int i = 0;

            vertex.Filter.sharedMesh = _meshes[i];
            vertex.Renderer.sharedMaterial = _materials[i];

            var t = _scales[i];
            vertex.transform.localScale = new Vector3(t, t, t);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        private void SetVisited(VertexObject vertex)
        {
            const int i = 2;

            vertex.Filter.sharedMesh = _meshes[i];
            vertex.Renderer.sharedMaterial = _materials[i];

            var t = _scales[i];
            vertex.transform.localScale = new Vector3(t, t, t);
        }
    }
}
