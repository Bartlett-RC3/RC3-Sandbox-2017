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
        [SerializeField] private int _index;


        /// <summary>
        /// 
        /// </summary>
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }
    }
}
