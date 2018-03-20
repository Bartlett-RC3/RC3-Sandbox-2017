using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC3.WFC
{
    /// <summary>
    /// 
    /// </summary>
    public class TileSet<T>
    {
        #region Static

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tileCount"></param>
        /// <returns></returns>
        public static TileSet<T> CreateQuadrilateral(int tileCount)
        {
            const int n = 4;
            var tileSet = new TileSet<T>(tileCount, 4);
            
            for (int i = 0; i < n; i += 2)
                tileSet.MakeOpposites(i, i + 1);

            return tileSet;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tileCount"></param>
        /// <returns></returns>
        public static TileSet<T> CreateHexagonal(int tileCount)
        {
            const int n = 6;
            var tileSet = new TileSet<T>(tileCount, n);

            for (int i = 0; i < n; i += 2)
                tileSet.MakeOpposites(i, i + 1);

            return tileSet;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tileCount"></param>
        /// <returns></returns>
        public static TileSet<T> CreateTruncatedOctahedral(int tileCount)
        {
            const int n = 14;
            var tileSet = new TileSet<T>(tileCount, n);

            for(int i = 0; i < n; i+=2)
                tileSet.MakeOpposites(i, i + 1);

            return tileSet;
        }

        #endregion

    
        private T[][] _labels; // labels [direction][tile]
        private int[] _opposites; // the opposite direction for each direction
        private int _tileCount;


        /// <summary>
        /// The number of tiles in this set
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
            get { return _labels.Length; }
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tileCount"></param>
        private TileSet(int tileCount, int tileDegree)
        {
            _labels = new T[tileDegree][];
            _opposites = new int[tileDegree];
            _tileCount = tileCount;

            for (int i = 0; i < _labels.Length; i++)
                _labels[i] = new T[tileCount];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        public int GetOpposite(int direction)
        {
            return _opposites[direction];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="other"></param>
        private void MakeOpposites(int direction0, int direction1)
        {
            _opposites[direction0] = direction1;
            _opposites[direction1] = direction0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public T GetLabel(int tile, int direction)
        {
            return _labels[direction][tile];
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="labels"></param>
        public void SetLabel(int tile, int direction, T label)
        {
            _labels[direction][tile] = label;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TileMap CreateMap()
        {
            var map = new TileMap(_tileCount, _opposites.Length);
            
            // create a lookup table for the opposite of each direction
            for(int i = 0; i < _labels.Length; i++)
            {
                var lookup = CreateLookup(_opposites[i]);
                var labels = _labels[i];

                // set adjacency for each tile
                for (int j = 0; j < _tileCount; j++)
                    map.SetAdjacency(j, i, lookup[labels[j]]);
            }

            return map;
        }


        /// <summary>
        /// Returns a dictionary of tiles using labels assigned to the given direction as keys
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private Dictionary<T, List<int>> CreateLookup(int direction)
        {
            var result = new Dictionary<T, List<int>>();
            var labels = _labels[direction];

            // for each tile in the given direction...
            for(int i = 0; i < _tileCount; i++)
            {
                var key = labels[i];
                List<int> tiles;

                if (!result.TryGetValue(key, out tiles))
                    tiles = result[key] = new List<int>();

                tiles.Add(i);
            }

            return result;
        }
    }
}
