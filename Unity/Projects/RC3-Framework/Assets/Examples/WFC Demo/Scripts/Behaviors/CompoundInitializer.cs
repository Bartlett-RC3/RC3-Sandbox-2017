using RC3.WFC;
using UnityEngine;

namespace RC3.Unity.WFCDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class CompoundInitializer : TileModelInitializer
    {
        [SerializeField] private TileModelInitializer[] _initializers;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public override void Initialize(TileModel model)
        {
            foreach (var init in _initializers)
                init.Initialize(model);
        }
    }

}
