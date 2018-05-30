using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RC3.Graphs;

namespace RC3.Unity.TerritorialAgents
{
    /// <summary>
    /// 
    /// </summary>
    public class GraphAgentA : GraphAgentBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected override void UpdateMyVertices()
        {
            // TODO
            // how does agent A update it's vertices?

            // check ownership of all vertices
            foreach(var v in _myVertices)
            {
                if(_vertices[v].Owner != this)
                    ReleaseVertex(v);
            }
            
            AcquireVertex(GetBestVertex());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        private void AcquireVertex(int vertex)
        {
            var vobj = _vertices[vertex];

            if(vobj.Owner != this)
                return;

            _myVertices.Add(vertex);
            vobj.Owner = this;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetBestVertex()
        {
            return -1;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        private void ReleaseVertex(int vertex)
        {
            // blah blah
            _myVertices.Remove(vertex);
        }
    }
}
