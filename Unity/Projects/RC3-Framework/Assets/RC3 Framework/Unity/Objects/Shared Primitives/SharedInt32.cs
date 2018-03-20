using System;
using UnityEngine;

/*
 * Notes
 * 
 * Based on "ScriptableObject Variables" pattern as detailed in https://www.youtube.com/watch?v=raQ3iHhE_Kk (@ 00:17:40)
 */

namespace RC3.Unity
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(menuName = "Objects/Shared/Integer")]
    public class SharedInt32 : Shared<Int32> { }
}
