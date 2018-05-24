using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using SpatialSlur.Field;

namespace RC3.Unity.SDFDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class ImplicitSurface: ScalarField
    {
        [SerializeField] private ImplicitSurfaceType _type;
        private Matrix4x4 _toLocal;


        /// <summary>
        /// 
        /// </summary>
        public override void BeforeEvaluate()
        {
            _toLocal = transform.worldToLocalMatrix;
        }


        /// <summary>
        /// 
        /// </summary>
        public override float Evaluate(Vector3 point)
        {
            // impl ref
            // http://iquilezles.org/www/articles/distfunctions/distfunctions.htm

            point = _toLocal.MultiplyPoint3x4(point);

            switch (_type)
            {
                case ImplicitSurfaceType.Diamond:
                    return (float)ImplicitSurfaces.Diamond(point);
                case ImplicitSurfaceType.Gyroid:
                    return (float)ImplicitSurfaces.Gyroid(point);
                case ImplicitSurfaceType.HybridPW:
                    return (float)ImplicitSurfaces.HybridPW(point);
                case ImplicitSurfaceType.IWP:
                    return (float)ImplicitSurfaces.IWP(point);
                case ImplicitSurfaceType.Neovius:
                    return (float)ImplicitSurfaces.Neovius(point);
            }

            throw new System.NotImplementedException();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public enum ImplicitSurfaceType
    {
        Diamond,
        Gyroid,
        HybridPW,
        IWP,
        Neovius
    }
}
