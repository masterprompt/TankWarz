using System;
using System.Collections.Generic;
using UnityEngine;
using Tangatek.Caching;

namespace Engine
{
    public class Avatar : CachableObject
    {
        #region Fields
        [System.NonSerialized]
        public new Transform transform;
        #endregion

        #region Monobehaviour
        public override void Awake()
        {
            base.Awake();
            transform = GetComponent<Transform>();
        }
        #endregion
    }
}
