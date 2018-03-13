using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Notes
 */ 

namespace RC3.Unity
{
    /// <summary>
    /// 
    /// </summary>
    interface ISelectionHandler
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsSelected { get; }


        /// <summary>
        /// 
        /// </summary>
        void OnDeselected();


        /// <summary>
        /// 
        /// </summary>
        void OnSelected();
    }
}
