using System;
using UnityEngine;
using SpatialSlur.Core;
using SpatialSlur.Mesh;

namespace RC3.Unity.TetrahedralGrowth
{
    using Mesh = UnityEngine.Mesh;

    /// <summary>
    /// 
    /// </summary>
    public class HeMesh3dCreator : MonoBehaviour
    {
        [SerializeField] private SharedHeMesh3d _source;


        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            var mesh = GetComponent<MeshFilter>().sharedMesh;
            _source.Value = ToHeMesh(mesh);

            Debug.Log($"{mesh.vertexCount} vertices");
            Debug.Log($"{mesh.GetIndexCount(0) / 3} triangles");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        private static HeMesh3d ToHeMesh(Mesh mesh)
        {
            var hem = new HeMesh3d();

            var positions = mesh.vertices;

            for (int i = 0; i < positions.Length; i++)
                hem.AddVertex().Position = positions[i];

            var nsub = mesh.subMeshCount;

            for (int i = 0; i < nsub; i++)
            {
                var stride = GetIndexStride(mesh.GetTopology(i));
                var indices = mesh.GetIndices(i);

                for (int j = 0; j < indices.Length; j += stride)
                    hem.AddFace(indices.TakeRange(j, stride));
            }

            return hem;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="topology"></param>
        /// <returns></returns>
        private static int GetIndexStride(MeshTopology topology)
        {
            switch (topology)
            {
                case MeshTopology.Triangles:
                    return 3;
                case MeshTopology.Quads:
                    return 4;
            }

            throw new System.NotSupportedException();
        }
    }
}
