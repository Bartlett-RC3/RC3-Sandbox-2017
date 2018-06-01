
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
    [CreateAssetMenu(menuName = "RC3/WFC Demo/TileSets/Rectangular")]
    public class RectangularTileSet : TileSet
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override TileMap<string> CreateMap(int tileCount)
        {
            return TileMap<string>.CreateQuadrilateral(Count);
        }
    }
}
