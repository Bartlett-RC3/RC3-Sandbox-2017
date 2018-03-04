using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Notes
 * 
 * Based on "ScriptableObject Variables" pattern as detailed in https://www.youtube.com/watch?v=raQ3iHhE_Kk (@ 00:17:40)
 */

namespace RC3
{
    /// <summary>
    /// 
    /// </summary>
    public class Shared<T> : ScriptableObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shared"></param>
        public static implicit operator T(Shared<T> shared)
        {
            return shared.Value;
        }


        [SerializeField] private T _value;


        /// <summary>
        /// 
        /// </summary>
        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Objects/Shared/Float")]
    public class SharedFloat : Shared<float> { }


    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Objects/Shared/Integer")]
    public class SharedInt : Shared<int> { }


    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Objects/Shared/String")]
    public class SharedString : Shared<string> { }


    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Objects/Shared/Color")]
    public class SharedColor : Shared<Color> { }


    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Objects/Shared/Vector3")]
    public class SharedVector3 : Shared<Vector3> { }
}
