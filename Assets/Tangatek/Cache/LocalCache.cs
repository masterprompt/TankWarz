using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tangatek.Caching
{
    /// <summary>
    /// Scene cache that will be destroyed on scene load
    /// </summary>
    [AddComponentMenu("Engine/Cache/Scene Cache")]
    public sealed class LocalCache : Cache
    {
        #region Properties
        private static LocalCache _instance;
        /// <summary>
        /// Current instance of cache
        /// </summary>
        private static LocalCache instance
        {
            get
            {
                if (_instance == null) _instance = (LocalCache)GameObject.FindObjectOfType(typeof(LocalCache));
                if (_instance == null) _instance = (new GameObject("Local Cache")).AddComponent<LocalCache>();
                return _instance;
            }
        }
        #endregion

        #region Monobehaviour
        public override void Awake()
        {
            //  Only allow 1
            if (_instance != null && _instance != this) return;
            if (_instance == null) _instance = this;
            base.Awake();
        }
        public void Start()
        {
            //  Delete us if we aren't the choosen one to avoid confusion
            if (_instance == this) return;
            Destroy(this.gameObject);
        }
        public void OnDestroy()
        {
            if (_instance == this) _instance = null;
        }
        public void Reset()
        {
            this.name = "Local Cache";
        }
        #endregion

        #region Cache
        protected override List<CachableObject> cachableObjects { get { return CachableObjects.FindAll(false); } }
        #endregion

        #region Retrieve / Return
        public static CachableObject Retrieve(CachableObject prefab)
        {
            return instance.RetrieveInstance(prefab);
        }
        #endregion
    }
}
