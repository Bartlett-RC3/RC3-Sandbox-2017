using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Notes
 */
 
namespace RC3.Unity.DendriticGrowth
{
    /// <summary>
    /// 
    /// </summary>
    public class VertexObject : RC3.Unity.VertexObject, ISelectionHandler
    {
        [SerializeField] private SharedSelection _sources;
        [SerializeField] private SharedMeshes _meshes;
        [SerializeField] private SharedMaterials _materials;
        [SerializeField] private SharedSingles _scales;

        private MeshFilter _filter;
        private MeshRenderer _renderer;
        private VertexStatus _status;


        /// <summary>
        /// 
        /// </summary>
        public VertexStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnSetStatus();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void OnSetStatus()
        {
            int index = (int)_status;

            _filter.sharedMesh = _meshes[index];
            _renderer.sharedMaterial = _materials[index];

            var t = _scales[index];
            transform.localScale = new Vector3(t, t, t);
        }


        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            _filter = GetComponent<MeshFilter>();
            _renderer = GetComponent<MeshRenderer>();
            Status = VertexStatus.Default; // default vertex state
        }


        #region Explicit interface implementations

        /// <summary>
        /// 
        /// </summary>
        bool ISelectionHandler.IsSelected
        {
            get { return _status == VertexStatus.Source; }
        }


        /// <summary>
        /// 
        /// </summary>
        void ISelectionHandler.OnDeselected()
        {
            _sources.Indices.Remove(Vertex);
            Status = VertexStatus.Default;
        }


        /// <summary>
        /// 
        /// </summary>
        void ISelectionHandler.OnSelected()
        {
            _sources.Indices.Add(Vertex);
            Status = VertexStatus.Source;
        }

        #endregion
    }
}