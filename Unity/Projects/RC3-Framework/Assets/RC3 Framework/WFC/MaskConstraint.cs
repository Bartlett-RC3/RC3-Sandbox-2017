using System.Collections.Generic;

namespace RC3.WFC
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MaskConstraint : Constraint<int>
    {
        private bool[][] _table;


        /// <summary>
        /// 
        /// </summary>
        public MaskConstraint(int targetSize, int sourceSize)
        {
            _table = new bool[targetSize][];

            for (int i = 0; i < targetSize; i++)
                _table[i] = new bool[sourceSize];
        }


        /// <summary>
        /// Returns the size of the target domain
        /// </summary>
        public int TargetSize
        {
            get { return _table.Length; }
        }


        /// <summary>
        /// Returns the size of the source domain
        /// </summary>
        public int SourceSize
        {
            get { return _table[0].Length; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="targets"></param>
        /// <param name="value"></param>
        public void Set(int target, IEnumerable<int> sources, bool value)
        {
            var src = _table[target];

            foreach(int s in sources)
                src[s] = value;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        protected override bool IsConsistent(int target, int source)
        {
            return _table[target][source];
        }
    }
}
