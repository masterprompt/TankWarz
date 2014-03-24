using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Tangatek;

namespace Game.PlayerSpace
{
    [AddComponentMenu("Game/Player")]
    public class Player : Photon.MonoBehaviour
    {

        #region Fields
        public static List<Player> List = new List<Player>();
        public static Player Me;
        [System.NonSerialized]
        public CharacterController characterController;
        [HideInInspector]
        public new Transform transform;
        private Control.Controller controller;
        public float speed = 1;
        public float turn = 90;
        public float accelerationDamping = 0.3f;
        private float accelerationVelocity;
        private float acceleration;
        private PhotonView view;
        public int score;
        public Bullet bullet;
        private float lastBulletTime;
        public float bulletDelay = 2;
        public Transform bulletSpawn;
        #endregion

        #region MonoBehaviour
        public void Awake()
        {
            characterController = GetComponent<CharacterController>();
            controller = new Control.Keyboard();
            view = GetComponentInChildren<PhotonView>();
            if (view != null && view.isMine) Me = this;
            List.Add(this);
        }
        public void Reset()
        {
            transform = GetComponent<Transform>();
        }
        public void Update()
        {
            if (!view.isMine) return;
            acceleration = Mathf.SmoothDamp(acceleration, controller.move.z, ref accelerationVelocity, accelerationDamping);

            characterController.SimpleMove(transform.TransformDirection(Vector3.forward) * acceleration * speed);
            transform.Rotate(Vector3.up, controller.turn.x * turn * Time.deltaTime);
            if (controller.attack && Time.time - lastBulletTime >= bulletDelay && bulletSpawn!=null)
            {
                Bullet.Spawn(bullet.name, bulletSpawn.position, bulletSpawn.rotation);
                lastBulletTime = Time.time;
            }

        }
        public void OnDestory()
        {
            List.Remove(this);
        }
        #endregion

        #region Photon.MonoBehaviour
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            
        }
        #endregion

        #region Spawn
        public static void Spawn(Vector3 position, Quaternion rotation)
        {
            PhotonNetwork.Instantiate("Player", position, rotation, 0);
        }
        /// <summary>
        /// Spawn player at random spawn point
        /// </summary>
        public static void Spawn()
        {
            var spawn = PlayerSpace.Spawn.GetRandom();
            if (spawn != null) Spawn(spawn.transform.position, spawn.transform.rotation);
            else Spawn(Vector3.zero, Quaternion.identity);
        }
        #endregion

    }
}
