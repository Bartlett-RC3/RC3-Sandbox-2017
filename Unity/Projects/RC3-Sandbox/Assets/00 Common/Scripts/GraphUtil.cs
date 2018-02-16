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
    /// 
    /// </summary>
    static class GraphUtil
    {
        /// <summary>
        /// 
        /// </summary>
        public static void GetVertexDepths(Graph graph, IEnumerable<int> sources, int[] result, IEnumerable<int> ignore = null)
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
            if (ignore == null)
                ignore = Enumerable.Empty<int>();

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


        /// <summary>
        /// 
        /// </summary>
        public static void GetVertexDistances(EdgeGraph graph, float[] edgeLengths, IEnumerable<int> sources, float[] result, IEnumerable<int> ignore = null)
        {
            // initialize depths
            for (int i = 0; i < graph.VertexCount; i++)
                result[i] = float.MaxValue;

            // create queue
            var queue = new Queue<int>();

            // initialize source(s)
            foreach (int source in sources)
            {
                queue.Enqueue(source);
                result[source] = 0.0f;
            }

            // initialize ignored
            if (ignore == null)
                ignore = Enumerable.Empty<int>();

            foreach (int index in ignore)
                result[index] = 0.0f;

            // search
            while (queue.Count > 0)
            {
                int v0 = queue.Dequeue();
                float d0 = result[v0];

                // iterate over edges incident to v0
                foreach (var ei in graph.GetIncidentEdges(v0))
                {
                    // calculate distance to v1
                    int v1 = graph.GetEdge(ei).Other(v0);
                    float d1 = d0 + edgeLengths[ei];

                    // if less than the current distance then update
                    if (d1 < result[v1])
                    {
                        queue.Enqueue(v1);
                        result[v1] = d1;
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="vertexDistances"></param>
        /// <param name="startVertex"></param>
        /// <returns></returns>
        public static IEnumerable<int> WalkToMin(EdgeGraph graph, float[] vertexDistances, int startVertex)
        {
            int v0 = startVertex;
            float d0 = vertexDistances[v0];

            while (true)
            {
                // edge to lowest neighbour
                int eMin = -1;
                float dMin = float.MaxValue;

                // find edge to neighbour with smallest distance
                foreach (var ei in graph.GetIncidentEdges(v0))
                {
                    var v1 = graph.GetEdge(ei).Other(v0);
                    var d1 = vertexDistances[v1];

                    if (d1 < dMin)
                    {
                        eMin = ei;
                        dMin = d1;
                    }
                }

                // if less than current distance, take a step
                if (dMin < d0)
                {
                    yield return eMin;

                    // update current vertex and distance
                    v0 = graph.GetEdge(eMin).Other(v0);
                    d0 = dMin;
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}