
/*
 * Notes
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RC3.WFC;

namespace RC3.Unity.WFCDemo
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "RC3/WFC Demo/TileSets/TruncatedOctahedral")]
    public class TruncatedOctahedralTileSet : TileSet
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override TileMap<string> CreateMap(int tileCount)
        {
            return TileMap<string>.CreateTruncatedOctahedral(tileCount);
        }
    }
}
