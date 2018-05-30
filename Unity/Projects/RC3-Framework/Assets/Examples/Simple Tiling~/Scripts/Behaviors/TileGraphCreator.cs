
/*
 * Notes
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RC3.Graphs;

namespace RC3.Unity.SimpleTiling
{
    /// <summary>
    /// 
    /// </summary>
    public class TileGraphCreator : MonoBehaviour
    {
        [SerializeField] SharedDigraph _tileGraph;
        [SerializeField] VertexObject _vertexPrefab;
        [SerializeField] private int _countX = 10;
        [SerializeField] private int _countY = 10;


        /// <summary>
        /// 
        /// </summary>
        void Awake()
        {
            _tileGraph.Initialize(Digraph.Factory.CreateRectangleGrid(_countX, _countY, true));
            _tileGraph.VertexObjects.AddRange(CreateVertexObjects());

            transform.position = new Vector3(_countX * -0.5f, 0.0f, _countY * -0.5f); // center
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<VertexObject> CreateVertexObjects()
        {
            int count = 0;
            for (int y = 0; y < _countY; y++)
            {
                for (int x = 0; x < _countX; x++)
                {
                    var vobj = Instantiate(_vertexPrefab, transform);
                    vobj.transform.localPosition = new Vector3(x, 0.0f, y);
                    vobj.Vertex = count++;
                    yield return vobj;
                }
            }
        }
    }
}
