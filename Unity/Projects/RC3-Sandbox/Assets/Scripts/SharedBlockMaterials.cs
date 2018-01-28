using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Objects/SharedBlockMeshes")]
public class SharedBlockMeshes : ScriptableObject
{
    [SerializeField]
    private Mesh _selectedMesh;

    [SerializeField]
    private Mesh _defaultMesh;


    /// <summary>
    /// 
    /// </summary>
    public Mesh SelectedMesh
    {
        get { return _selectedMesh; }
    }


    /// <summary>
    /// 
    /// </summary>
    public Mesh DefaultMesh
    {
        get { return _defaultMesh; }
    }
}
