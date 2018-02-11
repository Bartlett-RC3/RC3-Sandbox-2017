using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class VertexSelectionHandler : MonoBehaviour, ISelectionHandler
{
    [SerializeField]
    private VertexSelection _sources;
    
    [SerializeField]
    private SelectionMeshes _meshes;

    [SerializeField]
    private SelectionMaterials _materials;

    [SerializeField]
    private float _defaultScale = 0.1f;

    [SerializeField]
    private float _selectedScale = 0.2f;


    private VertexObject _vertex;
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
        _vertex = GetComponent<VertexObject>();
        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        OnDeselected();
    }


    /// <summary>
    /// 
    /// </summary>
    public void OnDeselected()
    {
        transform.localScale = new Vector3(_defaultScale, _defaultScale, _defaultScale);

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
        transform.localScale = new Vector3(_selectedScale, _selectedScale, _selectedScale);

        _filter.sharedMesh = _meshes.Selected;
        _renderer.sharedMaterial = _materials.Selected;
        _sources.Indices.Add(_vertex.Index);
        _selected = true;
    }
}
