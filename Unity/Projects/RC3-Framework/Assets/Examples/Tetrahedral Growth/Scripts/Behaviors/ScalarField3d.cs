using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SpatialSlur.Core;


namespace RC3.Unity.TetrahedralGrowth
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ScalarField3d : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        public abstract double Evaluate(Vec3d point);
    }
}
