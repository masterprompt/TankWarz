using UnityEngine;
using System;

namespace Tangatek.ViewManagement
{
    [AddComponentMenu("ViewSpace/Photon/Events/On Disconnect")]
    public class OnDisconnectEvent : PhotonViewSpaceEvent
    {

        #region Photon Messages
        public virtual void OnDisconnectedFromPhoton()
        {
            TriggerEvent();
        }
        #endregion
    }
}
