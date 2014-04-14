using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    [AddComponentMenu("Engine/View/View (Camera)")]
    [RequireComponent(typeof(UnityEngine.Camera))]
    public class View : MonoBehaviour
    {
        #region Fields
        private static List<View> activeViews = new List<View>();
        //[System.NonSerialized]
        public new Camera camera;
        [System.NonSerialized]
        public new Transform transform;
        #endregion

        #region Monobehaviour
        public void Awake()
        {
            transform = GetComponent<Transform>();
            camera = GetComponent<Camera>();
        }
        public void OnEnable()
        {
            if (camera == null) return;
            camera.enabled = true;
            if (!activeViews.Contains(this)) activeViews.Add(this);
            if (Camera.main != null) Camera.main.enabled = false;
        }
        public void OnDisable()
        {
            if (camera == null) return;
            camera.enabled = false;
            activeViews.Remove(this);
            if (activeViews.Count <= 0 && Camera.main != null) Camera.main.enabled = true;
        }
        public void OnDestroy()
        {
            activeViews.Remove(this);
        }
        #endregion
    }
}
