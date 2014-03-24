using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Tangatek.ViewManagement
{
    [AddComponentMenu("ViewSpace/View")]
    public class View : MonoBehaviour
    {
        #region Enumerations
        public enum Message
        {
            /// <summary>
            /// Message broadcasted to View children before it is activated
            /// </summary>
            OnViewShow,
            /// <summary>
            /// Message broadcasted to view children after it was activated
            /// </summary>
            OnViewShown,
            /// <summary>
            /// Message broadcasted to view children before it is deactivated
            /// </summary>
            OnViewHide,
            /// <summary>
            /// Message broadcasted to view children after it was deactivated
            /// </summary>
            OnViewHidden,
            /// <summary>
            /// Message broadcasted to view children when an even is pushed when the view is active
            /// </summary>
            OnViewEvent
        }

        public enum Phase
        {
            Show,
            Hide
        }
        #endregion

        #region Fields
        public List<Transition> transitions = new List<Transition>();
        private ViewSpace _viewSpace;
        private int _activeAgents;
        private LerpAgent[] _agents = new LerpAgent[0];
        #endregion

        #region Properties
        /// <summary>
        /// Returns true if current view is busy with activities
        /// </summary>
        public bool busy { get { return _activeAgents > 0; } }
        /// <summary>
        /// Returns the current ViewSpace that owns this view
        /// </summary>
        public ViewSpace viewSpace { get { return _viewSpace; } }
        #endregion

        #region MonoBehaviour
        public virtual void Awake()
        {
            //  Climb the hierarchy and find a viewspace to attach ourselves to
            _viewSpace = ViewSpace.FindParent(transform);
            ReloadHierarchy();
        }
        #endregion

        #region RefreshHierarchy
        public void ReloadHierarchy()
        {
            _agents = this.GetLerpAgentsInChildren();
        }
        #endregion

        #region Event
        /// <summary>
        /// Trigger event on view (only has effect if view is active)
        /// </summary>
        /// <param name="eventName"></param>
        public void TriggerEvent(string eventName)
        {
            if (viewSpace == null) return;
            if (viewSpace.currentView == this) PublishEvent(eventName);
            if (viewSpace.currentModal == this) PublishEvent(eventName);
        }
        internal void PublishEvent(string eventName)
        {
            Stop();
            foreach (var transition in transitions)
                if (transition != null)
                    if (transition.eventName == eventName)
                        StartCoroutine(transition.Begin(viewSpace));
        }
        #endregion

        #region Show / Hide
        internal void Show(float speed)
        {
            
            StartCoroutine(Execute(Phase.Show, speed));
        }
        internal void Hide(float speed)
        {
            StartCoroutine(Execute(Phase.Hide, speed));
        }
        #endregion

        #region Execute
        private IEnumerator Execute(Phase phase, float speed)
        {
            //  Call our pre-State message
            if(phase == Phase.Show) BroadcastMessage(Message.OnViewShow.ToString(), SendMessageOptions.DontRequireReceiver);
            else BroadcastMessage(Message.OnViewHide.ToString(), SendMessageOptions.DontRequireReceiver);

            //  Run all the agents
            foreach (var agent in _agents)
                if (agent.LerpType == phase)
                    StartCoroutine(Begin(agent, speed));

            //  Wait for all tweens to complete
            while (busy) yield return null;

            //  Call our post-state message
            if (phase == Phase.Show) BroadcastMessage(Message.OnViewShown.ToString(), SendMessageOptions.DontRequireReceiver);
            else BroadcastMessage(Message.OnViewHidden.ToString(), SendMessageOptions.DontRequireReceiver);
            
        }
        private void Stop()
        {
            StopAllCoroutines();
            _activeAgents = 0;
        }
        #endregion

        #region Agents
        private IEnumerator Begin(LerpAgent agent, float speed)
        {
            _activeAgents++;
            yield return StartCoroutine(agent.LerpBegin(speed));
            _activeAgents--;
        }
        #endregion

        #region Find
        /// <summary>
        /// Locates the closest View ancestor
        /// </summary>
        /// <param name="transform"></param>
        /// <returns>View, if found, otherwise null</returns>
        public static View FindParent(Transform transform)
        {
            if (transform == null) return null;
            return ( transform.GetComponent<View>() ?? FindParent(transform.parent));
        }
        /// <summary>
        /// Locates the closest View ancestor
        /// </summary>
        /// <param name="behaviour"></param>
        /// <returns>View, if found, otherwise null</returns>
        public static View FindParent(MonoBehaviour behaviour)
        {
            return (behaviour != null ? FindParent(behaviour.transform) : null);
        }
        #endregion
    }
}
