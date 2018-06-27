
/*
 * Notes
 */

using System;
using System.Linq;

namespace RC3.WFC
{
    /// <summary>
    /// 
    /// </summary>
    public class RandomTileSelector : ITileSelector
    {
        private Random _random;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public RandomTileSelector(int seed)
        {
            _random = new Random(seed);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public int Select(TileModel model, int position)
        {
            var d = model.GetDomain(position);
            return d.ElementAt(_random.Next(d.Count));
        }
    }
}
