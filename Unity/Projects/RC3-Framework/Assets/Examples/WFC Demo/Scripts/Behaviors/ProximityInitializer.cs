using System.Collections;
using System.Collections.Generic;
using RC3.WFC;
using RC3.Unity.SDFDemo;
using UnityEngine;

namespace RC3.Unity.WFCDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class ProximityInitializer : TileModelInitializer
    {
        [SerializeField] private SharedDigraph _tileGraph;
        [SerializeField] private ScalarField _distanceField;
        [SerializeField] private float _maxDistance = 0.0f;
        [SerializeField] private int[] _tiles;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public override void Initialize(TileModel model)
        {
            // assign a tile for all positions that are within some distance of our SDF object

            var verts = _tileGraph.VertexObjects;
            _distanceField.BeforeEvaluate(); // call before evaluating the distance field

            for(int i = 0; i < verts.Count; i++)
            {
                var p = verts[i].transform.position;
                var d = _distanceField.Evaluate(p);

                if (d < _maxDistance)
                    model.SetDomain(i, _tiles);
            }
        }
    }

}
