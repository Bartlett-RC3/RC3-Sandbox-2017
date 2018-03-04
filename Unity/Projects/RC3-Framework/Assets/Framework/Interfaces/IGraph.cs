using System.Collections.Generic;

/*
 * Notes
 */
 
namespace RC3
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
        void AddEdge(int v0, int v1);


        /// <summary>
        /// 
        /// </summary>
        void AddVertex();


        /// <summary>
        /// 
        /// </summary>
        int GetVertexNeighbor(int vertex, int index);


        /// <summary>
        /// 
        /// </summary>
        IEnumerable<int> GetVertexNeighbors(int vertex);
    }
}