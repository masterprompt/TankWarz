using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tangatek.Caching
{
    /// <summary>
    /// Application cache that will be kept regardless of scene load
    /// </summary>
    [AddComponentMenu("Engine/Cache/Application Cache")]
    public sealed class GlobalCache : Cache
    {
        #region Properties
        private static GlobalCache _instance;
        /// <summary>
        /// Current instance of cache
        /// </summary>
        private static GlobalCache instance
        {
            get
            {
                if (_instance == null) _instance = (GlobalCache)GameObject.FindObjectOfType(typeof(GlobalCache));
                if (_instance == null) _instance = (new GameObject("Global Cache")).AddComponent<GlobalCache>();
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
            DontDestroyOnLoad(this);
        }
        public void Start()
        {
            //  Delete us if we aren't the choosen one to avoid confusion
            if (_instance == this) return;
            Destroy(this.gameObject);
        }
        public void Reset()
        {
            this.name = "Global Cache";
        }
        #endregion

        #region Cache
        protected override List<CachableObject> cachableObjects { get { return CachableObjects.FindAll(true); } }
        #endregion

        #region Retrieve / Return
        public static CachableObject Retrieve(CachableObject prefab)
        {
            return instance.RetrieveInstance(prefab);
        }
        #endregion
    }
}
