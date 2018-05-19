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
    public abstract class DigraphFactoryBase<G>
        where G : IDigraph
    {
        /// <summary>
        /// 
        /// </summary>
        protected abstract G Create();


        /// <summary>
        /// 
        /// </summary>
        public G CreateGrid(int countX, int countY)
        {
            var g = Create();
            int n = countX * countY;

            int lastX = countX - 1;
            int lastY = countY - 1;

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

                    // x+1
                    if (x < lastX)
                        g.AddEdge(i, i + 1);

                    // y+1
                    if (y < lastY)
                        g.AddEdge(i, i + countX); 
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
        public G CreateTriangleGrid(int countX, int countY)
        {
            var g = Create();
            int n = countX * countY;

            var lastX = countX - 1;
            var lastY = countY - 1;

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

                    // x-1, y-1
                    if (x > 0 && y > 0)
                        g.AddEdge(i, i - countX - 1);

                    // y-1
                    if (y > 0)
                        g.AddEdge(i, i - countX);

                    // x+1
                    if (x < lastX)
                        g.AddEdge(i, i + 1);

                    // y+1
                    if (y < lastY)
                        g.AddEdge(i, i + countX);

                    // x-1, y+1
                    if (x > 0 && y < lastY)
                        g.AddEdge(i, i + countX - 1);
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

                    // x+1, y-1
                    if (x < lastX && y > 0)
                        g.AddEdge(i, i - countX + 1);

                    // x+1
                    if (x < lastX)
                        g.AddEdge(i, i + 1);

                    // x+1, y+1
                    if (x < lastX && y < lastY)
                        g.AddEdge(i, i + countX + 1);

                    // y+1
                    if (y < lastY)
                        g.AddEdge(i, i + countX);
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
            int lastZ = countZ - 1;

            // add vertices
            for (int i = 0; i < count; i++)
            {
                g.AddVertex();
                g.AddVertex();
            }

            // add primal edges
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

                        // x+1
                        if (x < lastX)
                            g.AddEdge(i, i + 1);

                        // y+1
                        if (y < lastY)
                            g.AddEdge(i, i + countX);

                        // z+1
                        if (z < lastZ)
                            g.AddEdge(i, i + countXY);

                        //
                        g.AddEdge(i, j);

                        // x-1
                        if (x > 0)
                            g.AddEdge(i, j - 1);

                        // x-1, y-1
                        if (x > 0 && y > 0)
                            g.AddEdge(i, j - countX - 1);

                        // y-1
                        if (y > 0)
                            g.AddEdge(i, j - countX);

                        // z-1
                        if (z > 0)
                            g.AddEdge(i, j - countXY);

                        // x-1, z-1
                        if (x > 0 && z > 0)
                            g.AddEdge(i, j - countXY - 1);

                        // x-1, y-1, z-1
                        if (x > 0 && y > 0 && z > 0)
                            g.AddEdge(i, j - countXY - countX - 1);

                        // y-1, z-1
                        if (y > 0 && z > 0)
                            g.AddEdge(i, j - countXY - countX);
                    }
                }
            }

            // add dual edges
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

                        // x+1
                        if (x < lastX)
                            g.AddEdge(j, j + 1);

                        // y+1
                        if (y < lastY)
                            g.AddEdge(j, j + countX);

                        // z+1
                        if (z < lastZ)
                            g.AddEdge(j, j + countXY);

                        // x+1, y+1, z+1
                        if (x < lastX && y < lastY && z < lastZ)
                            g.AddEdge(j, i + countXY + countX + 1);

                        // y+1, z+1
                        if (y < lastY && z < lastZ)
                            g.AddEdge(j, i + countXY + countX);

                        // z+1
                        if (z < lastZ)
                            g.AddEdge(j, i + countXY);

                        // x+1, z+1
                        if (x < lastX && z < lastZ)
                            g.AddEdge(j, i + countXY + 1);

                        // x+1, y+1
                        if (x < lastX && y < lastY)
                            g.AddEdge(j, i + countX + 1);

                        // y+1
                        if (y < lastY)
                            g.AddEdge(j, i + countX);

                        //
                        g.AddEdge(j, i);

                        // x+1
                        if (x < lastX)
                            g.AddEdge(j, i + 1);
                    }
                }
            }

            return g;
        }
    }
}