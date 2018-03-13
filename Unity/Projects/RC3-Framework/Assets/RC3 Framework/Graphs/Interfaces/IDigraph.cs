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
    public interface IDigraph
    {
        /// <summary>
        /// 
        /// </summary>
        int VertexCount { get; }


        /// <summary>
        /// 
        /// </summary>
        int GetDegreeOut(int vertex);


        /// <summary>
        /// 
        /// </summary>
        int GetDegreeIn(int vertex);


        /// <summary>
        /// 
        /// </summary>
        void AddVertex();


        /// <summary>
        /// 
        /// </summary>
        void AddEdge(int v0, int v1);


        /// <summary>
        /// 
        /// </summary>
        bool HasEdge(int v0, int v1);


        /// <summary>
        /// 
        /// </summary>
        int GetVertexNeighborOut(int vertex, int index);


        /// <summary>
        /// 
        /// </summary>
        IEnumerable<int> GetVertexNeighborsOut(int vertex);


        /// <summary>
        /// 
        /// </summary>
        int GetVertexNeighborIn(int vertex, int index);


        /// <summary>
        /// 
        /// </summary>
        IEnumerable<int> GetVertexNeighborsIn(int vertex);
    }
}
