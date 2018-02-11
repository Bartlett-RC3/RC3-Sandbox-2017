using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 
/// </summary>
public static class GraphFactory
{
    /// <summary>
    /// 
    /// </summary>
    public static Graph CreateGrid(int countX, int countY)
    {
        Graph g = new Graph();
        int n = countX * countY;

        // add vertices
        for (int i = 0; i < n; i++)
            g.AddVertex();

        // add edges
        for (int i = 0; i < countY; i++)
        {
            for (int j = 0; j < countX; j++)
            {
                int id0 = j + i * countX;

                // -y
                if(i > 0)
                {
                    var id1 = j + (i - 1) * countX;
                    g.AddEdge(id0, id1);
                }

                // -x
                if(j > 0)
                {
                    var id1 = j - 1 + i * countX;
                    g.AddEdge(id0, id1);
                }
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
    public static EdgeGraph CreateTriangleGrid(int countX, int countY)
    {
        var g = new EdgeGraph();
        int n = countX * countY;

        // add vertices
        for (int i = 0; i < n; i++)
            g.AddVertex();

        // add edges
        for (int i = 0; i < countY; i++)
        {
            for (int j = 0; j < countX; j++)
            {
                int v0 = ToIndex(j, i, countX);

                // x - 1
                if (j > 0)
                {
                    var v1 = ToIndex(j - 1, i, countX);
                    g.AddEdge(v0, v1);
                }

                // y - 1
                if (i > 0)
                {
                    var v1 = ToIndex(j, i - 1, countX);
                    g.AddEdge(v0, v1);
                }

                if (i % 2 == 0)
                {
                    // y - 1, x - 1
                    if (i > 0 && j > 0)
                    {
                        var v1 = ToIndex(j - 1, i - 1, countX);
                        g.AddEdge(v0, v1);
                    }
                }
                else
                {
                    // y - 1, x + 1
                    if (i > 0 && j < countX - 1)
                    {
                        var v1 = ToIndex(j + 1, i - 1, countX);
                        g.AddEdge(v0, v1);
                    }
                }
            }
        }

        return g;
    }


    /// <summary>
    /// Maps a 2d index to a 1d index
    /// </summary>
    private static int ToIndex(int x, int y, int countX)
    {
        return x + y * countX;
    }
}
