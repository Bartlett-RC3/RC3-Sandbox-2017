using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SpatialSlur.Mesh;

namespace RC3.Unity.TetrahedralGrowth
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class FaceEvaluator : MonoBehaviour
    {
        /// <summary>
        /// 
        /// </summary>
        public abstract double Evalutate(HeMesh3d.Face face);
    }
}
