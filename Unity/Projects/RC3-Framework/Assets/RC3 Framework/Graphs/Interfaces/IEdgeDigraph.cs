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
    public interface IEdgeDigraph : IDigraph
    {
        /// <summary>
        /// 
        /// </summary>
        int EdgeCount { get; }


        /// <summary>
        /// 
        /// </summary>
        int GetStartVertex(int edge);


        /// <summary>
        /// 
        /// </summary>
        int GetEndVertex(int edge);


        /// <summary>
        /// 
        /// </summary>
        int FindEdge(int v0, int v1);


        /// <summary>
        /// 
        /// </summary>
        int GetOutgoingEdge(int vertex, int index);


        /// <summary>
        /// 
        /// </summary>
        IEnumerable<int> GetOutgoingEdges(int vertex);


        /// <summary>
        /// 
        /// </summary>
        int GetIncomingEdge(int vertex, int index);


        /// <summary>
        /// 
        /// </summary>
        IEnumerable<int> GetIncomingEdges(int vertex);
    }
}
