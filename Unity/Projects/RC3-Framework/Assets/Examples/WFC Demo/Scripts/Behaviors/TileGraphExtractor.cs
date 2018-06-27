
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
        public EdgeGraph ExtractEdgeGraph()
        {
            var result = new EdgeGraph();
            ExtractTo(result);

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        public Graph ExtractGraph()
        {
            var result = new Graph();
            ExtractTo(result);

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        private void ExtractTo(IGraph target)
        {
            if (_model == null)
                Initialize();

            var source = _tileGraph.Graph;
            var n = _map.TileDegree;

            for (int v0 = 0; v0 < source.VertexCount; v0++)
            {
                var tile = _model.GetAssigned(v0);

                for (int i = 0; i < n; i++)
                {
                    var label = _map.GetLabel(i, tile);

                    if (_labelSet.Contains(label))
                    {
                        var v1 = source.GetVertexNeighborOut(v0, i);

                        // avoids multi-edges and self loops
                        if (v1 > v0)
                            target.AddEdge(v0, v1);
                    }
                }
            }
        }
    }

}
