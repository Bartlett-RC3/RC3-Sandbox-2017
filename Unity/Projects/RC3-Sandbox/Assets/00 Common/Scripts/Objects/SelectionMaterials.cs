using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Objects/SelectionMaterials")]
public class SelectionMaterials : ScriptableObject
{
    [SerializeField]
    private Material _selected;

    [SerializeField]
    private Material _default;


    /// <summary>
    /// 
    /// </summary>
    public Material Selected
    {
        get { return _selected; }
    }


    /// <summary>
    /// 
    /// </summary>
    public Material Default
    {
        get { return _default; }
    }
}
