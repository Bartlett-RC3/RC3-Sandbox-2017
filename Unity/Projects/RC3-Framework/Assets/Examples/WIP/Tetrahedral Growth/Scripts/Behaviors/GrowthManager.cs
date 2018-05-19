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

        // TODO
        // visualization
        // fill
        // track tetrahedra

        private HeMesh3d _mesh;
        private HashSet<int> _growthFaces = new HashSet<int>();
        private bool _run = true;


        /// <summary>
        /// 
        /// </summary>
        public bool Run
        {
            get { return _run; }
            set { _run = value; }
        }

        
        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            _mesh = _growthMesh.Value;
            ValidateMesh();

            var faces = _mesh.Faces;
            
            // add all faces to the list
            for(int i = 0; i < faces.Count; i++)
                _growthFaces.Add(i);
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
            if (_run)
            {
                if (TryGrow())
                    Fill();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private bool TryGrow()
        {
            if (_growthFaces.Count == 0)
                return false;

            if(!TryAppend(SelectFace()))
                TryGrow();

            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int SelectFace()
        {
            var faces = _mesh.Faces;
            return _growthFaces.SelectMin(i => _faceEvaluator.Evalutate(faces[i]));
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
            if (!CheckCollision(p, dist))
            {
                _growthFaces.Remove(faceIndex);
                return false;
            }

            // add new faces
            var nf = faces.Count;
            var v = _mesh.PokeFace(face);
            v.Position = p;

            // add index of new faces to the growth list
            _growthFaces.Add(nf);
            _growthFaces.Add(nf + 1);

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
        private bool CheckCollision(Vec3d point, double radius)
        {
            var verts = _mesh.Vertices;
            var radSqr = radius * radius;

            foreach(var v in verts)
            {
                if (v.IsUnused) continue;

                if (point.SquareDistanceTo(v.Position) < radSqr)
                    return false;
            }

            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        private void Fill()
        {
            // TODO
        }
    }
}
