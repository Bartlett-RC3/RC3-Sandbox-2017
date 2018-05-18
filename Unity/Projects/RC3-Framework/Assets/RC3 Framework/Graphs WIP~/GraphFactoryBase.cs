using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Notes
 */  

namespace RC3.Graphs
{
    /// <summary>
    /// Contains static methods for creating instances of EdgeGraph
    /// </summary>
    public abstract class GraphFactoryBase<G>
        where G : IGraph
    {
        /// <summary>
        /// 
        /// </summary>
        protected abstract G Create();


        /// <summary>
        /// 
        /// </summary>
        public G CreateQuadGrid(int countX, int countY)
        {
            var g = Create();
            int n = countX * countY;

            // add vertices
            for (int i = 0; i < n; i++)
                g.AddVertex();

            // add edges
            for (int y = 0; y < countY; y++)
            {
                for (int x = 0; x < countX; x++)
                {
                    int i = x + y * countX;

                    // x-1
                    if (x > 0)
                        g.AddEdge(i, i - 1);

                    // y-1
                    if (y > 0)
                        g.AddEdge(i, i - countX);
                }
            }

            return g;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="countX"></param>
        /// <param name="countY"></param>
        /// <returns></returns>
        public G CreateHexagonGrid(int countX, int countY)
        {
            var g = Create();
            int n = countX * countY;

            int lastX = countX - 1;

            // add vertices
            for (int i = 0; i < n; i++)
                g.AddVertex();

            // add even row edges
            for (int y = 0; y < countY; y += 2)
            {
                for (int x = 0; x < countX; x++)
                {
                    int i = x + y * countX;

                    // x-1
                    if (x > 0)
                        g.AddEdge(i, i - 1);

                    // y-1
                    if (y > 0)
                        g.AddEdge(i, i - countX);

                    // y-1, x-1
                    if (y > 0 && x > 0)
                        g.AddEdge(i, i - countX - 1);
                }
            }

            // add odd row edges
            for (int y = 1; y < countY; y += 2)
            {
                for (int x = 0; x < countX; x++)
                {
                    int i = x + y * countX;

                    // x-1
                    if (x > 0)
                        g.AddEdge(i, i - 1);

                    // y-1
                    if (y > 0)
                        g.AddEdge(i, i - countX);

                    // y-1, x + 1
                    if (y > 0 && x < lastX)
                        g.AddEdge(i, i - countX + 1);
                }
            }

            return g;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="countX"></param>
        /// <param name="countY"></param>
        /// <param name="countZ"></param>
        /// <returns></returns>
        public G CreateTruncatedOctahedronGrid(int countX, int countY, int countZ)
        {
            var g = Create();
            int countXY = countX * countY;
            int count = countXY * countZ;

            int lastX = countX - 1;
            int lastY = countY - 1;

            // add vertices
            for (int i = 0; i < count; i++)
            {
                g.AddVertex();
                g.AddVertex();
            }

            // add even layer edges
            for (int z = 0; z < countZ; z++)
            {
                for (int y = 0; y < countY; y++)
                {
                    for (int x = 0; x < countX; x++)
                    {
                        int i = x + y * countX + z * countXY;
                        int j = i + count;

                        // x-1
                        if (x > 0)
                            g.AddEdge(i, i - 1);

                        // y-1
                        if (y > 0)
                            g.AddEdge(i, i - countX);

                        // z-1
                        if (z > 0)
                            g.AddEdge(i, i - countXY);

                        // x-1, y-1, z-1
                        if (x > 0 && y > 0 && z > 0)
                            g.AddEdge(i, j - countXY - countX - 1);

                        // y-1, z-1
                        if (y > 0 && z > 0)
                            g.AddEdge(i, j - countXY - countX);

                        // x-1, z-1
                        if (x > 0 && z > 0)
                            g.AddEdge(i, j - countXY - 1);

                        // z-1
                        if (z > 0)
                            g.AddEdge(i, j - countXY);
                    }
                }
            }

            // add odd layer edges
            for (int z = 0; z < countZ; z++)
            {
                for (int y = 0; y < countY; y++)
                {
                    for (int x = 0; x < countX; x++)
                    {
                        int i = x + y * countX + z * countXY;
                        int j = i + count;

                        // x-1
                        if (x > 0)
                            g.AddEdge(j, j - 1);

                        // y-1
                        if (y > 0)
                            g.AddEdge(j, j - countX);

                        // z-1
                        if (z > 0)
                            g.AddEdge(j, j - countXY);

                        //
                        g.AddEdge(j, i);

                        // x+1
                        if (x < lastX)
                            g.AddEdge(j, i + 1);

                        // y+1
                        if (y < lastY)
                            g.AddEdge(j, i + countX);

                        // x+1, y+1
                        if (x < lastX && y < lastY)
                            g.AddEdge(j, i + countX + 1);
                    }
                }
            }

            return g;
        }
    }
}