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
    public class SetBoundaryTile : TileModelInitializer
    {
        [SerializeField] private SharedDigraph _tileGraph;
        [SerializeField] private int _tile;

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public override void Initialize(TileModel model)
        {
            if (_tile < 0)
                return;

            var graph = _tileGraph.Graph;
            
            for (int i = 0; i < graph.VertexCount; i++)
            {
                foreach (int j in graph.GetVertexNeighborsOut(i))
                {
                    if (j == i)
                    {
                        model.Assign(i, _tile);
                        break;
                    }
                }
            }
        }
    }
}
