using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Notes
 */

namespace RC3.Unity.DendriticGrowth
{
    /// <summary>
    /// 
    /// </summary>
    public class SharedItems<T> : ScriptableObject
    {
        [SerializeField]
        private T[] _items;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public T this[int i]
        {
            get { return _items[i]; }
        }


        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return _items.Length; }
        }
    }
}