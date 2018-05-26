using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Notes
 */
 
namespace RC3.Unity.Examples.DendriticGrowth
{
    /// <summary>
    /// 
    /// </summary>
    public class VertexObject : RC3.Unity.VertexObject
    {
        private MeshFilter _filter;
        private MeshRenderer _renderer;


        /// <summary>
        /// 
        /// </summary>
        public MeshFilter Filter
        {
            get { return _filter; }
        }


        /// <summary>
        /// 
        /// </summary>
        public MeshRenderer Renderer
        {
            get { return _renderer; }
        }


        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            _filter = GetComponent<MeshFilter>();
            _renderer = GetComponent<MeshRenderer>();
        }
    }
}