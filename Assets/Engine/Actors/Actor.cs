using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    [RequireComponent(typeof(PhotonView))]
    public class Actor : Photon.MonoBehaviour
    {
        #region Fields
        protected PhotonView view;
        [System.NonSerialized]
        public new Transform transform;
        #endregion

        #region MonoBehaviour
        public virtual void Awake()
        {
            view = GetComponent<PhotonView>();
            transform = GetComponent<Transform>();
        }
        #endregion

        #region Photon.MonoBehaviour
        /// <summary>
        /// Called by Photon when synchronizing this object
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="messageInfo"></param>
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo messageInfo)
        {
            if (stream.isWriting) OnBroadcast(stream, messageInfo);
            else OnReceive(stream, messageInfo);
        }
        #endregion

        #region Client Synchronization
        /// <summary>
        /// Called when ready to broadcast data to other clients (UDP)
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="messageInfo"></param>
        protected virtual void OnBroadcast(PhotonStream stream, PhotonMessageInfo messageInfo)
        {
        }
        /// <summary>
        /// Called when receiving data from controlling client (UDP)
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="messageInfo"></param>
        protected virtual void OnReceive(PhotonStream stream, PhotonMessageInfo messageInfo)
        {
        }
        #endregion
    }
}
