using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tangatek
{
	public static class Vector3Extensions
    {
        #region StripY
        /// <summary>
        /// Strips Y component
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Vector3 StripY(this Vector3 source)
        {
            return new Vector3(source.x, 0, source.z);
        }
        #endregion
	}
}
