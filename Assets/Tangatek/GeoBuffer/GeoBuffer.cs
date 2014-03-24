using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tangatek
{
	public class GeoBuffer
    {
        #region Fields
        private Snapshot[] buffer = new Snapshot[10];
        private double forcedLatency = 0.2d;
        #endregion

        #region Update
        public void Update(Transform transform)
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
