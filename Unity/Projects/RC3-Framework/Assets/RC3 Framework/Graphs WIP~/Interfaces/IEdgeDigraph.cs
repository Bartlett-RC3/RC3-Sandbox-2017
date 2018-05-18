using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpatialSlur.Core;

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
        ReadOnlyListView<int> GetOutgoingEdges(int vertex);


        /// <summary>
        /// 
        /// </summary>
        ReadOnlyListView<int> GetIncomingEdges(int vertex);
    }
}
