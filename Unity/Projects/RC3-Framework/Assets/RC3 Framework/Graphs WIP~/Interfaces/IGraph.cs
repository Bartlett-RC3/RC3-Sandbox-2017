using System.Collections.Generic;

using SpatialSlur.Core;

/*
 * Notes
 */
 
namespace RC3.Graphs
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGraph
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
        ReadOnlyListView<int> GetVertexNeighbors(int vertex);
    }
}