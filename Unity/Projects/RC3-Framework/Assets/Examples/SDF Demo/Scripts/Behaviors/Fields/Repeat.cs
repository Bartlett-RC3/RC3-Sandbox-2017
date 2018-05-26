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
    public class Repeat : ScalarField
    {
        [SerializeField] private ScalarField _source;
        [SerializeField] private Vector3 _offset;

        private Matrix4x4 _toLocal;


        /// <summary>
        /// 
        /// </summary>
        public override void BeforeEvaluate()
        {
            _toLocal = transform.worldToLocalMatrix;
            _source.BeforeEvaluate();
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
            point = Mod(point, _offset) - 0.5f * _offset;
            return _source.Evaluate(point);
        }



        private static Vector3 Mod(Vector3 p, Vector3 d)
        {
            return new Vector3(
                Mathf.Repeat(p.x, d.x),
                Mathf.Repeat(p.y, d.y),
                Mathf.Repeat(p.z, d.z));
        }
    }
}
