using System;
using System.Collections.Generic;
using UnityEngine;
using Tangatek.Caching;

namespace Engine
{
    public class Projectile : CachableObject
    {
        #region Fields
        public float speed = 10;
        public Player player;
        public LayerMask scoreLayers;
        public LayerMask hitLayers;
        public Particle collisionParticles;
        private float distance;
        public float maxDistance = 100;
        #endregion

        #region Properties
        #endregion

        #region Monobehaviour
        public override void Awake()
        {
            base.Awake();
        }
        public virtual void Update()
        {
            var frameDistance = speed * Time.deltaTime;
            distance += frameDistance;
            var vector = transform.TransformDirection(Vector3.forward);
            var position = transform.position;
            RaycastHit hit;
            if (Physics.Raycast(position, vector, out hit, frameDistance, hitLayers))
            {
                if ((scoreLayers.value & 1 <<hit.collider.gameObject.layer)!=0)
                {
                    if (player != null) player.score++;
                }
                if (collisionParticles != null)
                {
                    var particle = collisionParticles.RetrieveFromCache();
                    particle.transform.position = hit.point;
                    particle.Waken();
                }

                ReturnToCache();
                return;
            }
            transform.position += (vector * frameDistance);
            if (distance > 100) PhotonNetwork.Destroy(gameObject);
        }
        #endregion

    }
}
