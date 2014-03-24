using UnityEngine;
using System;

namespace Tangatek.ViewManagement
{
    [AddComponentMenu("ViewSpace/Photon/Events/On Joined Room")]
    public class OnJoinedRoomEvent : PhotonViewSpaceEvent
    {

        #region Photon Messages
        public virtual void OnJoinedRoom()
        {
            Debug.Log("Joined room, Sending event");
            TriggerEvent();
        }
        #endregion
    }
}
