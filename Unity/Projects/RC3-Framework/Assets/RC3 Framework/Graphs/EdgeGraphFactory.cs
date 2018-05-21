using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Notes 
 */
 
namespace RC3.Graphs
{
    /// <summary>
    /// 
    /// </summary>
    public class EdgeGraphFactory : GraphFactoryBase<EdgeGraph>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override EdgeGraph Create()
        {
            return new EdgeGraph();
        }
    }
}
