using System.Collections;
using System.Collections.Generic;
using RC3.WFC;
using UnityEngine;

using RC3.Graphs;

namespace RC3.Unity.WFCDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class BoundaryInitializer : TileModelInitializer
    {
        [SerializeField] private SharedDigraph _tileGraph;
        [SerializeField] private int[] _tiles;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public override void Initialize(TileModel model)
        {
            var graph = _tileGraph.Graph;
            
            for (int i = 0; i < graph.VertexCount; i++)
            {
                foreach (int j in graph.GetVertexNeighborsOut(i))
                {
                    if (j == i)
                    {
                        model.SetDomain(i, _tiles);
                        break;
                    }
                }
            }
        }
    }
}
