using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Tangatek.ViewManagement
{
    public class ViewSpaceEvent : MonoBehaviour
    {

        #region Fields
        public EventTrigger eventTrigger = new EventTrigger();
        #endregion

        #region MonoBehaviour
        public virtual void Awake()
        {
            eventTrigger.Localize(this);
        }
        #endregion

        #region TriggerEvent
        public void TriggerEvent()
        {
            eventTrigger.Trigger();
        }
        #endregion
    }
}
