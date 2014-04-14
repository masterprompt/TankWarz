using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tangatek.Caching
{
    public sealed class Group
    {
        #region Fields
        internal Transform holder;
        private CachableObject prefab;
        private Queue<CachableObject> list = new Queue<CachableObject>();
        #endregion

        #region Constructors
        internal Group(Transform parent, CachableObject prefab)
        {
            //  Create holder
            holder = (new GameObject(prefab.name)).transform;
            holder.parent = parent;

            //  Fill the cache
            this.prefab = prefab;
            for (var i = 0; i < prefab.cacheSize; i++)
                Return(Instantiate());
        }
        #endregion

        #region Retrieve / Return
        internal CachableObject Retrieve()
        {
            if (list.Count > 0) return list.Dequeue();
            return Instantiate();
        }
        internal void Return(CachableObject cachableObject)
        {
            if (cachableObject == null) return;
            cachableObject.Hibernate();
            list.Enqueue(cachableObject);
        }
        #endregion

        #region Instantiate
        private CachableObject Instantiate()
        {
            if (prefab == null) return null;
            var cachableObject = GameObject.Instantiate(prefab, holder.position, holder.rotation) as CachableObject;
            if (cachableObject != null)
            {
                cachableObject.cacheGroup = this;
                cachableObject.name = prefab.name;
            }
            return cachableObject;
        }
        #endregion
    }
}
