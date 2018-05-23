using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SpatialSlur.Core;
using SpatialSlur.Mesh;

namespace RC3.Unity.TetrahedralGrowth
{
    /// <summary>
    /// 
    /// </summary>
    public class GrowthManager : MonoBehaviour
    {
        [SerializeField] private SharedHeMesh3d _growthMesh;
        [SerializeField] private FaceEvaluator _faceEvaluator;
        [SerializeField] private ScalarField3d _offsetField;

        private HeMesh3d _mesh;
        private List<Tetra> _tetrahedra = new List<Tetra>();

        private HashSet<int> _growthFaces = new HashSet<int>();
        private Queue<int> _fillEdges = new Queue<int>();
        private double _fillAngle = Math.PI * 0.75;

        
        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            _mesh = _growthMesh.Value;
            ValidateMesh();

            var faces = _mesh.Faces;
            
            // add all faces to the growth list
            for(int i = 0; i < faces.Count; i++)
                _growthFaces.Add(i);
            
            var edges = _mesh.Edges;

            // add all edges to the fill queue
            for (int i = 0; i < edges.Count; i++)
                _fillEdges.Enqueue(i);

            // fill initial
            Fill();
        }


        /// <summary>
        /// 
        /// </summary>
        private void ValidateMesh()
        {
            foreach(var f in _mesh.Faces)
            {
                if (!f.IsDegree3)
                    throw new System.ArgumentException("The given mesh must have triangular faces.");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Grow();
                // Fill();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void LateUpdate()
        {
            DebugDisplaySurface();
            DebugDisplayVolume();
        }


        /// <summary>
        /// 
        /// </summary>
        private void DebugDisplaySurface()
        {
            var color = Color.white;

            foreach(var he in _mesh.Edges)
            {
                var p0 = (Vector3)he.Start.Position;
                var p1 = (Vector3)he.End.Position;
                Debug.DrawLine(p0, p1, color);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void DebugDisplayVolume()
        {
            var color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
            var verts = _mesh.Vertices;

            foreach (var tetra in _tetrahedra)
            {
                var p0 = (Vector3)verts[tetra.Vertex0].Position;
                var p1 = (Vector3)verts[tetra.Vertex1].Position;
                var p2 = (Vector3)verts[tetra.Vertex2].Position;
                var p3 = (Vector3)verts[tetra.Vertex3].Position;

                Debug.DrawLine(p0, p1, color);
                Debug.DrawLine(p0, p2, color);
                Debug.DrawLine(p0, p3, color);

                Debug.DrawLine(p1, p2, color);
                Debug.DrawLine(p1, p3, color);

                Debug.DrawLine(p2, p3, color);
            }
        }


        #region Grow

        /// <summary>
        /// 
        /// </summary>
        private void Grow()
        {
            if (_growthFaces.Count == 0)
                return;

            if (!TryAppend(SelectFace()))
                Grow();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int SelectFace()
        {
            var faces = _mesh.Faces;

            return _growthFaces.SelectMin(fi =>
            {
                var f = faces[fi];
                return _faceEvaluator.Evalutate(f);
            });
        }


        /// <summary>
        /// 
        /// </summary>
        private bool TryAppend(int faceIndex)
        {
            var faces = _mesh.Faces;
            var face = faces[faceIndex];

            double dist;
            var p = GetFacePoint(face, out dist);

            // check point for collisions
            if (IsCollision(p, dist))
            {
                _growthFaces.Remove(faceIndex);
                return false;
            }

            // add index of new faces to the growth list
            _growthFaces.Add(faces.Count);
            _growthFaces.Add(faces.Count + 1);

            // add modified edges to the fill queue
            foreach (var he in face.Halfedges)
                _fillEdges.Enqueue(he >> 1);

            // add new faces
            var v = _mesh.PokeFace(face);
            v.Position = p;

            // cache tetrahedron
            // AddTetrahedron(face, v);

            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        private Vec3d GetFacePoint(HeMesh3d.Face face, out double distance)
        {
            var he = face.First;

            // get positions of face vertices
            var p0 = he.Start.Position;
            he = he.Next;
            var p1 = he.Start.Position;
            he = he.Next;
            var p2 = he.Start.Position;

            // get centroid of face
            var p = (p0 + p1 + p2) / 3.0;

            // get normal of face
            var n = Vec3d.Cross(p1 - p0, p2 - p1);

            // scale the normal
            distance = _offsetField.Evaluate(p);
            n *= distance / n.Length;

            return p + n;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="face"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private bool IsCollision(Vec3d point, double radius)
        {
            var verts = _mesh.Vertices;
            var radSqr = radius * radius;

            foreach(var v in verts)
            {
                if (v.IsUnused) continue;

                if (point.SquareDistanceTo(v.Position) < radSqr)
                    return true;
            }

            return false;
        }

        #endregion


        #region Fill

        /// <summary>
        /// 
        /// </summary>
        private void Fill()
        {
            var edges = _mesh.Edges;

            while(_fillEdges.Count > 0)
            {
                var he0 = edges[_fillEdges.Dequeue()];
                var he1 = he0.Twin;

                // TODO check if tip is the same
                if(he0.Previous.Start == he1.Previous.Start)
                {
                    // TODO handle degen case
                    continue;
                }
                
                var a = he0.GetDihedralAngle(f => f.GetNormal());
                
                if (a < _fillAngle)
                {
                    // enqueue fill edges
                    _fillEdges.Enqueue(he0.Previous >> 1);
                    _fillEdges.Enqueue(he0.Next >> 1);

                    _fillEdges.Enqueue(he1.Previous >> 1);
                    _fillEdges.Enqueue(he1.Next >> 1);

                    // cache tetrahedron
                    // AddTetrahedron(he0);

                    _mesh.SpinEdge(he0);
                }
            }
        }
        
        #endregion

        

        /// <summary>
        /// 
        /// </summary>
        private void AddTetrahedronGrow(HeMesh3d.Face face, HeMesh3d.Vertex vertex)
        {
            var he = face.First;

            var v0 = he.Start;
            he = he.Next;
            var v1 = he.Start;
            he = he.Next;
            var v2 = he.Start;

            _tetrahedra.Add(new Tetra(vertex, v2, v1, v0));
        }


        /// <summary>
        /// 
        /// </summary>
        private void AddTetrahedron(HeMesh3d.Halfedge hedge)
        {
            var v3 = hedge.Twin.Previous.Start;

            var v0 = hedge.Start;
            hedge = hedge.Next;
            var v1 = hedge.Start;
            hedge = hedge.Next;
            var v2 = hedge.Start;

            _tetrahedra.Add(new Tetra(v3, v2, v1, v0));
        }
    }
}
