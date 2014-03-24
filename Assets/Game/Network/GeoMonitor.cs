using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Tangatek;

namespace Game.Networking
{
    [AddComponentMenu("Game/Networking/GeoMonitor")]
    public class GeoMonitor : Photon.MonoBehaviour
    {

        #region Fields
        private new Transform transform;
        private PhotonView view;
        private GeoBuffer geoBuffer;
        #endregion

        #region MonoBehaviour
        public void Awake()
        {
            view = GetComponentInChildren<PhotonView>();
            geoBuffer = new GeoBuffer();
        }
        public void Reset()
        {
            transform = GetComponent<Transform>();
        }
        public void Update()
        {
            if (view == null) return;
            if (view.isMine) return;
            geoBuffer.Update(transform);
        }
        #endregion

        #region Photon.MonoBehaviour
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting) OnTransmit(stream, info);
            else OnReceive(stream, info);
        }
        protected virtual void OnTransmit(PhotonStream stream, PhotonMessageInfo info)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        protected virtual void OnReceive(PhotonStream stream, PhotonMessageInfo info)
        {
            var position = (Vector3)stream.ReceiveNext();
            var rotation = (Quaternion)stream.ReceiveNext();
            geoBuffer.Add(info.timestamp, position, rotation);
        }
        #endregion
    }
}
