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
    /// 
    /// </summary>
    static class GraphUtil
    {
        /// <summary>
        /// 
        /// </summary>
        public static void GetVertexDepths(IGraph graph, IEnumerable<int> sources, int[] result, IEnumerable<int> ignore = null)
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
                foreach (int v1 in graph.GetVertexNeighbors(v0))
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
        public static void GetVertexDistances(IEdgeGraph graph, float[] edgeLengths, IEnumerable<int> sources, float[] result, IEnumerable<int> ignore = null)
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
                foreach (var e in graph.GetIncidentEdges(v0))
                {
                    // calculate distance to v1
                    int v1 = graph.GetOppositeVertex(e, v0);
                    float d1 = d0 + edgeLengths[e];

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
        public static IEnumerable<int> WalkToMin(IGraph graph, float[] vertexDistances, int startVertex)
        {
            int v0 = startVertex;
            float d0 = vertexDistances[v0];

            while (true)
            {
                int vmin = -1;
                float dmin = float.MaxValue;

                // find neighbour with smallest distance
                foreach (var v1 in graph.GetVertexNeighbors(v0))
                {
                    var d1 = vertexDistances[v1];

                    if (d1 < dmin)
                    {
                        vmin = v1;
                        dmin = d1;
                    }
                }

                // break if local minimum found
                if (dmin >= d0)
                    yield break;

                yield return vmin;
                v0 = vmin;
                d0 = dmin;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="vertexDistances"></param>
        /// <param name="startVertex"></param>
        /// <returns></returns>
        public static IEnumerable<int> WalkToMin(IEdgeGraph graph, float[] vertexDistances, int startVertex)
        {
            int v0 = startVertex;
            float d0 = vertexDistances[v0];

            while (true)
            {
                // edge to lowest neighbour
                int emin = -1;
                float dmin = float.MaxValue;

                // find edge to neighbour with smallest distance
                foreach (var e in graph.GetIncidentEdges(v0))
                {
                    var v1 = graph.GetOppositeVertex(e, v0);
                    var d1 = vertexDistances[v1];

                    if (d1 < dmin)
                    {
                        emin = e;
                        dmin = d1;
                    }
                }

                // break if local minimum found
                if (dmin >= d0)
                    yield break;

                yield return emin;
                v0 = graph.GetOppositeVertex(emin, v0);
                d0 = dmin;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="vertexDistances"></param>
        /// <param name="startVertex"></param>
        /// <returns></returns>
        public static IEnumerable<int> WalkToMax(IEdgeGraph graph, float[] vertexDistances, int startVertex)
        {
            int v0 = startVertex;
            float d0 = vertexDistances[v0];

            while (true)
            {
                // edge to lowest neighbour
                int emax = -1;
                float dmax = float.MinValue;

                // find edge to neighbour with smallest distance
                foreach (var e in graph.GetIncidentEdges(v0))
                {
                    var v1 = graph.GetOppositeVertex(e, v0);
                    var d1 = vertexDistances[v1];

                    if (d1 > dmax)
                    {
                        emax = e;
                        dmax = d1;
                    }
                }

                // break if local maximum found
                if (dmax <= d0)
                    yield break;

                yield return emax;
                v0 = graph.GetOppositeVertex(emax, v0);
                d0 = dmax;
            }
        }
    }
}