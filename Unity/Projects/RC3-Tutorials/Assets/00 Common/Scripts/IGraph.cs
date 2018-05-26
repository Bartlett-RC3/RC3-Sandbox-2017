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
        /// <param name="vertex"></param>
        /// <returns></returns>
        int GetDegree(int vertex);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        void AddEdge(int v0, int v1);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="capacity"></param>
        void AddVertex(int capacity = 4);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        IEnumerable<int> GetConnectedVertices(int vertex);
    }
}