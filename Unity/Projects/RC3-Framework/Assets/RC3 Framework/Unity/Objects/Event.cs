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
    [CreateAssetMenu(menuName = "RC3/Event")]
    public class Event : ScriptableObject
    {
        private List<EventListener> _listeners = new List<EventListener>();


        /// <summary>
        /// 
        /// </summary>
        public void Raise()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventRaised();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="listener"></param>
        public void Register(EventListener listener)
        {
            _listeners.Add(listener);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="listener"></param>
        public void Unregister(EventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}
