using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Buffers
{
    class LocationBuffer
    {
        #region Fields
        private Snapshot[] buffer = new Snapshot[10];
        private double forcedLatency = 0.2d;
        public Transform transform;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public LocationBuffer(Transform transform)
        {
            this.transform = transform;
        }
        #endregion

        #region Update
        /// <summary>
        /// Updates this transform with current buffered position/rotation
        /// </summary>
        public void Update()
        {
            if (transform == null) return;
            var sampleTime = PhotonNetwork.time - forcedLatency;
            var lower = buffer[0];
            var upper = buffer[0];
            double lowerTime = 0;
            double upperTime = PhotonNetwork.time;

            for (var i = 0; i < buffer.Length; i++)
            {
                if (buffer[i].time < sampleTime)
                    if (buffer[i].time > lowerTime)
                    {
                        lower = buffer[i];
                        lowerTime = lower.time;
                    }
                if (buffer[i].time >= sampleTime)
                    if (buffer[i].time < upperTime)
                    {
                        upper = buffer[i];
                        upperTime = upper.time;
                    }
            }


            //  Nothing was found that would fit
            if (lowerTime == 0 || upperTime == PhotonNetwork.time) return;

            var duration = (upper.time - lower.time);
            var current = sampleTime - lower.time;
            var delta = current / duration;
            var d = (float)delta;

            transform.position = Vector3.Lerp(lower.position, upper.position, d);
            transform.rotation = Quaternion.Lerp(lower.rotation, upper.rotation, d);
        }
        #endregion

        #region Synchronization
        /// <summary>
        /// Called by Photon when synchronizing this object
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="messageInfo"></param>
        public void OnBroadcast(PhotonStream stream, PhotonMessageInfo messageInfo)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        /// <summary>
        /// Called when receiving data from controlling client (UDP)
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="messageInfo"></param>
        public void OnReceive(PhotonStream stream, PhotonMessageInfo messageInfo)
        {
            var position = (Vector3)stream.ReceiveNext();
            var rotation = (Quaternion)stream.ReceiveNext();
            Add(messageInfo.timestamp, position, rotation);
            
        }
        #endregion

        #region Add
        public void Add(double time, Vector3 position, Quaternion rotation)
        {
            //  get oldest time
            var index = oldestIndex;
            //  If our time is older than our oldest, dont add it
            if (time < buffer[index].time) return;
            buffer[index].time = time;
            buffer[index].position = position;
            buffer[index].rotation = rotation;
        }
        private int oldestIndex
        {
            get
            {
                var index = 0;
                var time = PhotonNetwork.time;
                for (var i = 0; i < buffer.Length; i++)
                    if (buffer[i].time < time)
                    {
                        index = i;
                        time = buffer[i].time;
                    }
                return index;
            }
        }
        #endregion

        #region Snapshot
        internal struct Snapshot
        {
            #region Fields
            internal double time;
            internal Vector3 position;
            internal Quaternion rotation;
            #endregion
        }
        #endregion
    }
}
