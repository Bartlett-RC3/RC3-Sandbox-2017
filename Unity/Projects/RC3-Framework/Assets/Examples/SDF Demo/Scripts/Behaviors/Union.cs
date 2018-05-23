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
    public class Union : ScalarField
    {
        [SerializeField] private ScalarField _sourceA;
        [SerializeField] private ScalarField _sourceB;


        /// <summary>
        /// 
        /// </summary>
        public override void BeforeEvaluate()
        {
            _sourceA.BeforeEvaluate();
            _sourceB.BeforeEvaluate();
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

            return Mathf.Min(_sourceA.Evaluate(point), _sourceB.Evaluate(point));
        }
    }
}
