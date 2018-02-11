using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class EdgeGraphManager : MonoBehaviour
{
    [SerializeField]
    private VertexSelection _sources;

    [SerializeField]
    private VertexObject _vertexPrefab;

    [SerializeField]
    private EdgeObject _edgePrefab;

    [SerializeField]
    private int _countX = 5;

    [SerializeField]
    private int _countY = 5;

    private EdgeGraph _graph;
    private VertexObject[] _vertices;
    private EdgeObject[] _edges;

    private float[] _vertexDistances;
    private float[] _edgeLengths;
    private float[] _edgeScale;

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        _graph = GraphFactory.CreateTriangleGrid(_countX, _countY);
        CreateVertices();
        CreateEdges();

        _vertexDistances = new float[_graph.VertexCount];
        _edgeLengths = new float[_graph.EdgeCount];
        _edgeScale = new float[_graph.EdgeCount];
        
        // initialize edge lengths
        for(int i = 0; i < _graph.EdgeCount; i++)
        {
            var e = _graph.GetEdge(i);
            var p0 = _vertices[e.Start].transform.localPosition;
            var p1 = _vertices[e.End].transform.localPosition;
            _edgeLengths[i] = Vector3.Distance(p0, p1) + Random.Range(0, 0.001f);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void CreateVertices()
    {
        _vertices = new VertexObject[_graph.VertexCount];
        int index = 0;

        for (int i = 0; i < _countY; i++)
        {
            float dx = (i % 2 == 0) ? 0.0f : 0.5f;

            for (int j = 0; j < _countX; j++)
            {
                // create vertex
                var v = Instantiate(_vertexPrefab, transform);
                v.Index = index;

                // set vertex attributes
                v.transform.localPosition = new Vector3(j + dx, 0, i);

                // cache it
                _vertices[index] = v;
                index++;
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void CreateEdges()
    {
        _edges = new EdgeObject[_graph.EdgeCount];
        int index = 0;

        for(int i = 0; i < _graph.EdgeCount; i++)
        {
            var e = _graph.GetEdge(i);

            var p0 = _vertices[e.Start].transform.position;
            var p1 = _vertices[e.End].transform.position;
            
            var obj = Instantiate(_edgePrefab, transform);
            obj.Index = i;

            var t = obj.transform;

            var dir = p1 - p0;
            var mag = dir.magnitude;

            // scale to edge length
            t.localScale = new Vector3(0.1f, mag * 0.5f, 0.1f);

            // translate to edge mid point
            t.localPosition = (p0 + p1) * 0.5f; //Vector3.Lerp(p0, p1, 0.5f);

            // align with edge vector
            t.localRotation = Quaternion.FromToRotation(t.up, dir);

            // cache it
            _edges[index] = obj;
            index++;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        // calculate distances when key is pressed
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GraphUtil.GetVertexDistances(_graph, _edgeLengths, _sources.Indices, _vertexDistances);

            /*
            // negate distances for walk to max
            for (int i = 0; i < _vertexDistances.Length; i++)
                _vertexDistances[i] *= -1.0f;
            */

            for (int i = 0; i < _edgeLengths.Length; i++)
                _edgeScale[i] = 0.0f;

            const float maxScale = 1.0f;
            const float scaleIncr = 0.01f;

            // shortes walk from each vertex
            for (int i = 0; i < _graph.VertexCount; i++)
            {
                var path = GraphUtil.WalkToMin(_graph, _vertexDistances, i);

                foreach (var ei in path)
                    _edgeScale[ei] += scaleIncr;
            }
            
            // scale cross section of each edge by traffic
            for (int i = 0; i < _edges.Length; i++)
            {
                var t = _edges[i].transform;

                var scale = t.localScale;
                var xz = Mathf.Min(_edgeScale[i], maxScale);
                t.localScale = new Vector3(xz, scale.y, xz);
            }
        }
    }


}
