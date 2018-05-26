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
    public abstract class ScalarField : MonoBehaviour, IField<float>
    {
        /// <summary>
        /// 
        /// </summary>
        public abstract void BeforeEvaluate();


        /// <summary>
        /// 
        /// </summary>
        public abstract float Evaluate(Vector3 point);
    }
}
