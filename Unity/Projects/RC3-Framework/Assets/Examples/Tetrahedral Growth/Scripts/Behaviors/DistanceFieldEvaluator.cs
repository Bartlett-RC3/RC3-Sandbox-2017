using System;
using UnityEngine;
using SpatialSlur.Mesh;
using RC3.Unity.SDFDemo;


namespace RC3.Unity.TetrahedralGrowth
{
    /// <summary>
    /// 
    /// </summary>
    public class DistanceFieldEvaluator : FaceEvaluator
    {
        [SerializeField] private ScalarField _distanceField;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        public override double Evalutate(HeMesh3d.Face face)
        {
            var p0 = transform.TransformPoint((Vector3)face.GetBarycenter());
            return Math.Abs(_distanceField.Evaluate(p0));
            //return _distanceField.Evaluate(p0);
        }
    }
}
