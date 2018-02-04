using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSelectionHandler : MonoBehaviour, ISelectionHandler
{
    [SerializeField]
    private SelectionMeshes _meshes;

    [SerializeField]
    private SelectionMaterials _materials;

    private Rigidbody _rigidbody;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    private float _scaleSelected = 1.0f;
    private float _scaleDefault = 0.5f;


    /// <summary>
    /// 
    /// </summary>
    void Start ()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        OnDeselected();
	}


    /// <summary>
    /// 
    /// </summary>
    public bool IsSelected
    {
        get { return _rigidbody.isKinematic; }
    }


    /// <summary>
    /// 
    /// </summary>
    public void OnSelected()
    {
        _rigidbody.isKinematic = true;
        _meshFilter.mesh = _meshes.Selected;
        _meshRenderer.material = _materials.Selected;
        transform.localScale = new Vector3(_scaleSelected, _scaleSelected, _scaleSelected);
    }


    /// <summary>
    /// 
    /// </summary>
    public void OnDeselected()
    {
        _rigidbody.isKinematic = false;
        _meshFilter.mesh = _meshes.Default;
        _meshRenderer.material = _materials.Default;
        transform.localScale = new Vector3(_scaleDefault, _scaleDefault, _scaleDefault);
    }
}
