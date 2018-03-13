using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.Events;

/*
 * Notes
 * Based on "ScriptableObject Events" pattern as detailed in https://www.youtube.com/watch?v=raQ3iHhE_Kk (@ 00:32:50)
 */

namespace RC3.Unity
{
    /// <summary>
    /// 
    /// </summary>
    public class EventListener : MonoBehaviour
    {
        [SerializeField]
        private Event _event;

        [SerializeField]
        private UnityEvent _response;


        /// <summary>
        /// 
        /// </summary>
        private void OnEnable()
        {
            _event.Register(this);
        }


        /// <summary>
        /// 
        /// </summary>
        private void OnDisable()
        {
            _event.Unregister(this);
        }


        /// <summary>
        /// 
        /// </summary>
        public void OnEventRaised()
        {
            _response.Invoke();
        }
    }
}
