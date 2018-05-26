using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Notes 
 */

namespace RC3.Unity.Examples.DendriticGrowth
{
    /// <summary>
    /// 
    /// </summary>
    public class OctahedralGraphCreator : MonoBehaviour
    {
        [SerializeField] private SharedGraph _grid;
        [SerializeField] private VertexObject _vertexObject;
        [SerializeField] private int _countX = 5;
        [SerializeField] private int _countY = 5;
        [SerializeField] private int _countZ = 5;
        

        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            _grid.Initialize(Graph.Factory.CreateTruncatedOctahedronGrid(_countX, _countY, _countZ));
            _grid.VertexObjects.AddRange(CreateVertexObjects());
        }


        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<VertexObject> CreateVertexObjects()
        {
            int index = 0;

            foreach(var p in GetVertexPositions())
            {
                var vObj = Instantiate(_vertexObject, transform);
                vObj.transform.localPosition = p;
                vObj.Index = index++;
                yield return vObj;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Vector3> GetVertexPositions()
        {
            // primal vertices
            for (int z = 0; z < _countZ; z++)
            {
                for (int y = 0; y < _countY; y++)
                {
                    for (int x = 0; x < _countX; x++)
                        yield return new Vector3(x, y, z);
                }
            }

            // dual vertices
            for (int z = 0; z < _countZ; z++)
            {
                for (int y = 0; y < _countY; y++)
                {
                    for (int x = 0; x < _countX; x++)
                        yield return new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
                }
            }
        }
    }
}
