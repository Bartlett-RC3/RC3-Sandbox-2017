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
    public class BlockSelectionHandler : MonoBehaviour, ISelectionHandler
    {
        [SerializeField] private SharedMeshes _meshes;
        [SerializeField] private SharedMaterials _materials;
        [SerializeField] private SharedFloats _scales;

        private Rigidbody _rigidbody;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        

        /// <summary>
        /// 
        /// </summary>
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
            OnDeselected();
        }


        /// <summary>
        /// 
        /// </summary>
        public bool IsSelected
        {
            get { return _rigidbody.isKinematic; }
        }


        /// <summary>
        /// 
        /// </summary>
        public void OnSelected()
        {
            _rigidbody.isKinematic = true;
            const int index = 1;

            _meshFilter.mesh = _meshes[index];
            _meshRenderer.material = _materials[index];

            var t = _scales[index];
            transform.localScale = new Vector3(t, t, t);
        }


        /// <summary>
        /// 
        /// </summary>
        public void OnDeselected()
        {
            _rigidbody.isKinematic = false;
            const int index = 0;

            _meshFilter.mesh = _meshes[index];
            _meshRenderer.material = _materials[index];

            var t = _scales[index];
            transform.localScale = new Vector3(t, t, t);
        }
    }
}
