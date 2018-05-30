
/*
 * Notes
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RC3.WFC;

namespace RC3.Unity.SimpleTiling
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "RC3/Simple Tiling/TileSet")]
    public class TileSet : ScriptableObject
    {
        [SerializeField, HideInInspector] private Tile[] _tiles;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Tile this[int i]
        {
            get { return _tiles[i]; }
        }


        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return _tiles.Length; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TileMap CreateMap()
        {
            var map = new TileMap(_tiles.Length, _tiles[0].Degree);

            for(int i = 0; i < map.TileCount; i++)
            {
                var tile = _tiles[i];

                for(int j = 0; j < map.TileDegree; j++)
                    map.SetAdjacency(i, j, tile.GetAdjacent(j));
            }

            return map;
        }
    }
}
