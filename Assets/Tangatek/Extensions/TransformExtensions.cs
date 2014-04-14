using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tangatek
{
	public static class TransformExtensions
    {
        #region Minic
        /// <summary>
        /// Mimic's the local transform and rotation of target transform (without scale)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void ImitateLocal(this Transform source, Transform target)
        {
            source.ImitateLocal(target, false);
        }
        public static void ImitateLocal(this Transform source, Transform target, bool scale)
        {
            source.localPosition = target.localPosition;
            source.localRotation = target.localRotation;
            if (scale) source.localScale = target.localScale;
        }
        public static void ImitateGlobal(this Transform source, Transform target)
        {
            source.ImitateGlobal(target, false);
        }
        public static void ImitateGlobal(this Transform source, Transform target, bool scale)
        {
            source.position = target.position;
            source.rotation = target.rotation;
            if (scale) source.localScale = target.localScale;
        }
        #endregion

        #region Reset
        /// <summary>
        /// Resets the local coordinates for transform (without scale)
        /// </summary>
        /// <param name="source"></param>
        public static void ResetLocal(this Transform source)
        {
            source.ResetLocal(false);
        }
        public static void ResetLocal(this Transform source, bool scale)
        {
            source.localPosition = Vector3.zero;
            source.localRotation = Quaternion.identity;
            if (scale) source.localScale = Vector3.one;
        }
        #endregion

        #region PathHash
        /// <summary>
        /// Hash code for full path
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int PathHash(this Transform source)
        {
            return source.FullPath().GetHashCode();
        }
        #endregion

        #region FullPath
        /// <summary>
        /// Full path to transform (parent/parent/child)
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string FullPath(this Transform source)
        {
            return ( source.transform.parent == null ? "" : source.transform.parent.FullPath() + "/" ) + source.name;
        }
        #endregion
    }
}
