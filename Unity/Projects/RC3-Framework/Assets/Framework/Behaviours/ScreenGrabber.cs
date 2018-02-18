using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

/*
 * Notes 
 */
 
namespace RC3.Unity
{
    /// <summary>
    /// 
    /// </summary>
    public class ScreenGrabber : MonoBehaviour
    {
        [SerializeField] private string _folder = "ScreenCaptures/00";
        [SerializeField] private string _format = "frame_{0:D3}.png";
        [SerializeField] private KeyCode _key = KeyCode.LeftShift;
        [SerializeField] private int _superSize = 1;

        private int _counter = 0;


        /// <summary>
        /// 
        /// </summary>
        void Start()
        {
            Directory.CreateDirectory(_folder);
        }


        /// <summary>
        /// 
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(_key))
            {
                var path = string.Format(Path.Combine(_folder, _format), _counter++);
                ScreenCapture.CaptureScreenshot(path, _superSize);
                Debug.Log($"Screenshot saved to {path}");
            }
        }
    }
}
