using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC3.WFC
{
    /// <summary>
    /// Constraints are assigned per edge in a graph
    /// </summary>
    public abstract class Constraint<T>
    {
        /// <summary>
        /// Returns all elements in the target domain that are consistent with the given source domain.
        /// </summary>
        /// <param name="targets"></param>
        /// <param name="sources"></param>
        /// <returns></returns>
        public IEnumerable<T> GetConsistent(IEnumerable<T> targets, IEnumerable<T> sources)
        {
            return targets.Where(t => IsConsistent(t, sources));
        }


        /// <summary>
        /// Returns all elements in the target domain that are inconsistent with the given source domain.
        /// </summary>
        /// <param name="domain"></param>
        public IEnumerable<T> GetInconsistent(IEnumerable<T> targets, IEnumerable<T> sources)
        {
            return targets.Where(t => !IsConsistent(t, sources));
        }


        /// <summary>
        /// Returns true if the target is consistent with at least one source
        /// </summary>
        private bool IsConsistent(T target, IEnumerable<T> sources)
        {
            foreach (var s in sources)
            {
                if (IsConsistent(target, s))
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Returns true if the given target is consistent with the given source.
        /// </summary>
        protected abstract bool IsConsistent(T target, T source);
    }
}
