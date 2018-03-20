
/*
 * Notes
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC3.WFC
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Clear<T>(this T[] array)
        {
            Array.Clear(array, 0, array.Length);
        }


        /// <summary>
        /// 
        /// </summary>
        public static void Set<T>(this T[] array, T value)
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
        }


        /// <summary>
        /// 
        /// </summary>
        public static void Set<T>(this T[] array, int[] indices, T value)
        {
            for(int i = 0; i < indices.Length; i++)
                array[indices[i]] = value;
        }


        /// <summary>
        /// 
        /// </summary>
        public static void Swap<T>(this T[] items, int i, int j)
        {
            var t = items[i];
            items[i] = items[j];
            items[j] = t;
        }


        /// <summary>
        /// 
        /// </summary>
        public static void Shuffle<T>(this T[] array, Random random)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = random.Next(i);
                array.Swap(i, j);
            }
        }
    }
}
