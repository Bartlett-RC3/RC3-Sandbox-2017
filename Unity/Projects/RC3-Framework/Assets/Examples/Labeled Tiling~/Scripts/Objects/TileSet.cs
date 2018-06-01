
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
    public abstract class TileSet : ScriptableObject
    {
        [SerializeField, HideInInspector] private Tile[] _tiles;
        private TileSet<string> _tileSet;


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
        private void OnEnable()
        {
            // skip if tiles have not beed assigned yet
            if (_tiles == null)
                return;

            _tileSet = InitTileSet();

            for (int i = 0; i < _tiles.Length; i++)
            {
                var labels = _tiles[i].Labels;

                for (int j = 0; j < labels.Length; j++)
                    _tileSet.SetLabel(i, j, labels[j]);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract TileSet<string> InitTileSet();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TileMap CreateMap()
        {
            return _tileSet.CreateMap();
        }
    }
}
