
/*
 * Notes
 */

using System;

namespace RC3.WFC
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITileSelector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        int Select(TileModel model, int position);
    }
}
