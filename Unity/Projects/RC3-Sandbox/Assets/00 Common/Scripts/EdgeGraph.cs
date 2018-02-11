using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 
/// </summary>
public class EdgeGraph
{
    private List<List<int>> _adj;
    private List<Edge> _edges;


    /// <summary>
    /// 
    /// </summary>
    public EdgeGraph()
    {
        _adj = new List<List<int>>();
        _edges = new List<Edge>();
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
    public int EdgeCount
    {
        get { return _edges.Count; }
    }


    /// <summary>
    /// 
    /// </summary>
    public void AddVertex()
    {
        _adj.Add(new List<int>());
    }


    /// <summary>
    /// Adds an edge between start and end
    /// </summary>
    public void AddEdge(int start, int end)
    {
        var e = new Edge(start, end);
        var ei = _edges.Count;

        // add edge to vertex lists
        _adj[start].Add(ei);
        _adj[end].Add(ei);
        
        // add edge to edge list
        _edges.Add(e);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Edge GetEdge(int index)
    {
        return _edges[index];
    }


    /// <summary>
    /// Returns all edges incident to the given vertex.
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    public IEnumerable<int> GetIncidentEdges(int vertex)
    {
        return _adj[vertex];
    }

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    public IEnumerable<int> GetConnectedVertices(int vertex)
    {
        foreach(var ei in _adj[vertex])
            yield return _edges[ei].Other(vertex);
    }
}


/// <summary>
/// 
/// </summary>
public struct Edge
{
    private int _start;
    private int _end;


    /// <summary>
    /// 
    /// </summary>
    public int Start
    {
        get { return _start; }
    }


    /// <summary>
    /// 
    /// </summary>
    public int End
    {
        get { return _end; }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public Edge(int start, int end)
    {
        _start = start;
        _end = end;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    public int Other(int vertex)
    {
        if (vertex == _start)
            return _end;
        else if (vertex == _end)
            return _start;

        throw new ArgumentException("This edge isn't connected to the given vertex!");
    }
}