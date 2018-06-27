
/*
 * Notes
 */

using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

using SpatialSlur.Core;
using RC3.WFC;

namespace RC3.Unity.WFCDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class ProbabilisticTileSelector : MonoBehaviour, ITileSelector
    {
        [SerializeField] private TileSet _tileSet;
        [SerializeField] private double[] _weights;
        [SerializeField] private int _seed;

        private ProbabilitySelector _selector;


        /// <summary>
        /// 
        /// </summary>
        private void Awake()
        {
            if (_weights.Length != _tileSet.Count)
                throw new ArgumentException("The number of weights provided must match the size of the tile set.");

            _selector = new ProbabilitySelector(_weights, new System.Random(_seed));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public int Select(TileModel model, int position)
        {
            var d = model.GetDomain(position);
            _selector.SetWeights(GetModifiedWeights(d)); // update the weights in the selector
            return _selector.Next();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        private IEnumerable<double> GetModifiedWeights(ReadOnlySet<int> domain)
        {
            for(int i =0; i <_weights.Length; i++)
                yield return domain.Contains(i) ? _weights[i] : 0.0;
        }
    }
}
