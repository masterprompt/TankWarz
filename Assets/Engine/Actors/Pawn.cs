using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    [RequireComponent(typeof(AvatarManager))]
    [RequireComponent(typeof(CharacterController))]
    public class Pawn : Actor
    {
        #region Fields
        public float health = 1;
        public float damage = 1;
        private Buffers.LocationBuffer locationBuffer;
        [System.NonSerialized]
        public CharacterController characterController;
        [System.NonSerialized]
        public AvatarManager avatarManager;
        [System.NonSerialized]
        public Viewport viewport;
        public Transform barrel;
        public float attackDelay = 0.5f;
        //public Tank tank;
        public Projectile projectile;
        #endregion

        #region Properties
        /// <summary>
        /// Global velocity of Actor
        /// </summary>
        protected Vector3 velocity { get { return characterController.velocity; } }
        /// <summary>
        /// local velocity relative to forward
        /// </summary>
        protected Vector3 localVelocity { get { return transform.InverseTransformDirection(velocity); } }
        #endregion

        #region Monobehaviour
        public override void Awake()
        {
            base.Awake();
            locationBuffer = new Buffers.LocationBuffer(this.transform);
            characterController = GetComponent<CharacterController>();
            avatarManager = GetComponent<AvatarManager>();
            viewport = this.GetViewportInChildren();
            //avatarManager.avatarHash = tank.hashKey;
        }
        public virtual void Update()
        {
            if (!view.isMine) locationBuffer.Update();
        }
        public virtual void OnDisable()
        {
            avatarManager.Clear();
        }
        #endregion

        #region Create
        public static Pawn Create(Space space, Vector3 position, Quaternion rotation)
        {
            var data = new System.Object[]{};
            if (space == Space.Self) return PhotonNetwork.Instantiate("Pawn", position, rotation, 0, data).GetComponent<Pawn>();
            else return PhotonNetwork.InstantiateSceneObject("Pawn", position, rotation, 0, data).GetComponent<Pawn>(); 
        }
        #endregion

        #region Client Synchronization
        /// <summary>
        /// Called by Photon when synchronizing this object
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="messageInfo"></param>
        protected override void OnBroadcast(PhotonStream stream, PhotonMessageInfo messageInfo)
        {
            base.OnBroadcast(stream, messageInfo);
            locationBuffer.OnBroadcast(stream, messageInfo);
            //stream.SendNext(health);
            //stream.SendNext(damage);
            //stream.SendNext(avatarManager.avatarHash);
        }
        /// <summary>
        /// Called when receiving data from controlling client (UDP)
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="messageInfo"></param>
        protected override void OnReceive(PhotonStream stream, PhotonMessageInfo messageInfo)
        {
            base.OnReceive(stream, messageInfo);
            locationBuffer.OnReceive(stream, messageInfo);
            //health = (float)stream.ReceiveNext();
            //damage = (float)stream.ReceiveNext();
            //avatarManager.avatarHash = (int)stream.ReceiveNext();
        }
        #endregion


        #region Helpers
        public void Move(Vector3 vector, Space space)
        {
            if (!characterController.enabled) return;
            if (space == Space.Self) vector = transform.TransformDirection(vector);
            characterController.SimpleMove(vector);
        }
        public void Move(Vector3 vector)
        {
            Move(vector, Space.Self);
        }
        /// <summary>
        /// Local space
        /// </summary>
        /// <param name="vector"></param>
        public void Look(Vector3 vector)
        {
            transform.rotation = Quaternion.LookRotation(vector, Vector3.up);
        }
        public void Rotate(Vector3 axis, float angle)
        {
            transform.Rotate(axis, angle);
        }
        /// <summary>
        /// Along Y axis
        /// </summary>
        /// <param name="angle"></param>
        public void Rotate(float angle)
        {
            Rotate(Vector3.up, angle);
        }

        private float attackTime;
        public void Attack(Player player)
        {
            if (Time.time - attackTime < attackDelay) return;
            attackTime = Time.time;
            view.RPC("Attack", PhotonTargets.Others, barrel.position, barrel.rotation);
            Fire(barrel.position, barrel.rotation, player);
        }
        [RPC]
        public void Attack(Vector3 position, Quaternion rotation)
        {
            Fire(position, rotation, null);
        }
        public void Fire(Vector3 position, Quaternion rotation, Player player)
        {
            if (this.projectile == null) return;
            var projectile = (Projectile)this.projectile.RetrieveFromCache();
            projectile.player = player;
            projectile.transform.position = position;
            projectile.transform.rotation = rotation;
            projectile.Waken();
        }
        #endregion

        public void SetAvatar(Avatar avatar)
        {
            view.RPC("SetAvatar", PhotonTargets.AllBuffered, avatar.hashKey);
            Debug.Log("Setting avatar " + avatar.hashKey, avatar);
        }
        [RPC]
        public void SetAvatar(int key)
        {
            Debug.Log("Receving avatar " + key, this);
            avatarManager.avatarHash = key;
        }

    }
}
