using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// Simple adjacency representation of a graph
/// </summary>
public class Graph
{
    private List<List<int>> _adj;


    /// <summary>
    /// 
    /// </summary>
    public Graph()
    {
        _adj = new List<List<int>>();
    }


    /// <summary>
    /// 
    /// </summary>
    public int VertexCount
    {
        get { return _adj.Count; }
    }


    /// <summary>
    /// 
    /// </summary>
    public void AddVertex()
    {
        _adj.Add(new List<int>());
    }


    /// <summary>
    /// Adds an edge between v0 and v1
    /// </summary>
    public void AddEdge(int v0, int v1)
    {
        _adj[v0].Add(v1);
        _adj[v1].Add(v0);
    }


    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<int> GetConnectedVertices(int vertex)
    {
        return _adj[vertex];
    }
}

