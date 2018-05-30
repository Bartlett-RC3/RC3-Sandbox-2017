using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RC3.Graphs;

/*
 * Notes 
 */

namespace RC3.Unity.TerritorialAgents
{
    /// <summary>
    /// 
    /// </summary>
    public class EdgeGraphCreator : MonoBehaviour
    {
        [SerializeField] private SharedEdgeGraph _sharedGraph;


        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            _sharedGraph.Initialize(new EdgeGraph());
        }

        
        /*
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
                vObj.Vertex = index++;
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
            var graph = _sharedGraph.Graph;
            var verts = _sharedGraph.VertexObjects;

            for (int i = 0; i < graph.EdgeCount; i++)
            {
                var v0 = graph.GetStartVertex(i);
                var v1 = graph.GetEndVertex(i);

                var p0 = verts[v0].transform.localPosition;
                var p1 = verts[v1].transform.localPosition;

                var eObj = Instantiate(_edgeObject, transform);
                eObj.Edge = i;

                var xform = eObj.transform;
                var dir = p1 - p0;
                var mag = dir.magnitude;

                // scale to edge length
                xform.localScale = new Vector3(1.0f, mag * 0.5f, 1.0f);

                // align up with edge direction
                xform.localRotation = Quaternion.FromToRotation(xform.up, dir);

                // translate to edge mid point
                xform.localPosition = (p0 + p1) * 0.5f;

                yield return eObj;
            }
        }
        */
    }
}
