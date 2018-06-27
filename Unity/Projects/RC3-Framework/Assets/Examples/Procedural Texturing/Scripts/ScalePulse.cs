
using UnityEngine;

using RC3.Unity.SDFDemo;

namespace RC3.Unity.ProceduralTexturing
{
    public class ScalePulse : ScalarField
    {
        [SerializeField] private ScalarField _source;
        [SerializeField] private float _period = 1.0f; // seconds per cycle
        [SerializeField] private float _scale0 = 1.0f;
        [SerializeField] private float _scale1 = 2.0f;

        private float _scale;


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
            return _source.Evaluate(point) * _scale;

        }


        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            var t = Mathf.Cos(Time.time / _period * Mathf.PI * 2.0f) * 0.5f + 0.5f;
            _scale = Mathf.Lerp(_scale0, _scale1, t);
        }
    }
}
