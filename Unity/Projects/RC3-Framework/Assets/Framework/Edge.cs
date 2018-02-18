using System;

/*
 * Notes
 */ 

namespace RC3
{
    /// <summary>
    /// 
    /// </summary>
    public struct Edge
    {
        private int _start;
        private int _end;


        /// <summary>
        /// 
        /// </summary>
        public int Start
        {
            get { return _start; }
        }


        /// <summary>
        /// 
        /// </summary>
        public int End
        {
            get { return _end; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public Edge(int start, int end)
        {
            _start = start;
            _end = end;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public int Other(int vertex)
        {
            return vertex == _start ? _end : vertex == _end ? _start : -1;
        }
    }
}