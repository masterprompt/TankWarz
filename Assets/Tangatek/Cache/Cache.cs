using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tangatek.Caching
{
    public abstract class Cache : MonoBehaviour
    {
        #region Fields
        private Dictionary<int, Group> catelog = new Dictionary<int, Group>();
        #endregion

        #region Monobehaviour
        public virtual void Awake()
        {
            //  Pre-cache here
            foreach (var cachableObject in cachableObjects)
            {
                if (catelog.ContainsKey(cachableObject.hashKey)) continue;
                catelog.Add(cachableObject.hashKey, new Group(transform, cachableObject));
            }
        }
        #endregion

        #region Cache
        protected abstract List<CachableObject> cachableObjects { get; }
        #endregion

        #region Retrieve / Return
        protected CachableObject RetrieveInstance(CachableObject prefab)
        {
            Group group;
            if (!catelog.TryGetValue(prefab.hashKey, out group)) return null;
            return group.Retrieve();
        }
        #endregion
    }
}
