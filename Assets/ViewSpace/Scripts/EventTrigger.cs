using System;
using UnityEngine;

namespace Tangatek.ViewManagement
{
    [System.Serializable]
	public class EventTrigger
    {
        #region Fields
        public string eventName;
        private View view;
        #endregion

        #region Localize
        /// <summary>
        /// Locates the nearest View for trigger to execute on
        /// </summary>
        /// <param name="monoBehaviour"></param>
        public void Localize(MonoBehaviour monoBehaviour)
        {
            view = View.FindParent(monoBehaviour);
        }
        #endregion

        #region Trigger
        /// <summary>
        /// Triggers this event
        /// </summary>
        public void Trigger()
        {
            if (view == null)
            {
                Debug.LogError("Possibly orphaned EventTrigger " + eventName + " (did you forget to localize it?)");
                return;
            }
            view.TriggerEvent(eventName);
        }
        #endregion

    }
}
