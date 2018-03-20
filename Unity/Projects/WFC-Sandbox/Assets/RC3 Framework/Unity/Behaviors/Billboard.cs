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
        private Transform _target;

        
        // Use this for initialization
        void Start()
        {
            _target = Camera.main.transform;
        }


        // Update is called once per frame
        void Update()
        {
            var d = transform.position - _target.position;
        
            if (d.sqrMagnitude > 0.0f)
                transform.rotation = Quaternion.LookRotation(d, _target.up);
        }
    }
}