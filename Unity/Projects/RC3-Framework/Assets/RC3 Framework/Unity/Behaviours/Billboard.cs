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
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private SharedTransform _target;


        /// <summary>
        /// 
        /// </summary>
        public Transform Target
        {
            get { return _target; }
            set { _target.Value = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        void Update()
        {
            var t = _target.Value;
            var d = transform.position - t.position;
        
            if (d.sqrMagnitude > 0.0f)
                transform.rotation = Quaternion.LookRotation(d, t.up);
        }
    }
}