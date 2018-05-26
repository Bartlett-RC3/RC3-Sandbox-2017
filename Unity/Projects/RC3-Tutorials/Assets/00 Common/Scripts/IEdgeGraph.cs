using System.Collections.Generic;

/*
 * Notes
 */
 
namespace RC3
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
        /// <param name="index"></param>
        /// <returns></returns>
        Edge GetEdge(int index);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        IEnumerable<int> GetIncidentEdges(int vertex);
    }
}