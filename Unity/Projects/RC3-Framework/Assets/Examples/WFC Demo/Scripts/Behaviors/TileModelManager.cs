
/*
 * Notes
 * 
 * Tiles must be scaled by -1 in the x if modeled in a right hand coordinate system.
 */

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RC3.Graphs;
using RC3.WFC;

namespace RC3.Unity.WFCDemo
{


    /// <summary>
    /// 
    /// </summary>
    public class TileModelManager : InitializableBehavior
    {
        [SerializeField] private SharedDigraph _tileGraph;
        [SerializeField] private TileSet _tileSet;
        [SerializeField] private int _substeps = 10;
        [SerializeField] private int _seed = 1;

        private Digraph _graph;
        private List<VertexObject> _verts;

        private TileModel _model;
        private TileMap<string> _map;
        private CollapseStatus _status;

        private TileModelInitializer _initializer;


        /// <summary>
        /// 
        /// </summary>
        public TileModel TileModel
        {
            get { return _model; }
        }


        /// <summary>
        /// 
        /// </summary>
        public TileMap<string> TileMap
        {
            get { return _map; }
        }


        /// <summary>
        /// 
        /// </summary>
        public override void Initialize()
        {
            _graph = _tileGraph.Graph;
            _verts = _tileGraph.VertexObjects;

            _map = _tileSet.CreateMap();
            _model = TileModel.CreateFromGraph(_map, _graph, _seed);

            _model.DomainChanged += OnDomainChanged;
            _status = CollapseStatus.Incomplete;

            _initializer = GetComponent<TileModelInitializer>();
            _initializer?.Initialize(_model);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="domain"></param>
        private void OnDomainChanged(int position, ReadOnlySet<int> domain)
        {
            var v = _verts[position];
            int n = domain.Count;
            
            switch(n)
            {
                case 0:
                    v.Collapse();
                    break;
                case 1:
                    v.Tile = _tileSet[domain.First()];
                    break;
                default:
                    v.Reduce((float)n / _map.TileCount);
                    break;
            }
        }

      
        /// <summary>
        /// 
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                ResetModel();
            
            if (_status == CollapseStatus.Incomplete)
            {
                for (int i = 0; i < _substeps; i++)
                {
                    _status = _model.Step();

                    if (_status == CollapseStatus.Contradiction)
                    {
                        Debug.Log("Contradiction found! Reset the model and try again.");
                        return;
                    }
                    else if (_status == CollapseStatus.Complete)
                    {
                        Debug.Log("Collapse complete!");
                        return;
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void ResetModel()
        {
            _model.ResetAllDomains();
            _status = CollapseStatus.Incomplete;

            foreach (var v in _verts)
                v.Tile = null;

            _initializer?.Initialize(_model);
        }
    }

}
