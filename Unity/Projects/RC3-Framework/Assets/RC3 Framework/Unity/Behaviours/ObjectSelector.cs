using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Notes
 */

namespace RC3.Unity
{
    /// <summary>
    /// 
    /// </summary>
    public class ObjectSelector : MonoBehaviour
    {
        private Camera _camera;


        /// <summary>
        /// 
        /// </summary>
        private void Start()
        {
            _camera = GetComponent<Camera>();
        }


        /// <summary>
        /// 
        /// </summary>
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    var handler = hit.transform.GetComponent<ISelectionHandler>();

                    if (handler != null)
                    {
                        if (handler.IsSelected)
                            handler.OnDeselected();
                        else
                            handler.OnSelected();
                    }
                }
            }
        }
    }
}
