using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RC3.Graphs;

/*
 * Notes 
 */

namespace RC3.Unity.GraphGrowth
{
    /// <summary>
    /// 
    /// </summary>
    public class EdgeGraphCreator : MonoBehaviour
    {
        [SerializeField] private SharedEdgeGraph _sharedGraph;


        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            _sharedGraph.Initialize(new EdgeGraph());
        }
    }
}
