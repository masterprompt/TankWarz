using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Tangatek;

namespace Engine
{
    /// <summary>
    /// Houses all child views
    /// </summary>
    [AddComponentMenu("Engine/View/Viewport (View Collection)")]
    public class Viewport : MonoBehaviour
    {
        #region Fields
        [System.NonSerialized]
        public new Transform transform;
        private List<View> views = new List<View>();
        #endregion

        #region Monobehaviour
        public void Awake()
        {
            transform = GetComponent<Transform>();
            views = GetComponentsInChildren<View>().ToList();
        }
        public void OnEnable()
        {
            foreach (var view in views)
                view.enabled = true;
        }
        public void OnDisable()
        {
            foreach (var view in views)
                view.enabled = false;
        }
        #endregion

        #region GetViewportInChildren
        //public static Viewport GetViewportInChildren(
        #endregion
    }

    public static class ViewportExtensions
    {
        /// <summary>
        /// Finds viewport in children or creates one if it doesnt exist
        /// </summary>
        /// <param name="monoBehaviour"></param>
        /// <returns></returns>
        public static Viewport GetViewportInChildren(this MonoBehaviour monoBehaviour)
        {
            var viewport = monoBehaviour.GetComponentInChildren<Viewport>();
            if (viewport == null)
            {
                viewport = (new GameObject("Viewport")).AddComponent<Viewport>();
                viewport.transform.parent = monoBehaviour.transform;
                viewport.transform.ResetLocal();
            }
            return viewport;
        }
    }
}
