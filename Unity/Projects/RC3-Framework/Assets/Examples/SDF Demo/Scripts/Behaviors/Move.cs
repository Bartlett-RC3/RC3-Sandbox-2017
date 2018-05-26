using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC3.Unity.SDFDemo
{
    public class Move : MonoBehaviour
    {
        [SerializeField] private Vector3 _speed;
        private Vector3 _current;


        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            _current = transform.localPosition;
        }


        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            _current += _speed * Time.deltaTime;
            transform.localPosition = _current;
        }
    }
}
