using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Objects/SelectionMeshes")]
public class SelectionMeshes : ScriptableObject
{
    [SerializeField]
    private Mesh _selected;

    [SerializeField]
    private Mesh _default;


    /// <summary>
    /// 
    /// </summary>
    public Mesh Selected
    {
        get { return _selected; }
    }


    /// <summary>
    /// 
    /// </summary>
    public Mesh Default
    {
        get { return _default; }
    }
}
