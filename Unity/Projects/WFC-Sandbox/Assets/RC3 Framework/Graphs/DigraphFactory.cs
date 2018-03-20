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
    public class DigraphFactory : DigraphFactoryBase<Digraph>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Digraph Create()
        {
            return new Digraph();
        }
    }
}
