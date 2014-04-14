using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cameras
{
    [RequireComponent(typeof(Camera))]
    public class ActionCamera : MonoBehaviour
    {
        #region Fields
        [System.NonSerialized]
        public new Camera camera;
        [System.NonSerialized]
        public new Transform transform;
        public float padding = 2;
        public float sizeMin = 3;

        public float moveSmoothTime = 0.3f;
        public float zoomSmoothTime = 0.3f;
        private Vector3 moveVelocity;
        private float zoomVelocity;
        #endregion

        #region Monobehaviour
        public virtual void Awake()
        {
            transform = GetComponent<Transform>();
            camera = GetComponent<Camera>();
        }

        public virtual void Update()
        {
            if (ActionTarget.list.Count == 0) return;
            //  Camera size is half the total height of screen.
            var bounds = ActionTarget.GetBounds();
            

            var screen = new Vector2(Screen.width, Screen.height);
            //bound = new Vector2(bounds.size.x, bounds.size.z);


            //  Calculate screen aspect
            var aspect = (float)Screen.width / (float)Screen.height;


            var size = Vector2.one * sizeMin;
            size.y = ((bounds.size.z + (padding * 2)) * 0.5f);
            size.x = ((bounds.size.x + (padding * 2)) * 0.5f) / aspect;
            //Debug.Log("Size:" + size);
            camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, Mathf.Max(size.x, size.y, sizeMin), ref zoomVelocity, zoomSmoothTime);

            //  Move camera
            Vector3 targetPosition = new Vector3(bounds.center.x, transform.position.y, bounds.center.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref moveVelocity, moveSmoothTime);
            
            
        }
        #endregion
    }
}
