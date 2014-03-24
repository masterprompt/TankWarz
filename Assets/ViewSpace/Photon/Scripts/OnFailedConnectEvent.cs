using UnityEngine;
using System;

namespace Tangatek.ViewManagement
{
    [AddComponentMenu("ViewSpace/Photon/Events/On Connect Failed")]
    public class OnFailedConnectEvent : PhotonViewSpaceEvent
    {

        #region Photon Messages
        public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
        {
            TriggerEvent();
        }
        #endregion
    }
}
