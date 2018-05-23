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
    public class SignedBox : ScalarField
    {
        [SerializeField] private Vector3 _size;
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
        /// <param name="point"></param>
        /// <returns></returns>
        public override float Evaluate(Vector3 point)
        {
            // impl ref
            // http://iquilezles.org/www/articles/distfunctions/distfunctions.htm

            point = _toLocal.MultiplyPoint3x4(point);
            var d = Util.Abs(point) - _size;
            return Mathf.Min(Mathf.Max(d.x, Mathf.Max(d.y, d.z)), 0.0f) + Util.Max(d, 0.0f).magnitude;
        }
    }
}
