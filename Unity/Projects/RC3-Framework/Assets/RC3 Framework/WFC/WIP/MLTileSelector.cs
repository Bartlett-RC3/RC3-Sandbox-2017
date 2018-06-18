
/*
 * Notes
 */

using System;
using System.Linq;
using System.Collections.Generic;

using SpatialSlur.Core;

using System;

namespace RC3.WFC
{
    /// <summary>
    /// 
    /// </summary>
    public class MLTileSelector : TileSelector
    {
        private MLSelectorAgent _agent;
        private ProbabilitySelector _selector;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public MLTileSelector(TileModel model, MLSelectorAgent agent, int seed) : base(model)
        {
            if (agent == null)
                throw new ArgumentNullException();

            _agent = agent;
            _selector = new ProbabilitySelector(DefaultWeights, new Random(seed));
        }


        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<double> DefaultWeights
        {
            get
            {
                for (int i = 0; i < _model.TileCount; i++)
                    yield return 1.0;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override int Select(int position)
        {
            _selector.SetWeights(_agent.GetWeights(position));

            var d = _model.GetDomain(position);
            return d.ElementAt(_selector.Next());
        }
    }
}
