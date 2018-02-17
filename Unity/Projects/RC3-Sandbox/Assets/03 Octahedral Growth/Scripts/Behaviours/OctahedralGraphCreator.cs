using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Notes 
 */

namespace RC3.Unity
{
    /// <summary>
    /// 
    /// </summary>
    public class OctahedralGraphCreator : MonoBehaviour
    {
        [SerializeField] private SharedGraph _graph;
        [SerializeField] private VertexObject _vertexObject;
        [SerializeField] private int _countX = 5;
        [SerializeField] private int _countY = 5;
        [SerializeField] private int _countZ = 5;
        

        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            _graph.Initialize(Graph.Factory.CreateTruncatedOctahedronGrid(_countX, _countY, _countZ));
            _graph.VertexObjects.AddRange(CreateVertexObjects());
        }


        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<VertexObject> CreateVertexObjects()
        {
            int index = 0;

            // primal vertices
            for (int z = 0; z < _countZ; z++)
            {
                for (int y = 0; y < _countY; y++)
                {
                    for (int x = 0; x < _countX; x++)
                    {
                        var vObj = Instantiate(_vertexObject, transform);
                        vObj.transform.localPosition = new Vector3(x, y, z);
                        vObj.Index = index++;
                        yield return vObj;
                    }
                }
            }

            // dual vertices
            for (int z = 0; z < _countZ; z++)
            {
                for (int y = 0; y < _countY; y++)
                {
                    for (int x = 0; x < _countX; x++)
                    {
                        var vObj = Instantiate(_vertexObject, transform);
                        vObj.transform.localPosition = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
                        vObj.Index = index++;
                        yield return vObj;
                    }
                }
            }
        }
    }
}
