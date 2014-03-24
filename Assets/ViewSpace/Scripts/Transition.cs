using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tangatek.ViewManagement
{
    [System.Serializable]
	public sealed class Transition
    {
        #region Enumerations
        public enum Mode
        {
            Series,
            Parallel
        }
        #endregion

        #region Fields
        public Mode mode = Mode.Parallel;
        public View target;
        public float speed;
        public string eventName = "";
        #endregion

        #region Constructors
        public Transition()
        {
            speed = 1;
        }
        #endregion

        #region Begin
        public IEnumerator Begin(ViewSpace viewSpace)
        {
            if (viewSpace == null) yield break;
            if (target == null) yield break;
            //  Calling our current modal?  Dont do anything!
            if (viewSpace.currentModal == target) yield break;
            //  Pull current modal because we are either pusing a view or another modal.
            viewSpace.PullView(viewSpace.currentModal);
            //  Only pull the current view if we are pushing a new view AND our current view isnt the new view
            if (!(target is Modal) && viewSpace.currentView != target) viewSpace.PullView(viewSpace.currentView);
            if (mode == Mode.Series)
                while (viewSpace.busy) 
                    yield return null;
            viewSpace.PushView(target, speed);
        }
        #endregion
	}
}
