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
    
}
