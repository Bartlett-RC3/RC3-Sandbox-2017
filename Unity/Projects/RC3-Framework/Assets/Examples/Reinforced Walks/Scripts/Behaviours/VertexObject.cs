using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Notes
 */
 
namespace RC3.Unity.ReinforcedWalks
{
    /// <summary>
    /// 
    /// </summary>
    public class VertexObject : RC3.Unity.VertexObject, ISelectionHandler
    {
        #region Static
        
        private const float _minScale = 0.05f;

        #endregion


        [SerializeField] private SharedSelection _sources;
        [SerializeField] private SharedMaterials _materials;
        
        private MeshRenderer _renderer;
        private VertexStatus _status;
        private float _scale;


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
        public float Scale
        {
            get { return _scale; }
            set
            {
                _scale = Mathf.Max(value, _minScale);
                OnSetScale();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void OnSetStatus()
        {
            _renderer.sharedMaterial = _materials[(int)_status];

        }


        /// <summary>
        /// 
        /// </summary>
        private void OnSetScale()
        {
            transform.localScale = new Vector3(_scale, _scale, _scale);
        }


        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
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