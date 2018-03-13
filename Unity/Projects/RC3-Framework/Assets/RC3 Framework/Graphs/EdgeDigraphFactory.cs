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
    public class EdgeDigraphFactory : DigraphFactoryBase<EdgeDigraph>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override EdgeDigraph Create()
        {
            return new EdgeDigraph();
        }
    }
}
