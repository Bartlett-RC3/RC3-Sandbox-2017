using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Notes
 */
 
namespace RC3.Unity.TerritorialAgents
{
    /// <summary>
    /// 
    /// </summary>
    public class VertexObject : RC3.Unity.VertexObject
    {
        private GraphAgentBase _owner;


        /// <summary>
        /// 
        /// </summary>
        public GraphAgentBase Owner
        {
            get { return _owner; }
            set
            {
                _owner = value;
                OnSetOwner();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void OnSetOwner()
        {
            // update other things based on owner
        }
    }
}