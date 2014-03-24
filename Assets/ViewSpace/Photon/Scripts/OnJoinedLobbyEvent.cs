using UnityEngine;
using System;

namespace Tangatek.ViewManagement
{
    [AddComponentMenu("ViewSpace/Photon/Events/On Joined Lobby")]
    public class OnJoinedLobbyEvent : PhotonViewSpaceEvent
    {

        #region Photon Messages
        public virtual void OnJoinedLobby()
        {
            TriggerEvent();
        }
        #endregion
    }
}
