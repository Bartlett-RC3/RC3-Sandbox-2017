using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using RC3.Graphs;
using RC3.WFC;
using RC3.Unity.WFCDemo;

public class GraphVisualizer : MonoBehaviour
{

    [SerializeField]
    private SharedAnalysisEdgeGraph _analysisgraph;
    [SerializeField]
    private GraphAnalysisManager _graphanalysis;
    [SerializeField]
    private Material _material;


    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private Mesh _mesh;
    private GameObject _meshObj;
    private bool _isvisible = true;
    [SerializeField]
    private float minedgesize = .5f;
    [SerializeField]
    private float maxedgesize = 3f;

    public RenderMode _vizmode = RenderMode.DepthFromSource;

    public enum RenderMode
    {
        DepthFromSource,
        Components,
        ComponentsSize

    }

    void Awake()
    {
        _meshObj = new GameObject("Graph Mesh Object");
        _meshObj.transform.parent = gameObject.transform;
        _meshFilter = _meshObj.AddComponent(typeof(MeshFilter)) as MeshFilter;
        _meshRenderer = _meshObj.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

        _mesh = new Mesh();
        _mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        _mesh.name = "GraphMesh";
        _meshFilter.mesh = _mesh;
    }

    public void CreateMesh()
    {
        _mesh = new Mesh();
        _mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        _mesh.name = "GraphMesh";
        _meshFilter.mesh = _mesh;

        _mesh.vertices = _analysisgraph.Vertices;
        _mesh.SetIndices(_analysisgraph.LineIndices.ToArray<int>(), MeshTopology.Lines, 0);
        SetVizColors();
    }

    public void SetVizColors()
    {
        if (_vizmode == RenderMode.DepthFromSource)
        {
            _meshRenderer.sharedMaterial = _material;

            Vector2[] uvs = new Vector2[_mesh.vertices.Length];
            for (int i = 0; i < _mesh.vertices.Length; i++)
            {
                Vector2 uv = new Vector2(_analysisgraph.NormalizedDepths[i], 0);
                uvs[i] = uv;
            }

            Vector2[] uv2s = new Vector2[_mesh.vertices.Length];
            float[] edgethicknessarray = RemapValues(_analysisgraph.NormalizedDepths, minedgesize, maxedgesize);
            for (int i = 0; i < _mesh.vertices.Length; i++)
            {
                Vector2 uv = new Vector2(edgethicknessarray[i], 0);
                uv2s[i] = uv;
            }
            _mesh.uv = uvs;
            _mesh.uv2 = uv2s;

            _mesh.vertices = _analysisgraph.Vertices;
            _mesh.SetIndices(_analysisgraph.LineIndices.ToArray<int>(), MeshTopology.Lines, 0);
        }

        if (_vizmode == RenderMode.Components)
        {
            _meshRenderer.sharedMaterial = _material;

            Vector2[] uvs = new Vector2[_mesh.vertices.Length];
            for (int i = 0; i < _mesh.vertices.Length; i++)
            {
                Vector2 uv = new Vector2(_analysisgraph.NormalizedComponents[i], 0);
                uvs[i] = uv;
            }

            Vector2[] uv2s = new Vector2[_mesh.vertices.Length];
            float[] edgethicknessarray = RemapValues(_analysisgraph.NormalizedComponents, minedgesize, maxedgesize);
            for (int i = 0; i < _mesh.vertices.Length; i++)
            {
                Vector2 uv = new Vector2(edgethicknessarray[i], 0);
                uv2s[i] = uv;
            }
            _mesh.uv = uvs;
            _mesh.uv2 = uv2s;

            _mesh.vertices = _analysisgraph.Vertices;
            _mesh.SetIndices(_analysisgraph.LineIndices.ToArray<int>(), MeshTopology.Lines, 0);
        }

        if (_vizmode == RenderMode.ComponentsSize)
        {
            _meshRenderer.sharedMaterial = _material;

            Vector2[] uvs = new Vector2[_mesh.vertices.Length];
            for (int i = 0; i < _mesh.vertices.Length; i++)
            {
                Vector2 uv = new Vector2(_analysisgraph.NormalizedComponentsBySize[i], 0);
                uvs[i] = uv;
            }

            Vector2[] uv2s = new Vector2[_mesh.vertices.Length];
            float[] edgethicknessarray = RemapValues(_analysisgraph.NormalizedComponentsBySize, minedgesize, maxedgesize);
            for (int i = 0; i < _mesh.vertices.Length; i++)
            {
                Vector2 uv = new Vector2(edgethicknessarray[i], 0);
                uv2s[i] = uv;
            }
            _mesh.uv = uvs;
            _mesh.uv2 = uv2s;

            _mesh.vertices = _analysisgraph.Vertices;
            _mesh.SetIndices(_analysisgraph.LineIndices.ToArray<int>(), MeshTopology.Lines, 0);
        }

    }

    /// <summary>
    /// Remaps an array of values to a new range
    /// </summary>
    /// <param name="inputvalues"></param>
    /// <param name="minoutvalue"></param>
    /// <param name="maxoutvalue"></param>
    /// <returns></returns>
    private float[] RemapValues(int[] inputvalues, float minoutvalue, float maxoutvalue)
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
    private float[] RemapValues(float[] inputvalues, float minoutvalue, float maxoutvalue)
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

    public GameObject MeshObject
    {
        get { return _meshObj; }
    }

    public RenderMode VizMode
    {
        get { return _vizmode; }
        set { _vizmode = value; }
    } 


    public bool IsVisible
    {
        get { return _isvisible; }
        set { _isvisible = true; }
    }
}
