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
    public class VertexObject : MonoBehaviour
    {
        [SerializeField] private int _vertex;


        /// <summary>
        /// Returns the vertex associated with this object.
        /// </summary>
        public int Vertex
        {
            get { return _vertex; }
            set { _vertex = value; }
        }
    }
}