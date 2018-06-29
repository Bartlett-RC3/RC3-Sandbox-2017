using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RC3.Unity;
/*
 * Notes
 */

namespace RC3.Graphs
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessingUtil
    {
        #region Member Variables

        #endregion

        #region Constructors

        #endregion

        #region Public Methods

        /// <summary>
        /// Return integer depths to all vertices from ground level sources.
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public int[] DepthsFromGroundSources(IGraph graph, Vector3[] positions, float tolerance)
        {
            float[] values = new float[graph.VertexCount];

            List<int> sources = GetGroundSources(graph, positions, tolerance);
            int[] result = new int[graph.VertexCount];
            GraphUtil.GetVertexDepths(graph, sources, result);

            return result;
        }


        /// <summary>
        /// Returns ground level sources
        /// </summary>
        /// <param name="graph"></param>
        /// <returns></returns>
        public List<int> GetGroundSources(IGraph graph, Vector3[] positions, float tolerance)
        {
            List<int> sources = new List<int>();
            float minlevel = float.MaxValue;
            for (int i = 0; i < positions.Length; i++)
            {
                Vector3 position = positions[i];
                List<int> neighbors = graph.GetVertexNeighbors(i).ToList();
                if (neighbors.Count == 0)
                {
                    continue;
                }
                else
                {
                    if (position.y < minlevel)
                    {
                        minlevel = position.y;
                    }
                }

            }

            for (int i = 0; i < positions.Length; i++)
            {
                Vector3 position = positions[i];
                List<int> neighbors = graph.GetVertexNeighbors(i).ToList();
                if (neighbors.Count == 0)
                {
                    continue;
                }

                else
                {
                    if (position.y <= (minlevel + tolerance))
                    {
                        sources.Add(i);
                    }
                }

            }

            return sources;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="depths"></param>
        /// <returns></returns>
        public int MaxDepth(int[] depths)
        {
            int max = 0;
            for (int i = 0; i < depths.Length; i++)
            {
                if (depths[i] > max && depths[i] != int.MaxValue)
                {
                    max = depths[i];
                }
            }
            return max;
        }

        /// <summary>
        /// Remaps graph depths to new range, locates number of unreachable vertices and edgeless vertices
        /// </summary>
        /// <param name="inputvalues"></param>
        /// <param name="minoutvalue"></param>
        /// <param name="maxoutvalue"></param>
        /// <returns></returns>
        public void RemapGraphDepths(IGraph graph, int[] inputvalues, float minoutvalue, float maxoutvalue, out float[] outputvalues, out List<int> unreachablevertices, out List<int> edgelessvertices)
        {
            float[] _outputvalues = new float[inputvalues.Length];
            int maxvalue = int.MinValue;
            int minvalue = int.MaxValue;

            bool[] unreachable = new bool[inputvalues.Length];
            bool[] edgeless = new bool[inputvalues.Length];

            List<int> _edgelessvertices = new List<int>();
            List<int> _unreachablevertices = new List<int>();

            for (int i = 0; i < inputvalues.Length; i++)
            {
                int input = inputvalues[i];
                if (input == int.MaxValue)
                {
                    List<int> neighbors = graph.GetVertexNeighbors(i).ToList();
                    if (neighbors.Count > 0)
                    {
                        unreachable[i] = true;
                        _unreachablevertices.Add(i);
                    }

                    if (neighbors.Count == 0)
                    {
                        edgeless[i] = true;
                        _edgelessvertices.Add(i);
                    }
                }

                if (unreachable[i] != true && edgeless[i] != true)
                {
                    if (input > maxvalue)
                    {
                        maxvalue = input;
                    }
                    if (input < minvalue)
                    {
                        minvalue = input;
                    }
                }
            }

            for (int i = 0; i < inputvalues.Length; i++)
            {
                if (unreachable[i] == true || edgeless[i] == true)
                {
                    inputvalues[i] = maxvalue;
                }

                _outputvalues[i] = Remap((float)inputvalues[i], (float)minvalue, (float)maxvalue, minoutvalue, maxoutvalue);
            }

            outputvalues = _outputvalues;
            unreachablevertices = _unreachablevertices;
            edgelessvertices = _edgelessvertices;
        }

        /// <summary>
        /// Remap the components to an array for graph viz colors
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="components"></param>
        /// <returns></returns>
        public float[] RemapComponentsSizeToArray(IGraph graph, List<HashSet<int>> components)
        {
            float[] _outputvalues = new float[graph.VertexCount];
            List<int> componentsizes = new List<int>();
            int minsize = int.MaxValue;
            int maxsize = int.MinValue;

            for (int i = 0; i < components.Count; i++)
            {
                HashSet<int> component = components[i];
                if (components.Count > 1)
                {
                    int componentsize = component.Count;
                    componentsizes.Add(componentsize);
                    if (componentsize > maxsize)
                    {
                        maxsize = componentsize;
                    }

                    if (componentsize < minsize)
                    {
                        minsize = componentsize;
                    }
                }


            }

            for (int i = 0; i < components.Count; i++)
            {
                HashSet<int> component = components[i];
                if (components.Count > 1)
                {
                    int componentsize = component.Count;

                    foreach (int index in component)
                    {
                        _outputvalues[index] = Remap(componentsize, minsize, maxsize, 0, 1);
                    }

                }

                else
                {
                    foreach (int index in component)
                    {
                        _outputvalues[index] = 1;
                    }
                }

            }
            return _outputvalues;
        }

        /// <summary>
        /// Remap the components to an array for graph viz colors
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="components"></param>
        /// <returns></returns>
        public float[] RemapComponentsToArray(IGraph graph, List<HashSet<int>> components)
        {
            float[] _outputvalues = new float[graph.VertexCount];
            int componentcount = components.Count;
            int increment = 1 / components.Count;
            for (int i = 0; i < components.Count; i++)
            {
                HashSet<int> component = components[i];

                if (components.Count > 1)
                {
                    foreach (int index in component)
                    {
                        _outputvalues[index] = Remap(i, 0, components.Count - 1, 0, 1);
                    }
                }

                else
                {
                    foreach (int index in component)
                    {
                        _outputvalues[index] = 1;
                    }
                }
            }

            return _outputvalues;
        }

    /// <summary>
    /// Remaps an array of values to a new range
    /// </summary>
    /// <param name="inputvalues"></param>
    /// <param name="minoutvalue"></param>
    /// <param name="maxoutvalue"></param>
    /// <returns></returns>
    public float[] RemapValues(int[] inputvalues, float minoutvalue, float maxoutvalue)
    {
        float[] outputvalues = new float[inputvalues.Length];
        int maxvalue = int.MinValue;
        int minvalue = int.MaxValue;

        for (int i = 0; i < inputvalues.Length; i++)
        {
            int input = inputvalues[i];

            if (input > maxvalue)
            {
                maxvalue = input;
            }
            if (input < minvalue)
            {
                minvalue = input;
            }
        }

        for (int i = 0; i < inputvalues.Length; i++)
        {
            outputvalues[i] = Remap((float)inputvalues[i], (float)minvalue, (float)maxvalue, minoutvalue, maxoutvalue);
        }

        return outputvalues;
    }

    /// <summary>
    /// Remaps an array of values to a new range
    /// </summary>
    /// <param name="inputvalues"></param>
    /// <param name="minoutvalue"></param>
    /// <param name="maxoutvalue"></param>
    /// <returns></returns>
    public float[] RemapValues(float[] inputvalues, float minoutvalue, float maxoutvalue)
    {
        float[] outputvalues = new float[inputvalues.Length];
        float maxvalue = float.MinValue;
        float minvalue = float.MaxValue;

        foreach (int input in inputvalues)
        {
            if (input > maxvalue)
            {
                maxvalue = input;
            }
            if (input < minvalue)
            {
                minvalue = input;
            }
        }

        for (int i = 0; i < inputvalues.Length; i++)
        {
            outputvalues[i] = Remap(inputvalues[i], minvalue, maxvalue, minoutvalue, maxoutvalue);
        }

        return outputvalues;
    }

    /// <summary>
    /// Remap a value from one range to another range
    /// </summary>
    /// <param name="value"></param>
    /// <param name="inputfrom"></param>
    /// <param name="inputto"></param>
    /// <param name="outputfrom"></param>
    /// <param name="outputto"></param>
    /// <returns></returns>
    private float Remap(float value, float inputfrom, float inputto, float outputfrom, float outputto)
    {
        return (value - inputfrom) / (inputto - inputfrom) * (outputto - outputfrom) + outputfrom;
    }

    /// <summary>
    /// (UNDIRECTED GRAPH) - Get number of connected components, closures and List of connected components in undirected graph (not including vertices with no edges)
    /// </summary>
    public void CountClosures(IGraph graph, out int componentCount, out int closureCount, out List<HashSet<int>> connectedComponents)
    {
        List<HashSet<int>> components = new List<HashSet<int>>();

        var stack = new Stack<int>();
        bool[] visited = new bool[graph.VertexCount];
        List<HashSet<int>> tempcomponents = new List<HashSet<int>>(graph.VertexCount);

        componentCount = 0;
        int edgesTraversed = 0;

        HashSet<int> component = new HashSet<int>();

        for (int i = 0; i < graph.VertexCount; i++)
        {
            if (visited[i])
                continue;

            visited[i] = true;
            stack.Push(i);

            do
            {
                var v0 = stack.Pop();
                component.Add(v0);

                foreach (var v1 in graph.GetVertexNeighbors(v0))
                {
                    if (visited[v1]) continue;

                    visited[v1] = true;
                    stack.Push(v1);
                    edgesTraversed++;
                }

            } while (stack.Count > 0);

            tempcomponents.Add(component);
            component = new HashSet<int>();
            componentCount++;
        }

        foreach (HashSet<int> set in tempcomponents)
        {
            if (set.Count > 1)
            {
                components.Add(set);
            }
        }

        connectedComponents = components;
        componentCount = components.Count;
        closureCount = CountEdges(graph) - edgesTraversed;
    }

    /// <summary>
    /// (DIRECTED GRAPH) -Get all strongly connected components of a directed graph.
    /// Based on Kosaraju's Algorithm:
    /// https://en.wikipedia.org/wiki/Strongly_connected_component
    /// https://en.wikipedia.org/wiki/Kosaraju%27s_algorithm
    /// </summary>
    /// <param name="graph"></param>
    /// <returns></returns>
    public List<HashSet<int>> GetSCC_DirectedGraph(IDigraph graph)
    {
        //stack contains vertices by finish time(reverse order)
        Stack<int> stack = new Stack<int>();
        //hashset contains visited vertices
        HashSet<int> visited = new HashSet<int>();
        //List contains all vertices
        List<int> vertices = new List<int>();
        //List of hashsets of vertices representing cycles
        List<HashSet<int>> connectedcomponents = new List<HashSet<int>>();

        //get all connected vertices
        //add all vertices with edges to unvisited list
        for (int i = 0; i < graph.VertexCount; i++)
        {
            List<int> neighborsout = (List<int>)graph.GetVertexNeighborsOut(i);
            List<int> neighborsin = (List<int>)graph.GetVertexNeighborsIn(i);
            if (neighborsout.Count > 0 || neighborsin.Count > 0)
            {
                vertices.Add(i);
            }
        }

        foreach (int vertex in vertices)
        {
            if (visited.Contains(vertex))
            {
                continue;
            }

            DFSUtil(vertex, visited, stack, graph);
        }

        //reverse the graph
        IDigraph reversegraph = ReverseGraph(graph);

        //empty visited hashset
        visited.Clear();

        while (stack.Count > 0)
        {
            //get + remove vertex int at top of the stack
            int vertex = stack.Pop();
            if (visited.Contains(vertex))
            {
                continue;
            }

            HashSet<int> componentset = new HashSet<int>();
            DFSUtilReverseGraph(vertex, visited, componentset, reversegraph);
            connectedcomponents.Add(componentset);

        }

        return connectedcomponents;






    }

    /// <summary>
    /// (DIRECTED GRAPH) -Get all cycles of a directed graph.
    /// Based on Johnson's Algorithm:
    /// https://en.wikipedia.org/wiki/Johnson%27s_algorithm
    /// https://github.com/mission-peace/interview/blob/master/src/com/interview/graph/AllCyclesInDirectedGraphJohnson.java
    /// </summary>
    /// <param name="graph"></param>
    /// <returns></returns>
    public List<List<int>> GetAllCycles_DirectedGraph(IDigraph graph)
    {
        HashSet<int> blockedset = new HashSet<int>();
        Dictionary<int, HashSet<int>> blockedmap = new Dictionary<int, HashSet<int>>();
        Stack<int> stack = new Stack<int>();
        List<List<int>> allcycles = new List<List<int>>();

        int startindex = 0;

        while (startindex <= graph.VertexCount)
        {
            IDigraph subgraph = CreateSubGraph(startindex, graph.VertexCount - 1, graph);
            List<HashSet<int>> sccs = GetSCC_DirectedGraph(subgraph);
            Nullable<int> potentialleastvertex = LeastIndexSCC(sccs, subgraph);

            if (potentialleastvertex != null)
            {
                int leastvertex = (int)potentialleastvertex;

                blockedset.Clear();
                blockedmap.Clear();
                GetComponentCycles(leastvertex, leastvertex, subgraph, blockedset, blockedmap, stack, allcycles);

                startindex = leastvertex + 1;

            }
            else
            {
                break;
            }
        }


        return allcycles;
    }

    /// <summary>
    /// (DIRECTED GRAPH) -Create a subgraph of an input graph between a start and end vertex (includes start and end vertex)
    /// </summary>
    /// <param name="startindex"></param>
    /// <param name="endvertex"></param>
    /// <param name="graph"></param>
    /// <returns></returns>
    public IDigraph CreateSubGraph(int startindex, int endvertex, IDigraph graph)
    {
        IDigraph subgraph = new Digraph(graph.VertexCount);
        for (int i = 0; i < graph.VertexCount; i++)
        {
            subgraph.AddVertex();
        }

        for (int i = startindex; i <= endvertex; i++)
        {
            List<int> neighbors = (List<int>)graph.GetVertexNeighborsOut(i);
            foreach (int neighbor in neighbors)
            {
                if (neighbor >= startindex && neighbor <= endvertex)
                {
                    subgraph.AddEdge(i, neighbor);
                }
            }
        }
        return subgraph;
    }

    /// <summary>
    /// (DIRECTED GRAPH) -Create a copy of the directed graph that is reversed
    /// </summary>
    /// <param name="graph"></param>
    /// <returns></returns>
    public IDigraph ReverseGraph(IDigraph graph)
    {
        IDigraph reversegraph = new Digraph(graph.VertexCount);
        for (int i = 0; i < graph.VertexCount; i++)
        {
            reversegraph.AddVertex();
        }

        for (int i = 0; i < graph.VertexCount; i++)
        {

            List<int> vertexneighbors = (List<int>)graph.GetVertexNeighborsOut(i);
            //for each vertex that is connected, create a reverse edge with its neighbors
            if (vertexneighbors.Count > 0)
            {
                foreach (int neighbor in vertexneighbors)
                {
                    reversegraph.AddEdge(neighbor, i);
                }
            }
        }

        return reversegraph;
    }

    #endregion

    #region Private Methods - (Undirected Graph) - Counting Closures 
    /// <summary>
    /// Returns the number of edges in the graph.
    /// </summary>
    private static int CountEdges(IGraph graph)
    {
        int edgeCount = 0;

        for (int i = 0; i < graph.VertexCount; i++)
            edgeCount += graph.GetVertexNeighbors(i).Where(j => j > i).Count();

        return edgeCount;
    }

    #endregion

    #region Private Methods - (Directed Graph) - Cycles 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="startvertex"></param>
    /// <param name="currentvertex"></param>
    /// <param name="graph"></param>
    /// <param name="blockedset"></param>
    /// <param name="blockedmap"></param>
    /// <param name="stack"></param>
    /// <param name="allcycles"></param>
    /// <returns></returns>
    private bool GetComponentCycles(int startvertex, int currentvertex, IDigraph graph, HashSet<int> blockedset, Dictionary<int, HashSet<int>> blockedmap, Stack<int> stack, List<List<int>> allcycles)
    {
        bool hascycle = false;
        stack.Push(currentvertex);
        blockedset.Add(currentvertex);

        if (graph.GetDegreeOut(currentvertex) > 0)
        {
            foreach (int neighbor in graph.GetVertexNeighborsOut(currentvertex))
            {
                if (neighbor == startvertex)
                {
                    List<int> cycle = new List<int>();
                    stack.Push(startvertex);
                    cycle.AddRange(stack);
                    cycle.Reverse();
                    stack.Pop();
                    allcycles.Add(cycle);
                    hascycle = true;
                }

                else if (!blockedset.Contains(neighbor))
                {
                    bool gotcycle = GetComponentCycles(startvertex, neighbor, graph, blockedset, blockedmap, stack, allcycles);
                    if (gotcycle == true)
                    {
                        hascycle = true;
                    }
                }
            }

            if (hascycle == true)
            {
                UnBlock(currentvertex, blockedset, blockedmap);
            }

            else
            {
                foreach (int neighbor in graph.GetVertexNeighborsOut(currentvertex))
                {
                    HashSet<int> bset = GetBlockedSet(neighbor, blockedmap);
                    bset.Add(currentvertex);
                }
            }

            stack.Pop();
            return hascycle;
        }
        return hascycle;
    }

    /// <summary>
    /// Creates graph consisting of strongly connected components only and then returns the
    /// minimum vertex among all the strongly connected components graph, ignores single vertex graph since it can't have a cycle
    /// potentially can return null. 
    /// </summary>
    /// <param name="sccs"></param>
    /// <param name="graph"></param>
    /// <returns></returns>
    private Nullable<int> LeastIndexSCC(List<HashSet<int>> sccs, IDigraph graph)
    {
        int min = int.MaxValue;
        Nullable<int> minvertex = null;
        HashSet<int> minscc = null;

        foreach (HashSet<int> component in sccs)
        {
            if (component.Count == 1)
            {
                continue;
            }

            foreach (int vertex in component)
            {
                if (vertex < min)
                {
                    min = vertex;
                    minvertex = vertex;
                    minscc = component;
                }
            }
        }

        if (minvertex == null)
        {
            return null;
        }

        IDigraph graphscc = new Digraph(graph.VertexCount);
        for (int i = 0; i < graph.VertexCount; i++)
        {
            graphscc.AddVertex();
        }
        for (int i = 0; i < graph.VertexCount; i++)
        {
            if (minscc.Contains(i))
            {
                if (graph.GetDegreeOut(i) > 0)
                {
                    foreach (int neighbor in graph.GetVertexNeighborsOut(i))
                    {
                        if (minscc.Contains(neighbor))
                        {
                            graphscc.AddEdge(i, neighbor);
                        }
                    }
                }
            }
        }

        Nullable<int> potentialminvertex = null;

        if (graphscc.GetDegreeOut(min) > 0 || graphscc.GetDegreeIn(min) > 0)
        {
            potentialminvertex = minvertex;
        }
        return potentialminvertex;
    }

    /// <summary>
    /// Retrieve the blockedset for a vertex from the blockedmap, add empty blockedset entry to blockedmap if doesn't exist
    /// </summary>
    /// <param name="vertex"></param>
    /// <param name="blockedmap"></param>
    /// <returns></returns>
    private HashSet<int> GetBlockedSet(int vertex, Dictionary<int, HashSet<int>> blockedmap)
    {
        if (!blockedmap.ContainsKey(vertex))
        {
            blockedmap.Add(vertex, new HashSet<int>());
        }

        return blockedmap[vertex];
    }

    /// <summary>
    /// Remove vertex from blockedSet and recursively unblock all the other vertices dependent on this vertex stored in the block map from blockedSet
    /// </summary>
    /// <param name="vertex"></param>
    /// <param name="blockedset"></param>
    /// <param name="blockedmap"></param>
    private void UnBlock(int vertex, HashSet<int> blockedset, Dictionary<int, HashSet<int>> blockedmap)
    {
        blockedset.Remove(vertex);
        if (blockedmap.ContainsKey(vertex))
        {
            foreach (int v in blockedmap[vertex])
            {
                if (blockedset.Contains(v))
                {
                    UnBlock(v, blockedset, blockedmap);
                }
            }
        }
        blockedmap.Remove(vertex);
    }
    #endregion

    #region Private Methods - (Directed Graph - SCC)

    /// <summary>
    /// Depth first search populates the stack with vertices ordered by finish time (vertex finishing last at top)
    /// </summary>
    /// <param name="vertex"></param>
    /// <param name="visited"></param>
    /// <param name="stack"></param>
    /// <param name="graph"></param>
    private void DFSUtil(int vertex, HashSet<int> visited, Stack<int> stack, IDigraph graph)
    {
        visited.Add(vertex);
        foreach (int neighbor in graph.GetVertexNeighborsOut(vertex))
        {
            if (visited.Contains(neighbor))
            {
                continue;
            }
            DFSUtil(neighbor, visited, stack, graph);
        }

        stack.Push(vertex);
    }

    /// <summary>
    /// Depth first search by vertex finish time in decreasing order on the reversed graph
    /// </summary>
    /// <param name="vertex"></param>
    /// <param name="visited"></param>
    /// <param name="set"></param>
    /// <param name="reversegraph"></param>
    private void DFSUtilReverseGraph(int vertex, HashSet<int> visited, HashSet<int> componentset, IDigraph reversegraph)
    {
        visited.Add(vertex);
        componentset.Add(vertex);

        foreach (int neighbor in reversegraph.GetVertexNeighborsOut(vertex))
        {
            if (visited.Contains(neighbor))
            {
                continue;
            }
            DFSUtilReverseGraph(neighbor, visited, componentset, reversegraph);
        }
    }


    #endregion

    #region Public Properties
        //commit
    #endregion




}

}









