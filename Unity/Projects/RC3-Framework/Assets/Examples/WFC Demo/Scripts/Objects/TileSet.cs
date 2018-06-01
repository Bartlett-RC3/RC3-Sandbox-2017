
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
    public abstract class TileSet : ScriptableObject
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
        public TileMap<string> CreateMap()
        {
            var map = CreateMap(Count);

            for (int i = 0; i < _tiles.Length; i++)
            {
                var labels = _tiles[i].Labels;

                for (int j = 0; j < labels.Length; j++)
                    map.SetLabel(j, i, labels[j]);
            }

            return map;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract TileMap<string> CreateMap(int tileCount);
    }
}
