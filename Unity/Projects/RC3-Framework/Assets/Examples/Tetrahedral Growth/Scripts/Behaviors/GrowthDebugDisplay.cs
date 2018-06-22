using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace RC3.Unity.TetrahedralGrowth
{
    /// <summary>
    /// 
    /// </summary>
    public class GrowthDebugDisplay : MonoBehaviour
    {
        [SerializeField] private GrowthManager _growthManager;
        [SerializeField] private Material _material;
        

        /// <summary>
        /// Debug display 
        /// </summary>
        void OnPostRender()
        {
            // call GL stuff here
            _material.SetPass(0);
            _growthManager.DebugDisplayTetrahedra();
        }
    }
}
