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
    public class EdgeObject : MonoBehaviour
    {
        [SerializeField] private int _edge;


        /// <summary>
        /// Returns the edge associated with this object.
        /// </summary>
        public int Edge
        {
            get { return _edge; }
            set { _edge = value; }
        }
    }
}
