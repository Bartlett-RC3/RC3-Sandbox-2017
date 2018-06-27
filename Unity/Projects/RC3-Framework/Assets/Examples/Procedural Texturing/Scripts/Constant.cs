
using UnityEngine;

using RC3.Unity.SDFDemo;

namespace RC3.Unity.ProceduralTexturing
{
    public class Constant :ScalarField
    {
        [SerializeField] private float _value;


        /// <summary>
        /// 
        /// </summary>
        public override void BeforeEvaluate()
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override float Evaluate(Vector3 point)
        {
            return _value;
        }
    }
}
