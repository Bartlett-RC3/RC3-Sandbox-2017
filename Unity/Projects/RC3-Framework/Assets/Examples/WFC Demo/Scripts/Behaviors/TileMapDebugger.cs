using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC3.Unity.WFCDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class TileMapDebugger : MonoBehaviour
    {
        [SerializeField] private SharedDigraph _tileGraph;
        [SerializeField] private RectangularTileSet _tileSet;


        /// <summary>
        /// 
        /// </summary>
        void Start()
        {
            var map = _tileSet.CreateMap();
           
            // TODO
            // map is creating correct adjacency below
            // bug must be in model
            /*
            {
                //var tile = 2;

                for (int j = 0; j < map.TileCount; j++)
                {
                    for (int i = 0; i < map.TileDegree; i++)
                    {
                        var c = constraints[i];
                        var neighb = string.Concat(c.GetDebug(j).Select(x => $"{x}, "));
                        Debug.Log($"\nValid neighbors for tile {j} in direction {i} = ({neighb})");
                    }
                }

                Debug.Log("");
            }
            */
        }


        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            var graph = _tileGraph.Graph;
            var vertObjs = _tileGraph.VertexObjects;

            int v0 = 35;
            int v1 = graph.GetVertexNeighborOut(v0, 0);
            int v2 = graph.GetVertexNeighborOut(v0, 2);

            var p0 = vertObjs[v0].transform.position;
            var p1 = vertObjs[v1].transform.position;
            var p2 = vertObjs[v2].transform.position;

            Debug.DrawLine(p0, p1, Color.white);
            Debug.DrawLine(p0, p2, Color.white);
        }

    }
}
