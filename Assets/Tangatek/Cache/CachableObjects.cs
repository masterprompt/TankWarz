using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tangatek.Caching
{
    public static class CachableObjects
    {

        #region Fields
        private static Dictionary<int, CachableObject> catelog = new Dictionary<int, CachableObject>();
        public static List<CachableObject> available = new List<CachableObject>();
        #endregion

        #region Properties
        
        #endregion

        #region Constructors
        static CachableObjects()
        {
            available = Resources.LoadAll("", typeof(CachableObject)).Cast<CachableObject>().ToList();
            Debug.Log(available.Count.ToString() + " Cachable Objects");
            foreach (var item in available)
                if (!catelog.ContainsKey(item.hashKey))
                    catelog.Add(item.hashKey, item);
        }
        #endregion

        #region Find
        public static CachableObject Find(int hashKey)
        {
            CachableObject cachableObject = null;
            if (!catelog.TryGetValue(hashKey, out cachableObject)) return null;
            return cachableObject;
        }
        public static List<CachableObject> FindAll(bool isGlobal)
        {
            var list = new List<CachableObject>();
            foreach (var obj in available)
                if (obj.global == isGlobal)
                    list.Add(obj);
            return list;
        }
        #endregion
    }
}
