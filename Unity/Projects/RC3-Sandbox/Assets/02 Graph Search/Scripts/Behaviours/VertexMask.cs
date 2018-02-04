using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexMask : MonoBehaviour
{
    [SerializeField]
    private VertexSelection _ignored;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        var v = other.GetComponent<Vertex>();

        if (v != null)
            _ignored.Indices.Add(v.Index);
    }
    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerExit(Collider other)
    {
        var v = other.GetComponent<Vertex>();

        if (v != null)
            _ignored.Indices.Remove(v.Index);
    }
}
