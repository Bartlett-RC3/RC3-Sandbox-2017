using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class Vertex : MonoBehaviour
{
    [SerializeField]
    private int _index;


    /// <summary>
    /// 
    /// </summary>
    public int Index
    {
        get { return _index; }
        set { _index = value; }
    }
}
