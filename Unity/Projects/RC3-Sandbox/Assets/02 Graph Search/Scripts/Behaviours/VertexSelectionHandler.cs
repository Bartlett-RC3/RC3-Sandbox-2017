using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class VertexSelectionHandler : MonoBehaviour, ISelectionHandler
{
    [SerializeField]
    VertexSelection _sources;
    
    [SerializeField]
    SelectionMeshes _meshes;

    [SerializeField]
    SelectionMaterials _materials;

    private Vertex _vertex;
    private MeshFilter _filter;
    private MeshRenderer _renderer;
    private bool _selected;


    /// <summary>
    /// 
    /// </summary>
    public bool IsSelected
    {
        get { return _selected; }
    }


    /// <summary>
    /// 
    /// </summary>
    public void Start()
    {
        _vertex = GetComponent<Vertex>();
        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        OnDeselected();
    }


    /// <summary>
    /// 
    /// </summary>
    public void OnDeselected()
    {
        const float t = 0.5f;
        transform.localScale = new Vector3(t, t, t);

        _filter.sharedMesh = _meshes.Default;
        _renderer.sharedMaterial = _materials.Default;
        _sources.Indices.Remove(_vertex.Index);
        _selected = false;
    }


    /// <summary>
    /// 
    /// </summary>
    public void OnSelected()
    {
        const float t = 1.0f;
        transform.localScale = new Vector3(t, t, t);

        _filter.sharedMesh = _meshes.Selected;
        _renderer.sharedMaterial = _materials.Selected;
        _sources.Indices.Add(_vertex.Index);
        _selected = true;
    }
}
