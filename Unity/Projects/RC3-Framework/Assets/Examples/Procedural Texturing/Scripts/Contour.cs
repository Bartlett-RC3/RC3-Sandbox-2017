using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

using RC3.Unity.SDFDemo;

namespace RC3.Unity.ProceduralTexturing
{
    /// <summary>
    /// 
    /// </summary>
    public class Contour : ScalarField
    {
        [SerializeField] private ScalarField _source;
        [SerializeField] private float _range;

        
        /// <summary>
        /// 
        /// </summary>
        public override void BeforeEvaluate()
        {
            _source.BeforeEvaluate();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override float Evaluate(Vector3 point)
        {
            var d = _source.Evaluate(point);
            return Mathf.Repeat(d, _range) / _range;
        }
    }
}
