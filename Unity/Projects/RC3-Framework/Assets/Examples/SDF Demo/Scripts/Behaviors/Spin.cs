using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RC3.Unity.SDFDemo
{
    public class Spin : MonoBehaviour
    {
        [SerializeField] private Vector3 _speed;
        private Quaternion _current;


        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            _current = transform.localRotation;
        }


        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            _current = Quaternion.Euler(_speed * Time.deltaTime) * _current;
            transform.localRotation = _current;
        }
    }
}
