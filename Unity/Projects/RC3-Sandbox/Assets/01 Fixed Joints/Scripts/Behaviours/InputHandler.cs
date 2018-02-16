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
    public class InputHandler : MonoBehaviour
    {
        private Vector3 _gravity;


        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            OnToggleGravity(false);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void OnToggleGravity(bool value)
        {
            if (value)
            {
                Physics.gravity = _gravity;
            }
            else
            {
                _gravity = Physics.gravity;
                Physics.gravity = Vector3.zero;
            }
        }
    }
}
