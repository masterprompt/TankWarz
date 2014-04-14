using System.Collections.Generic;
using UnityEngine;
using Tangatek.Caching;

namespace Engine
{
    public static class Avatars
    {
        #region Fields
        private static List<Avatar> available = new List<Avatar>();
        #endregion

        #region Constructors
        static Avatars()
        {
            foreach (var obj in CachableObjects.available)
                if (obj is Avatar)
                    available.Add((Avatar)obj);
        }
        #endregion

        #region Random
        public static Avatar random
        {
            get
            {
                if (available.Count == 0) return null;
                if (available.Count == 1) return available[0];
                return available[Random.Range(0,available.Count-1)];
            }
        }
        #endregion
    }
}
