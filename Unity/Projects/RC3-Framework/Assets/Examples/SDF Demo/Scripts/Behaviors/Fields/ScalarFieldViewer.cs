using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using UnityEngine;
using SpatialSlur.Core;

namespace RC3.Unity.SDFDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class ScalarFieldViewer : MonoBehaviour
    {
        [SerializeField] private ScalarField _source;
        [SerializeField] private int _countX = 32;
        [SerializeField] private int _countY = 32;
        [SerializeField] private int _countZ = 32;
        [SerializeField] private bool _center;
        
        private Mesh _mesh;
        private Vector3[] _positions;
        private Vector2[] _texCoords;
        private int _count;


        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            _count = _countX * _countY * _countZ;
            InitMesh();

            if(_center)
                transform.position -= new Vector3(_countX, _countY, _countZ) * 0.5f;
        }

        
        /// <summary>
        /// 
        /// </summary>
        private void InitMesh()
        {
            _mesh = GetComponent<MeshFilter>().sharedMesh = new Mesh();
            _mesh.MarkDynamic();
            _mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

            _positions = new Vector3[_count];
            _texCoords = new Vector2[_count];
            
            int i = 0;
            for (int z = 0; z < _countZ; z++)
            {
                for (int y = 0; y < _countY; y++)
                {
                    for (int x = 0; x < _countX; x++)
                    {
                        _positions[i++] = new Vector3(x, y, z);
                    }
                }
            }
            
            _mesh.vertices = _positions;
            _mesh.uv = _texCoords;
            
            _mesh.SetIndices(Enumerable.Range(0, _count).ToArray(), MeshTopology.Points, 0);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        private void Update()
        {
            var m = transform.localToWorldMatrix;
            _source.BeforeEvaluate();

            Parallel.ForEach(Partitioner.Create(0, _count), range =>
            {
                for(int i = range.Item1; i < range.Item2; i++)
                    _texCoords[i].x = _source.Evaluate(m.MultiplyPoint3x4(_positions[i]));
            });

            _mesh.uv = _texCoords;
        }
    }
}
