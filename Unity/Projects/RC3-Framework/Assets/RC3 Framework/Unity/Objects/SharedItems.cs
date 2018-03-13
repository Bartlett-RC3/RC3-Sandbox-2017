using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Notes
 */

namespace RC3.Unity
{
    /// <summary>
    /// 
    /// </summary>
    public class SharedItems<T> : ScriptableObject
    {
        [SerializeField]
        private T[] _items;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public T this[int i]
        {
            get { return _items[i]; }
        }


        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get { return _items.Length; }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Objects/SharedItems/Meshes")]
    public class SharedMeshes : SharedItems<Mesh> { }


    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Objects/SharedItems/Materials")]
    public class SharedMaterials : SharedItems<Material> { }


    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Objects/SharedItems/Floats")]
    public class SharedFloats : SharedItems<float> { }
}