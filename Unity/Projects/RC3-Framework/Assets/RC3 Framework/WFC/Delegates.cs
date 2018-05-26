
/*
 * Notes
 */ 

using System;

namespace RC3.WFC
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <param name="tile"></param>
    public delegate void AssignedCallback(int position, int tile);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <param name="size"></param>
    public delegate void ReducedCallback(int position, int size);
}
