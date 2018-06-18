
/*
 * Notes
 * 
 * Tiles must be scaled by -1 in the x if modeled in a right hand coordinate system.
 */

using System.Collections.Generic;
using UnityEngine;
using RC3.Graphs;
using RC3.WFC;

namespace RC3.Unity.WFCDemo
{
    /// <summary>
    /// Creates a new graph based on tile labels
    /// </summary>
    public class TileGraphExtractor : MonoBehaviour
    {
        [SerializeField] private SharedDigraph _tileGraph;
        [SerializeField] private string[] _validLabels;

        private TileModel _model;
        private TileMap<string> _map;
        private HashSet<string> _labelSet;


        /// <summary>
        /// 
        /// </summary>
        private void Initialize()
        {
            var manager = GetComponent<TileModelManager>();

            _model = manager.TileModel;
            _map = manager.TileMap;
            _labelSet = new HashSet<string>(_validLabels);
        }


        /// <summary>
        /// 
        /// </summary>
        public Digraph Extract()
        {
            if (_model == null)
                Initialize();

            var g0 = _tileGraph.Graph;
            var g1 = new Digraph(g0.VertexCount);

            for (int i = 0; i < g0.VertexCount; i++)
            {
                var tile = _model.GetAssigned(i);
                var n = _map.TileDegree;

                for (int j = 0; j < n; j++)
                {
                    var label = _map.GetLabel(j, tile);

                    if (_labelSet.Contains(label))
                        g1.AddEdge(i, j);
                }
            }

            return g0;
        }
    }

}
