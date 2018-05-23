using System;
using UnityEngine;

namespace RC3.Unity.SDFDemo
{
    /// <summary>
    /// 
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector"></param>
        public static Vector3 Abs(Vector3 vector)
        {
            return new Vector3(
                Mathf.Abs(vector.x),
                Mathf.Abs(vector.y),
                Mathf.Abs(vector.z));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector"></param>
        public static Vector3 Max(Vector3 vector, float value)
        {
            return new Vector3(
                Mathf.Max(vector.x, 0.0f),
                Mathf.Max(vector.y, 0.0f),
                Mathf.Max(vector.z, 0.0f));
        }
    }
}
