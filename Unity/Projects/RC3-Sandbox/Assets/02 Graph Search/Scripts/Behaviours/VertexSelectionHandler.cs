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
    public class VertexSelectionHandler : MonoBehaviour, ISelectionHandler
    {
        [SerializeField] private SharedSelection _sources;
        [SerializeField] private SharedMeshes _meshes;
        [SerializeField] private SharedMaterials _materials;
        [SerializeField] private SharedFloats _scales;

        private VertexObject _vertex;
        private MeshFilter _filter;
        private MeshRenderer _renderer;
        private bool _selected;


        /// <summary>
        /// 
        /// </summary>
        public bool IsSelected
        {
            get { return _selected; }
        }


        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            _vertex = GetComponent<VertexObject>();
            _filter = GetComponent<MeshFilter>();
            _renderer = GetComponent<MeshRenderer>();
            OnDeselected();
        }


        /// <summary>
        /// 
        /// </summary>
        public void OnDeselected()
        {
            _sources.Indices.Remove(_vertex.Index);
            const int index = 0;

            _filter.sharedMesh = _meshes[index];
            _renderer.sharedMaterial = _materials[index];

            var t = _scales[index];
            transform.localScale = new Vector3(t, t, t);

            _selected = false;
        }


        /// <summary>
        /// 
        /// </summary>
        public void OnSelected()
        {
            _sources.Indices.Add(_vertex.Index);
            const int index = 1;

            _filter.sharedMesh = _meshes[index];
            _renderer.sharedMaterial = _materials[index];

            var t = _scales[index];
            transform.localScale = new Vector3(t, t, t);

            _selected = true;
        }
    }
}