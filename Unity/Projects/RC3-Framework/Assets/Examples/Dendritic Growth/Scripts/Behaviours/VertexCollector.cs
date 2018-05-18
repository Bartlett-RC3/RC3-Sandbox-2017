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
    public class VertexCollector : MonoBehaviour
    {
        [SerializeField] private SharedSelection _selection;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            var v = other.GetComponent<VertexObject>();

            if (v != null)
                _selection.Indices.Add(v.Vertex);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="collision"></param>
        void OnTriggerExit(Collider other)
        {
            var v = other.GetComponent<VertexObject>();

            if (v != null)
                _selection.Indices.Remove(v.Vertex);
        }
    }
}
