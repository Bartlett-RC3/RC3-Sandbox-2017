using System.Collections;
using System.Collections.Generic;

using SpatialSlur.Core;
using SpatialSlur.Mesh;

namespace RC3.Unity.TetrahedralGrowth
{


    /// <summary>
    /// 
    /// </summary>
    public class DistanceToPointEvaluator : FaceEvaluator
    {
        /// <summary>
        /// 
        /// </summary>
        public override double Evalutate(HeMesh3d.Face face)
        {
            var p0 = face.GetBarycenter();
            var p1 = (Vec3d)transform.position;

            return p0.DistanceTo(p1);
        }
    }
}
