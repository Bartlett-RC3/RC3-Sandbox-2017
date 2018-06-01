
/*
 * Notes
 * 
 * This constraint is overkill for the tile model
 * Can be an array of Sets since the number of variables is known in advance
 */ 

using System.Collections.Generic;

namespace RC3.WFC
{
    /// <summary>
    /// 
    /// </summary>
    public class SetConstraint : Constraint<int>
    {
        private HashSet<int>[] _table;


        /// <summary>
        /// 
        /// </summary>
        public SetConstraint(int targetSize)
        {
            _table = new HashSet<int>[targetSize];

            for (int i = 0; i < targetSize; i++)
                _table[i] = new HashSet<int>();
        }
        

        /// <summary>
        /// Adds valid sources for the given target.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public void Add(int target, IEnumerable<int> sources)
        {
            _table[target].UnionWith(sources);
        }


        /// <summary>
        /// Removes valid sources for the given target.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="sources"></param>
        public void Remove(int target, IEnumerable<int> sources)
        {
            _table[target].ExceptWith(sources);
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        protected override bool IsConsistent(int target, int source)
        {
            return _table[target].Contains(source);
        }


        // DEBUG_

        public HashSet<int> GetDebug(int target)
        {
            return _table[target];
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class SetConstraint<T> : Constraint<T>
    {
        private Dictionary<T, HashSet<T>> _table;


        /// <summary>
        /// 
        /// </summary>
        public SetConstraint()
        {
            _table = new Dictionary<T, HashSet<T>>();
        }


        /// <summary>
        /// Adds valid sources for the given target
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public void Add(T target, IEnumerable<T> sources)
        {
            HashSet<T> src;

            if (!_table.TryGetValue(target, out src))
                src = _table[target] = new HashSet<T>();

            src.UnionWith(sources);
        }


        /// <summary>
        /// Removes valid sources for the given target
        /// </summary>
        /// <param name="target"></param>
        /// <param name="sources"></param>
        public void Remove(T target, IEnumerable<T> sources)
        {
            HashSet<T> src;

            if (!_table.TryGetValue(target, out src))
                return;

            src.ExceptWith(sources);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        protected override bool IsConsistent(T target, T source)
        {
            HashSet<T> src;

            if (!_table.TryGetValue(target, out src))
                return false;

            return src.Contains(source);
        }
    }
}
