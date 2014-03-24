using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Game.PlayerSpace
{
    [AddComponentMenu("Game/Bullet")]
    public class Bullet : Photon.MonoBehaviour
    {

        #region Fields
        private PhotonView view;
        public float speed;
        public float maxDistance = 10;
        [HideInInspector]
        public new Transform transform;
        #endregion

        #region MonoBehaviour
        public void Awake()
        {
            view = GetComponentInChildren<PhotonView>();
            if (view != null && view.isMine) StartCoroutine(Run(speed, maxDistance));
        }
        public void Reset()
        {
            transform = GetComponent<Transform>();
        }
        #endregion

        #region Run
        public IEnumerator Run(float speed, float maxDistance)
        {
            var distance = 0f;
            var done = false;
            while (!done)
            {
                var frameDistance = speed * Time.deltaTime;
                transform.position += transform.TransformDirection(Vector3.forward) * frameDistance;
                distance += frameDistance;
                if (distance > maxDistance) done = true;
                yield return null;
            }
            PhotonNetwork.Destroy(gameObject);
        }
        #endregion

        #region Spawn
        public static void Spawn(string name, Vector3 position, Quaternion rotation)
        {
            PhotonNetwork.Instantiate(name, position, rotation, 0);
        }
        public static void Spawn(Vector3 position, Quaternion rotation)
        {
            PhotonNetwork.Instantiate("Bullet", position, rotation, 0);
        }
        #endregion
    }
}
