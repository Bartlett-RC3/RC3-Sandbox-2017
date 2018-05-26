
/*
 * Notes
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RC3.WFC;

namespace RC3.Unity.Examples.LabeledTiling
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "RC3/Examples/Labeled Tiling/QuadTileset")]
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
