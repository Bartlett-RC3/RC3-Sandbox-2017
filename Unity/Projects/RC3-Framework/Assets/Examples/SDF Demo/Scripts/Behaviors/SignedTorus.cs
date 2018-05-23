using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace RC3.Unity.SDFDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class SignedTorus: ScalarField
    {
        [SerializeField] private float _radius0 = 1.0f;
        [SerializeField] private float _radius1 = 0.2f;
        private Matrix4x4 _toLocal;


        /// <summary>
        /// 
        /// </summary>
        public override void BeforeEvaluate()
        {
            _toLocal = transform.worldToLocalMatrix;
        }


        /// <summary>
        /// 
        /// </summary>
        public override float Evaluate(Vector3 point)
        {
            // impl ref
            // http://iquilezles.org/www/articles/distfunctions/distfunctions.htm

            point = _toLocal.MultiplyPoint3x4(point);
            var d = new Vector2(Magnitude(point.x, point.z) - _radius0, point.y);
            return d.magnitude - _radius1;
        }


        /// <summary>
        /// 
        /// </summary>
        private static float Magnitude(float x, float y)
        {
            return Mathf.Sqrt(x * x + y * y);
        }
    }
}
