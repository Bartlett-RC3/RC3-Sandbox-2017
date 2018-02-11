using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class GraphManager : MonoBehaviour
{
    [SerializeField]
    private VertexSelection _sources;

    [SerializeField]
    private VertexSelection _ignored;

    [SerializeField]
    private VertexObject _vertexPrefab;

    [SerializeField]
    private int _countX = 5;

    [SerializeField]
    private int _countY = 5;
    
    [SerializeField, Range(0.0f, 1.0f)]
    private float _depthScale = 0.1f;
    
    private Graph _graph;
    private VertexObject[] _vertices;
    private int[] _depths;


	/// <summary>
    /// 
    /// </summary>
	void Start ()
    {
        _graph = GraphFactory.CreateGrid(_countX, _countY);
        _depths = new int[_graph.VertexCount];
        CreateVertices();
        //_ignored = CreateIgnored();

        // center on origin
        transform.localPosition = new Vector3(-_countX * 0.5f, 0.0f, -_countY * 0.5f);
	}


    /// <summary>
    /// 
    /// </summary>
    private void CreateVertices()
    {
        _vertices = new VertexObject[_graph.VertexCount];
        int index = 0;

        for(int i = 0; i < _countY; i++)
        {
            for(int j = 0; j < _countX; j++)
            {
                // create vertex
                var v = Instantiate(_vertexPrefab, transform);

                // set vertex attributes
                v.transform.localPosition = new Vector3(j, 0, i);
                v.Index = index;

                // cache it
                _vertices[index] = v;
                index++;
            }
        }
    }

	
	/// <summary>
    /// 
    /// </summary>
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space) && _sources.Indices.Count > 0)
            UpdateVertexDepths();
	}


    /// <summary>
    /// 
    /// </summary>
    private void UpdateVertexDepths()
    {
        GraphUtil.GetVertexDepths(_graph, _sources.Indices, _ignored.Indices, _depths);

        // map vertex depth to position.y
        for(int i = 0; i < _vertices.Length; i++)
        {
            var vt = _vertices[i].transform;
            var p = vt.localPosition;

            p.y = _depths[i] * _depthScale;
            vt.localPosition = p;
        }
    }

    
    /*
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    int[] CreateIgnored()
    {
        int dx = (int)(_countX * 0.75);
        int dy = (int)(_countY * 0.75);

        //int dx = _countX / 2;
        //int dy = _countY / 2;

        int[] result = new int[dx * dy];
        int index = 0;

        for(int i = 0; i < dy; i++)
        {
            for(int j = 0; j < dx; j++)
            {
                result[index] = j + i * _countX;
                index++;
            }
        }

        return result;
    }
    */


    /*
    /// <summary>
    /// 
    /// </summary>
    private IEnumerable<int> GetSourceIndices()
    {
        foreach(var src in _sources)
        {
            var v = GetClosestVertex(src.position);
            yield return v;
        }
    }
    

    /// <summary>
    /// 
    /// </summary>
    private int GetClosestVertex(Vector3 point)
    {
        int minVert = -1;
        float minDist = float.MaxValue;

        for(int i = 0; i< _blocks.Length; i++)
        {
            Vector3 d = _blocks[i].transform.position - point;
            var m = d.magnitude;

            if(m < minDist)
            {
                minVert = i;
                minDist = m;
            }
        }

        return minVert;
    }
    */
}
