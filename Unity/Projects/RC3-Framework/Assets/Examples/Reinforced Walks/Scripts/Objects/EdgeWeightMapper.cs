using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using SpatialSlur.Core;

/*
 * Notes
 */ 

namespace RC3.Unity.ReinforcedWalks
{
    [CreateAssetMenu(menuName = "RC3/ReinforcedWalks/EdgeWeightMapper")]
    public class EdgeWeightMapper : ScriptableObject
    {
        [SerializeField] private float _weight0;
        [SerializeField] private float _weight1;
        [SerializeField] private float _scale0;
        [SerializeField] private float _scale1;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        public float ToScale(float weight)
        {
            var t = SlurMath.Saturate(SlurMath.Normalize(weight, _weight0, _weight1));
            return SlurMath.Lerp(_scale0, _scale1, t);
        }
    }
}
