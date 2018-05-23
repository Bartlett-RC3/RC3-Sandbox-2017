using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC3.Unity.TetrahedralGrowth
{
    /// <summary>
    /// 
    /// </summary>
    public struct Tetra
    {
        public readonly int Vertex0;
        public readonly int Vertex1;
        public readonly int Vertex2;
        public readonly int Vertex3;


        /// <summary>
        /// 
        /// </summary>
        public Tetra(int v0, int v1, int v2, int v3)
        {
            Vertex0 = v0;
            Vertex1 = v1;
            Vertex2 = v2;
            Vertex3 = v3;
        }
    }
}
