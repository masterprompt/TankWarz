using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Game.PlayerSpace;

namespace Game
{
    [AddComponentMenu("Scripts/Network")]
    public class Network : MonoBehaviour
    {

        #region Properties

        #endregion

        #region MonoBehaviour
        void Awake()
        {

        }
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings("1");
        }

        #endregion

        public void OnJoinedLobby()
        {
            Debug.Log("Joined server");
            PhotonNetwork.JoinRandomRoom();
        }
        public void OnPhotonRandomJoinFailed()
        {
            Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, true, true, 4);");
            PhotonNetwork.CreateRoom(null, true, true, 4);
        }
        public void OnJoinedRoom()
        {
            Debug.Log("Joined room");
            Player.Spawn();

        }
    }
}
