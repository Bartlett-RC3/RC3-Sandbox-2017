
/*
 * Notes
 */

using System;

namespace RC3.WFC
{


    /// <summary>
    /// 
    /// </summary>
    public abstract class TileSelector
    {
        protected TileModel _model;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public TileSelector(TileModel model)
        {
            if (model == null)
                throw new ArgumentNullException();

            _model = model;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public abstract int Select(int position);
    }
}
