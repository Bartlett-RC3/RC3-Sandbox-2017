
/*
 * Notes
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace RC3.WFC
{
    /// <summary>
    /// 
    /// </summary>
    public class TileMap
    {
        private int[][][] _tables;
        private bool[] _allow;
        private int _tileCount;


        /// <summary>
        /// The number of tiles in this map
        /// </summary>
        public int TileCount
        {
            get { return _tileCount; }
        }


        /// <summary>
        /// The number of neighbors for each tile in this map.
        /// </summary>
        public int TileDegree
        {
            get { return _tables.Length; }
        }


        /// <summary>
        /// 
        /// </summary>
        public TileMap(int tileCount, int tileDegree)
        {
            _tables = new int[tileDegree][][];
            _allow = new bool[tileCount];
            _tileCount = tileCount;

            for (int i = 0; i < _tables.Length; i++)
            {
                var table = _tables[i] = new int[_tileCount][];

                for (int j = 0; j < table.Length; j++)
                    table[j] = Array.Empty<int>();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void SetAdjacency(int tile, int direction, IEnumerable<int> neighbors)
        {
            // TODO 
            // validate adjacent indices
            // ensure no duplicates?

            _tables[direction][tile] = neighbors.ToArray();
        }


        /// <summary>
        /// Reduces the target domain based on allowed adjacencies of each tile in the source domain.
        /// Returns the number of variables removed from the target domain.
        /// </summary>
        internal int Reduce(int direction, bool[] source, bool[] target)
        {
            var table = _tables[direction];

            _allow.Clear();

            for (int i = 0; i < _tileCount; i++)
            {
                if (source[i])
                    _allow.Set(table[i], true);
            }

            int n = 0;

            for (int i = 0; i < _tileCount; i++)
            {
                if (target[i] && !_allow[i])
                {
                    target[i] = false;
                    n++;
                }
            }

            return n;
        }
    }
}
