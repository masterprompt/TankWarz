using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Tangatek.ViewManagement
{
    [AddComponentMenu("Scripts/PhotonViewSpaceEvent")]
    public class PhotonViewSpaceEvent : Photon.MonoBehaviour
    {

        #region Fields
        public EventTrigger eventTrigger = new EventTrigger();
        #endregion

        #region MonoBehaviour
        public void Awake()
        {
            eventTrigger.Localize(this);
        }
        #endregion

        #region Trigger
        public void TriggerEvent()
        {
            eventTrigger.Trigger();
        }
        #endregion

    }
}
