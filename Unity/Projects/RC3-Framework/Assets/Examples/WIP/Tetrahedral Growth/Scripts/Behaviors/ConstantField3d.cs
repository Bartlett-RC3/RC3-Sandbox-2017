using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SpatialSlur.Core;


namespace RC3.Unity.TetrahedralGrowth
{
    /// <summary>
    /// 
    /// </summary>
    public class ConstantField3d : ScalarField3d
    {
        [SerializeField] private double _value;


        /// <summary>
        /// 
        /// </summary>
        public override double Evaluate(Vec3d point)
        {
            return _value;
        }
    }
}
