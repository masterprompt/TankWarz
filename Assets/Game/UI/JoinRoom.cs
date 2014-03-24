using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Tangatek.ViewManagement;

namespace Game
{
    [AddComponentMenu("Game/UI/JoinRoom")]
    public class JoinRoom : Photon.MonoBehaviour
    {

        public void OnViewShown()
        {
            //PhotonNetwork.isMessageQueueRunning = false;
            PhotonNetwork.JoinRandomRoom();
        }
        public virtual void OnPhotonRandomJoinFailed()
        {
            Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, true, true, 4);");
            PhotonNetwork.CreateRoom(null, true, true, 4);
        }
    }
}
