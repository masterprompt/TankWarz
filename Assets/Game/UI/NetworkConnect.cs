using UnityEngine;
using System;

namespace Game
{
    [AddComponentMenu("Game/UI/NetworkConnect")]
    public class NetworkConnect : Photon.MonoBehaviour
    {
        public void OnViewShown()
        {
            PhotonNetwork.ConnectUsingSettings("1"); 
        }
    }
}
