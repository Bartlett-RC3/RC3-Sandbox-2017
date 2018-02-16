using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Notes
 */  

namespace RC3
{
    /// <summary>
    /// Contains static methods for creating instances of EdgeGraph
    /// </summary>
    public abstract class GraphFactoryBase<T>
        where T : IGraph
    {
        /// <summary>
        /// 
        /// </summary>
        protected abstract T Create();


        /// <summary>
        /// 
        /// </summary>
        public T CreateGrid(int countX, int countY)
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

                    // -x
                    if (x > 0)
                        g.AddEdge(i, i - 1);

                    // -y
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
        public T CreateTriangleGrid(int countX, int countY)
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

                    // x - 1
                    if (x > 0)
                        g.AddEdge(i, i - 1);

                    // y - 1
                    if (y > 0)
                        g.AddEdge(i, i - countX);

                    if (y % 2 == 0)
                    {
                        // y - 1, x - 1
                        if (y > 0 && x > 0)
                            g.AddEdge(i, i - countX - 1);
                    }
                    else
                    {
                        // y - 1, x + 1
                        if (y > 0 && x < countX - 1)
                            g.AddEdge(i, i - countX + 1);
                    }
                }
            }

            return g;
        }
    }
}