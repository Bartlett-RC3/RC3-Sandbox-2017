using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

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
        private double _fillAngle = Math.PI * 2.0 / 3.0;


        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            _mesh = _growthMesh.Value;
            ValidateMesh();

            var faces = _mesh.Faces;
            var edges = _mesh.Edges;

            // add all faces to the growth list
            for (int i = 0; i < faces.Count; i++)
                _growthFaces.Add(i);
            
            // add all edges to the fill queue
            for (int i = 0; i < edges.Count; i++)
                _fillEdges.Enqueue(i);
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
                if (!Fill()) Grow();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void AddTetrahedron(HeMesh3d.Face face, int vertex)
        {
            var he = face.First;

            var v0 = he.Start;
            he = he.Next;
            var v1 = he.Start;
            he = he.Next;
            var v2 = he.Start;

            _tetrahedra.Add(new Tetra(vertex, v2, v1, v0));
        }


        #region Grow

        /// <summary>
        /// 
        /// </summary>
        private bool Grow()
        {
            if (_growthFaces.Count == 0)
                return false;

            if (!TryGrowAt(SelectFace()))
                return Grow();

            return true;
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
                return f.IsUnused? double.MaxValue : _faceEvaluator.Evalutate(f);
            });
        }


        /// <summary>
        /// 
        /// </summary>
        private bool TryGrowAt(int faceIndex)
        {
            var faces = _mesh.Faces;
            var face = faces[faceIndex];

            // face may have been deleted from the growth mesh
            if (face.IsUnused)
            {
                _growthFaces.Remove(faceIndex);
                return false;
            }

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

            // cache tetrahedron
            AddTetrahedron(face, _mesh.Vertices.Count);

            // modify growth mesh
            var v = _mesh.PokeFace(face);
            v.Position = p;

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
                //if (v.IsUnused) continue; // skipping unused verts causes non-manifold growth on occasion

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
        private bool Fill()
        {
            if (_fillEdges.Count == 0)
                return false;

            if (!TryFillAt(_fillEdges.Dequeue()))
                return Fill();

            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        private bool TryFillAt(int edgeIndex)
        {
            var edges = _mesh.Edges;
            var he0 = edges[edgeIndex];

            // check if edge is still valid
            if (he0.IsUnused)
                return false;

            var a = he0.GetDihedralAngle(f => f.GetNormal());

            // check dihedral angle
            if (a >= _fillAngle)
                return false;

            var he1 = he0.Twin;

            // handle degen case
            if (he0.Previous.Start.IsConnectedTo(he1.Previous.Start))
            {
                // TODO enqueue new fill edges and growth faces

                // cache tetrahedron
                AddTetrahedron(he0.Face, he1.Previous.Start);

                // modify growth mesh
                _mesh.CollapseEdge(he0);
            }
            else
            {
                // enqueue fill edges
                _fillEdges.Enqueue(he0.Previous >> 1);
                _fillEdges.Enqueue(he0.Next >> 1);

                _fillEdges.Enqueue(he1.Previous >> 1);
                _fillEdges.Enqueue(he1.Next >> 1);

                // cache tetrahedron
                AddTetrahedron(he0.Face, he1.Previous.Start);

                // modify growth mesh
                _mesh.SpinEdge(he0);
            }

            return true;
        }

        #endregion


        #region Debug display

        /// <summary>
        /// 
        /// </summary>
        public void DebugDisplayTetrahedra()
        {
            const float scale0 = 0.1f;
            const float scale1 = 0.3f;
            const float invPeriod = 1.0f / 3.0f; // period of pulse in seconds
            const float twoPi = Mathf.PI * 2.0f;

            var verts = _mesh.Vertices;

            Color c0 = new Color(0.2f, 0.2f, 0.2f, 1.0f);
            Color c1 = new Color(0.9f, 0.9f, 0.9f, 1.0f);
            
            float scale = Mathf.Lerp(scale0, scale1, Mathf.Cos(Time.time * invPeriod * twoPi) * 0.5f + 0.5f);
            float ti = 1.0f / (_tetrahedra.Count - 1);

            GL.PushMatrix();
            GL.MultMatrix(transform.localToWorldMatrix);

            // draw faces
            GL.Begin(GL.TRIANGLES);
            {
                for(int i = 0; i < _tetrahedra.Count; i++)
                {
                    var tetra = _tetrahedra[i];
                    
                    var p0 = (Vector3)verts[tetra.Vertex0].Position;
                    var p1 = (Vector3)verts[tetra.Vertex1].Position;
                    var p2 = (Vector3)verts[tetra.Vertex2].Position;
                    var p3 = (Vector3)verts[tetra.Vertex3].Position;

                    var cen = (p0 + p1 + p2 + p3) * 0.25f;
                    GL.Color(Color.Lerp(c0, c1, i * ti));
                    
                    p0 = Vector3.Lerp(p0, cen, scale);
                    p1 = Vector3.Lerp(p1, cen, scale);
                    p2 = Vector3.Lerp(p2, cen, scale);
                    p3 = Vector3.Lerp(p3, cen, scale);

                    GL.Vertex(p1);
                    GL.Vertex(p2);
                    GL.Vertex(p3);

                    GL.Vertex(p0);
                    GL.Vertex(p1);
                    GL.Vertex(p3);

                    GL.Vertex(p0);
                    GL.Vertex(p2);
                    GL.Vertex(p1);

                    GL.Vertex(p0);
                    GL.Vertex(p3);
                    GL.Vertex(p2);
                }
            }
            GL.End();

            // draw edges
            GL.Begin(GL.LINES);
            {
                GL.Color(Color.white);

                for (int i = 0; i < _tetrahedra.Count; i++)
                {
                    var tetra = _tetrahedra[i];

                    var p0 = (Vector3)verts[tetra.Vertex0].Position;
                    var p1 = (Vector3)verts[tetra.Vertex1].Position;
                    var p2 = (Vector3)verts[tetra.Vertex2].Position;
                    var p3 = (Vector3)verts[tetra.Vertex3].Position;
                    
                    GL.Vertex(p0);
                    GL.Vertex(p1);

                    GL.Vertex(p0);
                    GL.Vertex(p2);
                
                    GL.Vertex(p0);
                    GL.Vertex(p3);

                    GL.Vertex(p1);
                    GL.Vertex(p2);

                    GL.Vertex(p1);
                    GL.Vertex(p3);

                    GL.Vertex(p2);
                    GL.Vertex(p3);
                }
            }
            GL.End();

            GL.PopMatrix();
        }

        #endregion


        #region Export

        [SerializeField] private string _exportPath;


        /// <summary>
        /// 
        /// </summary>
        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.E))
                ExportTetrahedra();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private void ExportTetrahedra()
        {
            var tetraPts = GetTetrahedraCoords();
            CoreIO.SerializeBinary(tetraPts, _exportPath);
            Debug.Log($"Tetrahedra exported to {_exportPath}");
        }


        /*
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<Vec3d> GetTetrahedraPoints()
        {
            List<Vec3d> result = new List<Vec3d>();

            var verts = _mesh.Vertices;
            
            foreach(var tetra in _tetrahedra)
            {
                var p0 = verts[tetra.Vertex0].Position;
                var p1 = verts[tetra.Vertex0].Position;
                var p2 = verts[tetra.Vertex0].Position;
                var p3 = verts[tetra.Vertex0].Position;

                result.Add(p0);
                result.Add(p1);
                result.Add(p2);
                result.Add(p3);
            }

            return result;
        }
        */

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<double> GetTetrahedraCoords()
        {
            List<double> result = new List<double>();

            var verts = _mesh.Vertices;

            foreach (var tetra in _tetrahedra)
            {
                var p0 = verts[tetra.Vertex0].Position;
                var p1 = verts[tetra.Vertex1].Position;
                var p2 = verts[tetra.Vertex2].Position;
                var p3 = verts[tetra.Vertex3].Position;

                result.Add(p0.X);
                result.Add(p0.Y);
                result.Add(p0.Z);

                result.Add(p1.X);
                result.Add(p1.Y);
                result.Add(p1.Z);

                result.Add(p2.X);
                result.Add(p2.Y);
                result.Add(p2.Z);

                result.Add(p3.X);
                result.Add(p3.Y);
                result.Add(p3.Z);
            }

            return result;
        }

        #endregion
    }
}
