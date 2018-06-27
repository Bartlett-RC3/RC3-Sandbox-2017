using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RC3.Unity.SDFDemo;

namespace RC3.Unity.ProceduralTexturing
{
    /// <summary>
    /// 
    /// </summary>
    public class AssignUVs : MonoBehaviour
    {
        [SerializeField] private ScalarField _u;
        [SerializeField] private ScalarField _v;

        private Mesh _mesh;
        private Vector3[] _positions;
        private Vector2[] _texCoords;


        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            _mesh = GetComponent<MeshFilter>().sharedMesh;
            _mesh.MarkDynamic();

            _positions = _mesh.vertices;
            _texCoords = new Vector2[_positions.Length];
        }


        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            _u.BeforeEvaluate();
            _v.BeforeEvaluate();

            for(int i = 0; i < _positions.Length; i++)
            {
                var p = _positions[i];
                _texCoords[i] = new Vector2(_u.Evaluate(p), _v.Evaluate(p));
            }

            _mesh.uv = _texCoords;
        }
    }
}
