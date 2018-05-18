using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC3.Unity.GraphGrowth
{
    /// <summary>
    /// 
    /// </summary>
    public struct Triangle
    {
        public int Vertex0;
        public int Vertex1;
        public int Vertex2;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public Triangle(int v0, int v1, int v2)
        {
            Vertex0 = v0;
            Vertex1 = v1;
            Vertex2 = v2;
        }
    }
}
