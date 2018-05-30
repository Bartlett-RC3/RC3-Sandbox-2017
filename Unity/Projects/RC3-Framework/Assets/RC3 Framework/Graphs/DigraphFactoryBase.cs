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
        public G CreateRectangleGrid(int countX, int countY, bool uniform = false)
        {
            return uniform ? CreateRectangleGridUniform(countX, countY) : CreateRectangleGridDefault(countX, countY);
        }


        /// <summary>
        /// 
        /// </summary>
        private G CreateRectangleGridDefault(int countX, int countY)
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

                    // x+1
                    if (x < lastX)
                        g.AddEdge(i, i + 1);

                    // y-1
                    if (y > 0)
                        g.AddEdge(i, i - countX);
                   
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
        private G CreateRectangleGridUniform(int countX, int countY)
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
                    else
                        g.AddEdge(i, i);

                    // x+1
                    if (x < lastX)
                        g.AddEdge(i, i + 1);
                    else
                        g.AddEdge(i, i);

                    // y-1
                    if (y > 0)
                        g.AddEdge(i, i - countX);
                    else
                        g.AddEdge(i, i);

                    // y+1
                    if (y < lastY)
                        g.AddEdge(i, i + countX);
                    else
                        g.AddEdge(i, i);
                }
            }

            return g;
        }


        /// <summary>
        /// 
        /// </summary>
        public G CreateHexagonGrid(int countX, int countY, bool uniform = false)
        {
            return uniform ? CreateHexagonGridUniform(countX, countY) : CreateHexagonGridDefault(countX, countY);
        }


        /// <summary>
        /// 
        /// </summary>
        private G CreateHexagonGridDefault(int countX, int countY)
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

                    // x+1
                    if (x < lastX)
                        g.AddEdge(i, i + 1);

                    // x-1, y-1
                    if (x > 0 && y > 0)
                        g.AddEdge(i, i - countX - 1);

                    // x-1, y+1
                    if (x > 0 && y < lastY)
                        g.AddEdge(i, i + countX - 1);

                    // y-1
                    if (y > 0)
                        g.AddEdge(i, i - countX);
                    
                    // y+1
                    if (y < lastY)
                        g.AddEdge(i, i + countX);
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

                    // x+1
                    if (x < lastX)
                        g.AddEdge(i, i + 1);

                    // y-1
                    if (y > 0)
                        g.AddEdge(i, i - countX);

                    // y+1
                    if (y < lastY)
                        g.AddEdge(i, i + countX);

                    // x+1, y-1
                    if (x < lastX && y > 0)
                        g.AddEdge(i, i - countX + 1);

                    // x+1, y+1
                    if (x < lastX && y < lastY)
                        g.AddEdge(i, i + countX + 1);
                }
            }

            return g;
        }


        /// <summary>
        /// 
        /// </summary>
        private  G CreateHexagonGridUniform(int countX, int countY)
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
                    else
                        g.AddEdge(i, i);

                    // x+1
                    if (x < lastX)
                        g.AddEdge(i, i + 1);
                    else
                        g.AddEdge(i, i);

                    // x-1, y-1
                    if (x > 0 && y > 0)
                        g.AddEdge(i, i - countX - 1);
                    else
                        g.AddEdge(i, i);

                    // x-1, y+1
                    if (x > 0 && y < lastY)
                        g.AddEdge(i, i + countX - 1);
                    else
                        g.AddEdge(i, i);

                    // y-1
                    if (y > 0)
                        g.AddEdge(i, i - countX);
                    else
                        g.AddEdge(i, i);

                    // y+1
                    if (y < lastY)
                        g.AddEdge(i, i + countX);
                    else
                        g.AddEdge(i, i);
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
                    else
                        g.AddEdge(i, i);

                    // x+1
                    if (x < lastX)
                        g.AddEdge(i, i + 1);
                    else
                        g.AddEdge(i, i);

                    // y-1
                    if (y > 0)
                        g.AddEdge(i, i - countX);
                    else
                        g.AddEdge(i, i);

                    // y+1
                    if (y < lastY)
                        g.AddEdge(i, i + countX);
                    else
                        g.AddEdge(i, i);

                    // x+1, y-1
                    if (x < lastX && y > 0)
                        g.AddEdge(i, i - countX + 1);
                    else
                        g.AddEdge(i, i);

                    // x+1, y+1
                    if (x < lastX && y < lastY)
                        g.AddEdge(i, i + countX + 1);
                    else
                        g.AddEdge(i, i);
                }
            }

            return g;
        }


        /// <summary>
        /// 
        /// </summary>
        public G CreateTruncatedOctahedronGrid(int countX, int countY, int countZ, bool uniform = false)
        {
            return uniform ?
                CreateTruncatedOctahedronGridUniform(countX, countY, countZ) :
                CreateTruncatedOctahedronGridDefault(countX, countY, countZ);
        }


        /// <summary>
        /// 
        /// </summary>
        private G CreateTruncatedOctahedronGridDefault(int countX, int countY, int countZ)
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

                        // x+1
                        if (x < lastX)
                            g.AddEdge(i, i + 1);

                        // y-1
                        if (y > 0)
                            g.AddEdge(i, i - countX);

                        // y+1
                        if (y < lastY)
                            g.AddEdge(i, i + countX);

                        // z-1
                        if (z > 0)
                            g.AddEdge(i, i - countXY);

                        // z+1
                        if (z < lastZ)
                            g.AddEdge(i, i + countXY);

                        // x-1, y-1, z-1
                        if (x > 0 && y > 0 && z > 0)
                            g.AddEdge(i, j - countXY - countX - 1);

                        //
                        g.AddEdge(i, j);

                        // y-1, z-1
                        if (y > 0 && z > 0)
                            g.AddEdge(i, j - countXY - countX);

                        // x-1
                        if (x > 0)
                            g.AddEdge(i, j - 1);

                        // x-1, z-1
                        if (x > 0 && z > 0)
                            g.AddEdge(i, j - countXY - 1);

                        // y-1
                        if (y > 0)
                            g.AddEdge(i, j - countX);

                        // z-1
                        if (z > 0)
                            g.AddEdge(i, j - countXY);


                        // x-1, y-1
                        if (x > 0 && y > 0)
                            g.AddEdge(i, j - countX - 1);
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

                        // x+1
                        if (x < lastX)
                            g.AddEdge(j, j + 1);

                        // y-1
                        if (y > 0)
                            g.AddEdge(j, j - countX);

                        // y+1
                        if (y < lastY)
                            g.AddEdge(j, j + countX);

                        // z-1
                        if (z > 0)
                            g.AddEdge(j, j - countXY);

                        // z+1
                        if (z < lastZ)
                            g.AddEdge(j, j + countXY);

                        //
                        g.AddEdge(j, i);

                        // x+1, y+1, z+1
                        if (x < lastX && y < lastY && z < lastZ)
                            g.AddEdge(j, i + countXY + countX + 1);
                        
                        // x+1
                        if (x < lastX)
                            g.AddEdge(j, i + 1);

                        // y+1, z+1
                        if (y < lastY && z < lastZ)
                            g.AddEdge(j, i + countXY + countX);

                        // y+1
                        if (y < lastY)
                            g.AddEdge(j, i + countX);

                        // x+1, z+1
                        if (x < lastX && z < lastZ)
                            g.AddEdge(j, i + countXY + 1);

                        // x+1, y+1
                        if (x < lastX && y < lastY)
                            g.AddEdge(j, i + countX + 1);

                        // z+1
                        if (z < lastZ)
                            g.AddEdge(j, i + countXY);
                    }
                }
            }

            return g;
        }


        /// <summary>
        /// 
        /// </summary>
        private G CreateTruncatedOctahedronGridUniform(int countX, int countY, int countZ)
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
                        else
                            g.AddEdge(i, i);

                        // x+1
                        if (x < lastX)
                            g.AddEdge(i, i + 1);
                        else
                            g.AddEdge(i, i);

                        // y-1
                        if (y > 0)
                            g.AddEdge(i, i - countX);
                        else
                            g.AddEdge(i, i);

                        // y+1
                        if (y < lastY)
                            g.AddEdge(i, i + countX);
                        else
                            g.AddEdge(i, i);

                        // z-1
                        if (z > 0)
                            g.AddEdge(i, i - countXY);
                        else
                            g.AddEdge(i, i);

                        // z+1
                        if (z < lastZ)
                            g.AddEdge(i, i + countXY);
                        else
                            g.AddEdge(i, i);

                        // x-1, y-1, z-1
                        if (x > 0 && y > 0 && z > 0)
                            g.AddEdge(i, j - countXY - countX - 1);
                        else
                            g.AddEdge(i, i);

                        //
                        g.AddEdge(i, j);

                        // y-1, z-1
                        if (y > 0 && z > 0)
                            g.AddEdge(i, j - countXY - countX);
                        else
                            g.AddEdge(i, i);

                        // x-1
                        if (x > 0)
                            g.AddEdge(i, j - 1);
                        else
                            g.AddEdge(i, i);

                        // x-1, z-1
                        if (x > 0 && z > 0)
                            g.AddEdge(i, j - countXY - 1);
                        else
                            g.AddEdge(i, i);

                        // y-1
                        if (y > 0)
                            g.AddEdge(i, j - countX);
                        else
                            g.AddEdge(i, i);

                        // z-1
                        if (z > 0)
                            g.AddEdge(i, j - countXY);
                        else
                            g.AddEdge(i, i);

                        // x-1, y-1
                        if (x > 0 && y > 0)
                            g.AddEdge(i, j - countX - 1);
                        else
                            g.AddEdge(i, i);
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
                        else
                            g.AddEdge(j, j);

                        // x+1
                        if (x < lastX)
                            g.AddEdge(j, j + 1);
                        else
                            g.AddEdge(j, j);

                        // y-1
                        if (y > 0)
                            g.AddEdge(j, j - countX);
                        else
                            g.AddEdge(j, j);

                        // y+1
                        if (y < lastY)
                            g.AddEdge(j, j + countX);
                        else
                            g.AddEdge(j, j);

                        // z-1
                        if (z > 0)
                            g.AddEdge(j, j - countXY);
                        else
                            g.AddEdge(j, j);

                        // z+1
                        if (z < lastZ)
                            g.AddEdge(j, j + countXY);
                        else
                            g.AddEdge(j, j);

                        //
                        g.AddEdge(j, i);

                        // x+1, y+1, z+1
                        if (x < lastX && y < lastY && z < lastZ)
                            g.AddEdge(j, i + countXY + countX + 1);
                        else
                            g.AddEdge(j, j);

                        // x+1
                        if (x < lastX)
                            g.AddEdge(j, i + 1);
                        else
                            g.AddEdge(j, j);

                        // y+1, z+1
                        if (y < lastY && z < lastZ)
                            g.AddEdge(j, i + countXY + countX);
                        else
                            g.AddEdge(j, j);

                        // y+1
                        if (y < lastY)
                            g.AddEdge(j, i + countX);
                        else
                            g.AddEdge(j, j);

                        // x+1, z+1
                        if (x < lastX && z < lastZ)
                            g.AddEdge(j, i + countXY + 1);
                        else
                            g.AddEdge(j, j);

                        // x+1, y+1
                        if (x < lastX && y < lastY)
                            g.AddEdge(j, i + countX + 1);
                        else
                            g.AddEdge(j, j);
                        
                        // z+1
                        if (z < lastZ)
                            g.AddEdge(j, i + countXY);
                        else
                            g.AddEdge(j, j);
                    }
                }
            }

            return g;
        }
    }
}