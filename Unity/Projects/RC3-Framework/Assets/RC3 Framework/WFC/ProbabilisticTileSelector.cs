
/*
 * Notes
 */

using System;
using System.Linq;
using System.Collections.Generic;

using SpatialSlur.Core;

namespace RC3.WFC
{
    /// <summary>
    /// 
    /// </summary>
    public class ProbabilisticTileSelector : TileSelector
    {
        private ProbabilitySelector _selector;
        private double[] _weights;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public ProbabilisticTileSelector(TileModel model, IEnumerable<double> weights, int seed = 1) : base(model)
        {
            _weights = weights.ToArray();

            if (_weights.Length != model.TileCount)
                throw new ArgumentException("The number of weights provided must match the number of tiles in the model.");

            _selector = new ProbabilitySelector(_weights, new Random(seed));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override int Select(int position)
        {
            var d = _model.GetDomain(position);
            _selector.SetWeights(GetModifiedWeights(d)); // update the weights in the selector
            return d.ElementAt(_selector.Next());
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
