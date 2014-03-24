using UnityEngine;
using System;
using Tangatek.ViewManagement;

namespace Game
{
    [AddComponentMenu("Game/UI/Events/On Joined Room")]
    public class OnJoinedRoomEvent : Tangatek.ViewManagement.OnJoinedRoomEvent
    {

        #region Photon Messages
        public override void OnJoinedRoom()
        {
            PhotonNetwork.isMessageQueueRunning = false;
            Debug.Log("Joined room, pausing message queue");
            base.OnJoinedRoom();

        }
        #endregion
    }
}
