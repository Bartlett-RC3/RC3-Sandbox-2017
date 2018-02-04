using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(menuName = "Objects/VertexSelection")]
public class VertexSelection : ScriptableObject
{
    private HashSet<int> _indices;


    /// <summary>
    /// 
    /// </summary>
    public HashSet<int> Indices
    {
        get { return _indices; }
    }


    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        _indices = new HashSet<int>();
    }
}
