using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Notes
 */

namespace RC3
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDigraph
    {
        /// <summary>
        /// 
        /// </summary>
        int VertexCount { get; }


        /// <summary>
        /// 
        /// </summary>
        int GetDegree(int vertex);


        /// <summary>
        /// 
        /// </summary>
        void AddEdge(int v0, int v1);


        /// <summary>
        /// 
        /// </summary>
        void AddVertex(int capacity = 4);


        /// <summary>
        /// 
        /// </summary>
        int GetConnectedVertex(int vertex, int index);


        /// <summary>
        /// 
        /// </summary>
        IEnumerable<int> GetConnectedVertices(int vertex);
    }
}
