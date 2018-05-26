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
    public class OctahedralEdgeGraphCreator : MonoBehaviour
    {
        [SerializeField] private SharedEdgeGraph _grid;
        [SerializeField] private VertexObject _vertexObject;
        [SerializeField] private EdgeObject _edgeObject;
        [SerializeField] private int _countX = 5;
        [SerializeField] private int _countY = 5;
        [SerializeField] private int _countZ = 5;


        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            _grid.Initialize(EdgeGraph.Factory.CreateTruncatedOctahedronGrid(_countX, _countY, _countZ));
            _grid.VertexObjects.AddRange(CreateVertexObjects());
            _grid.EdgeObjects.AddRange(CreateEdgeObjects());
        }


        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<VertexObject> CreateVertexObjects()
        {
            int index = 0;

            foreach (var p in GetVertexPositions())
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


        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<EdgeObject> CreateEdgeObjects()
        {
            var graph = _grid.Graph;
            var verts = _grid.VertexObjects;

            for (int i = 0; i < graph.EdgeCount; i++)
            {
                var e = graph.GetEdge(i);

                var p0 = verts[e.Start].transform.position;
                var p1 = verts[e.End].transform.position;

                var eObj = Instantiate(_edgeObject, transform);
                eObj.Index = i;

                var xform = eObj.transform;
                var dir = p1 - p0;
                var mag = dir.magnitude;

                // scale to edge length
                xform.localScale = new Vector3(0.1f, mag * 0.5f, 0.1f);

                // translate to edge mid point
                xform.localPosition = (p0 + p1) * 0.5f;

                // align up with edge direction
                xform.localRotation = Quaternion.FromToRotation(xform.up, dir);

                yield return eObj;
            }
        }
    }
}
