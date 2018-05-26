using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * Notes
 */ 

namespace RC3
{
    /// <summary>
    /// 
    /// </summary>
    interface ISelectionHandler
    {
        bool IsSelected { get; }

        void OnDeselected();

        void OnSelected();
    }
}
