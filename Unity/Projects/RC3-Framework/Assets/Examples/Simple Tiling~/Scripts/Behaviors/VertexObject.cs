using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC3.Unity.SimpleTiling
{
    /// <summary>
    /// 
    /// </summary>
    public class VertexObject : RC3.Unity.VertexObject
    {
        [SerializeField] private Tile _tile;

        private GameObject _child;
        private MeshFilter _filter;
        private MeshRenderer _renderer;
        private Vector3 _scale;


        /// <summary>
        /// 
        /// </summary>
        public Tile Tile
        {
            get { return _tile; }
            set
            {
                _tile = value;
                OnSetTile();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            _child = transform.GetChild(0).gameObject;
            _filter = GetComponent<MeshFilter>();
            _renderer = GetComponent<MeshRenderer>();
            _scale = transform.localScale;
            OnSetTile();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        private void OnSetTile()
        {
            transform.localScale = _scale;

            if (_tile == null)
            {
                _filter.sharedMesh = null;
                _renderer.sharedMaterial = null;
                _child.SetActive(true);
                return;
            }

            _filter.sharedMesh = _tile.Mesh;
            _renderer.sharedMaterial = _tile.Material;
            _child.SetActive(false);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        public void SetScale(float factor)
        {
            transform.localScale = _scale * factor;
        }
    }
}
