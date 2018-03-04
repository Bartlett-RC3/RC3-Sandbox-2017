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