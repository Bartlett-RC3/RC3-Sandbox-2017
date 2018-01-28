using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Notes
 */

namespace AppBase
{
    /// <summary>
    /// 
    /// </summary>
    public class Pan : MonoBehaviour
    {
        [SerializeField]
        private float _sensitivity = 1.0f;

        [SerializeField]
        private float _stiffness = 5.0f;


        private Transform _pivot;
        private Vector3 _position;
        

        /// <summary>
        /// 
        /// </summary>
        void Start()
        {
            _pivot = transform.parent;
            _position = _pivot.position;
        }


        /// <summary>
        /// 
        /// </summary>
        private void LateUpdate()
        {
            if(Input.GetMouseButton(2))
            {
                var dx = Input.GetAxis("Mouse X") * _sensitivity;
                var dy = Input.GetAxis("Mouse Y") * _sensitivity;
                _position += transform.TransformVector(-dx, -dy, 0.0f);
            }
            
            _pivot.position = Vector3.Lerp(_pivot.position, _position, Time.deltaTime * _stiffness);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(Vector3 position)
        {
            _position = position;
        }
    }
}
