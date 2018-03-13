using System.Collections.Generic;

/*
 * Notes
 */
 
namespace RC3.Graphs
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEdgeGraph : IGraph
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
        int GetOppositeVertex(int edge, int vertex);


        /// <summary>
        /// 
        /// </summary>
        int GetIncidentEdge(int vertex, int index);
        

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<int> GetIncidentEdges(int vertex);
    }
}