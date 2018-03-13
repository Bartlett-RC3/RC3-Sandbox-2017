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
    public class GraphFactory : GraphFactoryBase<Graph>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Graph Create()
        {
            return new Graph();
        }
    }
}
