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
    public class TileMap<T>
    {
        #region Static

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tileCount"></param>
        /// <returns></returns>
        public static TileMap<T> CreateQuadrilateral(int tileCount)
        {
            const int n = 4;
            var tileSet = new TileMap<T>(n, tileCount);
            
            for (int i = 0; i < n; i += 2)
                tileSet.MakeOpposites(i, i + 1);

            return tileSet;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tileCount"></param>
        /// <returns></returns>
        public static TileMap<T> CreateHexagonal(int tileCount)
        {
            const int n = 6;
            var tileSet = new TileMap<T>(n, tileCount);

            for (int i = 0; i < n; i += 2)
                tileSet.MakeOpposites(i, i + 1);

            return tileSet;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tileCount"></param>
        /// <returns></returns>
        public static TileMap<T> CreateTruncatedOctahedral(int tileCount)
        {
            const int n = 14;
            var tileSet = new TileMap<T>(n, tileCount);

            for(int i = 0; i < n; i+=2)
                tileSet.MakeOpposites(i, i + 1);

            return tileSet;
        }

       
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<K, List<int>> CreateLookup<K>(K[] keys)
        {
            var result = new Dictionary<K, List<int>>();

            for (int i = 0; i < keys.Length; i++)
                Add(result, keys[i], i);

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        private static void Add<K, V>(Dictionary<K, List<V>> lookup, K key, V value)
        {
            List<V> vals;

            if (!lookup.TryGetValue(key, out vals))
                vals = lookup[key] = new List<V>();

            vals.Add(value);
        }

        #endregion


        private T[][] _labels; // labels [direction][tile]
        private int[] _opposite; // the opposite of each direction

       
        /// <summary>
        /// The number of neighbors for each tile in this map.
        /// </summary>
        public int TileDegree
        {
            get { return _labels.Length; }
        }


        /// <summary>
        /// The number of tiles in this set
        /// </summary>
        public int TileCount
        {
            get { return _labels[0].Length; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tileCount"></param>
        private TileMap(int tileDegree, int tileCount)
        {
            _labels = new T[tileDegree][];
            _opposite = new int[tileDegree];

            for (int i = 0; i < tileDegree; i++)
                _labels[i] = new T[tileCount];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        public int GetOpposite(int direction)
        {
            return _opposite[direction];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="other"></param>
        private void MakeOpposites(int direction0, int direction1)
        {
            _opposite[direction0] = direction1;
            _opposite[direction1] = direction0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        public T GetLabel(int direction, int tile)
        {
            return _labels[direction][tile];
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="labels"></param>
        public void SetLabel(int direction, int tile, T label)
        {
            _labels[direction][tile] = label;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SetConstraint> CreateConstraints()
        {
            for (int i = 0; i < TileDegree; i++)
            {
                var j = _opposite[i];
                yield return CreateConstraint(_labels[j], CreateLookup(_labels[i]));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private SetConstraint CreateConstraint(T[] labels, Dictionary<T, List<int>> lookup)
        {
            var constraint = new SetConstraint(TileCount);
            
            for (int i = 0; i < labels.Length; i++)
                constraint.Add(i, lookup[labels[i]]);

            return constraint;
        }
    }
}
