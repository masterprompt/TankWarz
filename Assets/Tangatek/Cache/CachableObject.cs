using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tangatek.Caching
{
    /// <summary>
    /// Extend this class for an object that should be cachable
    /// </summary>
    public class CachableObject : MonoBehaviour
    {
        #region Fields
        public int cacheId;
        public bool global;
        public int cacheSize = 3;
        /// <summary>
        /// State restoration.  Extend StateCollection and add to this to add more state monitoring
        /// </summary>
        protected StateController stateController = new StateController();
        private bool isPrefab = true;
        internal Group cacheGroup;
        internal Transform cachedTransform;
        #endregion

        #region Properties
        public int hashKey { get { return cacheId; } }
        #endregion

        #region Monobehaviour
        public virtual void Awake()
        {
            isPrefab = false;
            cachedTransform = GetComponent<Transform>();
            stateController.Add(new Renderers(this));
            stateController.Add(new Colliders(this));
            stateController.Add(new Monobehaviours(this));
            stateController.Add(new Animators(this));
            stateController.Add(new Rigidbodies(this));

        }
        public virtual void Reset()
        {
            cacheId = System.Guid.NewGuid().GetHashCode();
        }
        #endregion

        #region Retrieve / Return
        /// <summary>
        /// Retrieves an instance of this prefab from cache
        /// </summary>
        /// <returns></returns>
        public virtual CachableObject RetrieveFromCache()
        {
            if (!isPrefab) return null; //  Don't let them try and cache an instanced object, only prefabs can be returned
            return (global ? GlobalCache.Retrieve(this) : LocalCache.Retrieve(this));
        }
        public static CachableObject RetrieveFromCache(int hashKey)
        {
            var cachedObject = CachableObjects.Find(hashKey);
            if (cachedObject == null) return null;
            return cachedObject.RetrieveFromCache();
        }
        /// <summary>
        /// Sends this instance back into the cache from whence it came
        /// </summary>
        public virtual void ReturnToCache()
        {
            if (isPrefab) return;   //  Don't let them return a prefab to the cache, that just isnt right...
            if (cacheGroup == null) return;
            cacheGroup.Return(this);
        }
        #endregion

        #region CachableObject
        /// <summary>
        /// Called when ready to revive object
        /// </summary>
        public virtual void Waken()
        {
            if (cachedTransform.parent == cacheGroup.holder) cachedTransform.parent = null;
            stateController.Waken();
        }
        /// <summary>
        /// Called when object is put back into the cache
        /// </summary>
        public virtual void Hibernate()
        {
            stateController.Sleep();
            cachedTransform.parent = cacheGroup.holder;
            cachedTransform.ResetLocal();

        }
        #endregion
    }
}
