using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Tangatek.ViewManagement
{


    [AddComponentMenu("ViewSpace/View Space")]
    public class ViewSpace : MonoBehaviour
    {


        #region Fields
        public View currentView;
        public Modal currentModal;
        private View[] _views = new View[0];
        #endregion

        #region Properties
        /// <summary>
        /// Returns true if any view is busy doing something
        /// </summary>
        public bool busy { get { foreach (var view in _views) if (view.busy) return true; return false; } }
        /// <summary>
        /// Returns all views under this space
        /// </summary>
        public View[] views { get { return _views.ToArray(); } }
        #endregion

        #region MonoBehaviour
        public virtual void Awake()
        {
            ReloadHierarchy();
        }
        #endregion

        #region RefreshHierarchy
        public void ReloadHierarchy()
        {
            _views = GetComponentsInChildren<View>();
        }
        #endregion

        #region Push / Pull
        /// <summary>
        /// Activates a view or modal
        /// </summary>
        /// <param name="view"></param>
        public void PushView(View view) { PushView(view, 1); }
        /// <summary>
        /// Activates a view or modal
        /// </summary>
        /// <param name="view"></param>
        /// <param name="speed"></param>
        public void PushView(View view, float speed)
        {
            if (view == null) return;
            //  Don't reactivate a view we already have
            if (view == currentView || view == currentModal) return;
            view.Show(speed);
            if (view is Modal) currentModal = (Modal)view;
            else currentView = view;

        }
        /// <summary>
        /// Deactivates a view or modal
        /// </summary>
        /// <param name="view"></param>
        public void PullView(View view) { PullView(view, 1); }
        /// <summary>
        /// Deactivates a view or modal
        /// </summary>
        /// <param name="view"></param>
        /// <param name="speed"></param>
        public void PullView(View view, float speed)
        {
            if (view == null) return;
            view.Hide(speed);
            if (view == currentModal) currentModal = null;
            if (view == currentView) currentView = null;
        }
        #endregion

        #region Event
        /// <summary>
        /// Triggers an event on the active view AND modal
        /// </summary>
        /// <param name="eventName"></param>
        public void TriggerViewEvent(string eventName)
        {
            if (currentView != null) currentView.TriggerEvent(eventName);
            if (currentModal != null) currentModal.TriggerEvent(eventName);
        }
        #endregion

        #region Find
        /// <summary>
        /// Locates the closest ViewSpace ancestor
        /// </summary>
        /// <param name="transform"></param>
        /// <returns>ViewSpace, if found, otherwise null</returns>
        public static ViewSpace FindParent(Transform transform)
        {
            if (transform == null) return null;
            return (transform.GetComponent<ViewSpace>() ?? FindParent(transform.parent));
        }
        /// <summary>
        /// Locates the closest ViewSpace ancestor
        /// </summary>
        /// <param name="behaviour"></param>
        /// <returns>ViewSpace, if found, otherwise null</returns>
        public static ViewSpace FindParent(MonoBehaviour behaviour)
        {
            return (behaviour != null ? FindParent(behaviour.transform) : null);
        }
        #endregion

        #region Time
        /// <summary>
        /// Retrieves current Time.time (self) or Time.realtimeSinceStartup (world)
        /// </summary>
        /// <param name="timeSpace"></param>
        /// <returns></returns>
        public static float CurrentTime(TimeSpace timeSpace)
        {
            return (timeSpace == TimeSpace.Real ? Time.realtimeSinceStartup : Time.time);
        }
        #endregion
    }
}
