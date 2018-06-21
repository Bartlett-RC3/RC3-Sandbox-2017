
/*
 * Notes
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RC3.Graphs;

namespace RC3.Unity.WFCDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class RectangularGraphCreator : InitializableBehavior
    {
        [SerializeField] SharedDigraph _tileGraph;
        [SerializeField] VertexObject _vertexPrefab;
        [SerializeField] private int _countX = 10;
        [SerializeField] private int _countY = 10;


        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            _tileGraph.Initialize(Digraph.Factory.CreateRectangleGrid(_countX, _countY, true));
            _tileGraph.VertexObjects.AddRange(CreateVertexObjects());

            transform.localPosition = new Vector3(_countX - 1, _countY - 1, 0.0f) * -0.5f; // center
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
                    vobj.transform.localPosition = new Vector3(x, y, 0.0f);
                    vobj.Vertex = count++;
                    yield return vobj;
                }
            }
        }
    }
}
