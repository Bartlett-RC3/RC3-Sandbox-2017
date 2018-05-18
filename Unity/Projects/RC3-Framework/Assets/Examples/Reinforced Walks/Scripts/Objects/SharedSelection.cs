using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Notes
 */ 

namespace RC3.Unity.ReinforcedWalks
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "RC3/ReinforcedWalks/Shared/Selection")]
    public class SharedSelection : ScriptableObject
    {
        private HashSet<int> _indices;


        /// <summary>
        /// 
        /// </summary>
        public HashSet<int> Indices
        {
            get { return _indices; }
        }


        /// <summary>
        /// 
        /// </summary>
        private void OnEnable()
        {
            _indices = new HashSet<int>();
        }
    }
}
