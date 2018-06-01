
/*
 * Notes
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RC3.WFC;

namespace RC3.Unity.LabeledTiling
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "RC3/Labeled Tiling/QuadTileset")]
    public class QuadTileSet : TileSet
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override TileSet<string> InitTileSet()
        {
            return TileSet<string>.CreateQuadrilateral(Count);
        }
    }
}
