
/*
 * Notes
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace RC3.Unity.SimpleTiling
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "RC3/Simple Tiling/Tile")]
    public class Tile : ScriptableObject
    {
        private static readonly char[] _separators = {','};

        [SerializeField] private Mesh _mesh;
        [SerializeField] private Material _material;
        [SerializeField, HideInInspector] private string[] _adjacency;

        private int[][] _adj;


        /// <summary>
        /// 
        /// </summary>
        private void OnEnable()
        {
            _adj = new int[_adjacency.Length][];

            for(int i = 0; i < _adjacency.Length; i++)
            {
                var ids = _adjacency[i].Split(_separators, StringSplitOptions.RemoveEmptyEntries);
                _adj[i] = ids.Select(s => int.Parse(s)).ToArray();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public Mesh Mesh
        {
            get { return _mesh; }
        }


        /// <summary>
        /// 
        /// </summary>
        public Material Material
        {
            get { return _material; }
        }


        /// <summary>
        /// 
        /// </summary>
        public int Degree
        {
            get { return _adj.Length; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public IEnumerable<int> GetAdjacent(int direction)
        {
            return _adj[direction];
        }
    }
}
