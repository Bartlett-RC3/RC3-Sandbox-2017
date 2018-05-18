using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SpatialSlur.Core;

/*
 * Notes
 */
 
namespace RC3.Unity.ReinforcedWalks
{
    /// <summary>
    /// 
    /// </summary>
    public class EdgeObject : RC3.Unity.EdgeObject
    {
        #region Static

        private const float _minScale = 0.01f;

        #endregion


        private float _scale;


        /// <summary>
        /// 
        /// </summary>
        public float Scale
        {
            get { return _scale; }
            set
            {
                _scale = value;
                OnSetScale();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void OnSetScale()
        {
            if (_scale < _minScale)
            {
                if (gameObject.activeSelf)
                    gameObject.SetActive(false);
            }
            else
            {
                if (!gameObject.activeSelf)
                    gameObject.SetActive(true);

                var scale = transform.localScale;
                scale.x = scale.z = _scale;
                transform.localScale = scale;
            }
        }
    }
}