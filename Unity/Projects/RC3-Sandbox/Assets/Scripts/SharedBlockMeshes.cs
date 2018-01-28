using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Objects/SharedBlockMaterials")]
public class SharedBlockMaterials : ScriptableObject
{
    [SerializeField]
    private Material _selectedMaterial;

    [SerializeField]
    private Material _defaultMaterial;


    /// <summary>
    /// 
    /// </summary>
    public Material SelectedMaterial
    {
        get { return _selectedMaterial; }
    }


    /// <summary>
    /// 
    /// </summary>
    public Material DefaultMaterial
    {
        get { return _defaultMaterial; }
    }
}
