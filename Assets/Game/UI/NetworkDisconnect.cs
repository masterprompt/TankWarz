using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Tangatek.ViewManagement;

namespace Game
{
    [AddComponentMenu("Game/UI/Network Disconnect")]
    public class NetworkDisconnect : Photon.MonoBehaviour
    {
        public void OnViewShown()
        {

            PhotonNetwork.Disconnect();
        }
    }
}
