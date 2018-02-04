using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


static class GraphUtil
{
    /// <summary>
    /// 
    /// </summary>
    public static void GetVertexDepths(Graph graph, IEnumerable<int> sources, int[] result)
    {
        // initialize depths
        for (int i = 0; i < graph.VertexCount; i++)
            result[i] = int.MaxValue;

        // create queue
        var queue = new Queue<int>();

        // initialize source(s)
        foreach (int source in sources)
        {
            queue.Enqueue(source);
            result[source] = 0;
        }
 
        // search
        while(queue.Count > 0)
        {
            int v0 = queue.Dequeue();
            int d1 = result[v0] + 1; // depth from v0

            // iterate over neighbours of v0
            foreach(int v1 in graph.GetConnectedVertices(v0))
            {
                // if d1 is less than the current depth to v1...
                if(d1 < result[v1])
                {
                    // add to queue and update depth to v1
                    queue.Enqueue(v1);
                    result[v1] = d1;
                }
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public static void GetVertexDepths(Graph graph, IEnumerable<int> sources, IEnumerable<int> ignore, int[] result)
    {
        // initialize depths
        for (int i = 0; i < graph.VertexCount; i++)
            result[i] = int.MaxValue;

        // create queue
        var queue = new Queue<int>();

        // initialize source(s)
        foreach (int source in sources)
        {
            queue.Enqueue(source);
            result[source] = 0;
        }

        // initialize ignored
        foreach (int index in ignore)
            result[index] = 0;

        // search
        while (queue.Count > 0)
        {
            int v0 = queue.Dequeue();
            int d1 = result[v0] + 1; // depth from v0

            // iterate over neighbours of v0
            foreach (int v1 in graph.GetConnectedVertices(v0))
            {
                // if d1 is less than the current depth to v1...
                if (d1 < result[v1])
                {
                    // add to queue and update depth to v1
                    queue.Enqueue(v1);
                    result[v1] = d1;
                }
            }
        }
    }
}
