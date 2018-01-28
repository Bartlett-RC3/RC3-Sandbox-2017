using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// 
/// </summary>
interface ISelectionHandler
{
    bool IsSelected { get; }

    void OnDeselected();

    void OnSelected();
}
