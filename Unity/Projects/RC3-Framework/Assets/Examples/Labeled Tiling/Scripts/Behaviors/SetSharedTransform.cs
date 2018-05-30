using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace RC3.Unity.LabeledTiling
{
    /// <summary>
    /// 
    /// </summary>
    public class SetSharedTransform : MonoBehaviour
    {
        [SerializeField] private SharedTransform _target;


        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            _target.Value = transform;
        }
    }
}
