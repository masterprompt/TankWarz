using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class Particle : Avatar
    {
        #region Fields
        public new ParticleSystem particleSystem;
        #endregion

        #region Monobehaviour
        public override void Awake()
        {
            base.Awake();
            if(particleSystem==null ) particleSystem = GetComponent<ParticleSystem>();
        }
        public virtual void Update()
        {
            if (particleSystem == null) return;
            if (particleSystem.isPlaying) return;
            ReturnToCache();
        }
        #endregion

        public override void Waken()
        {
            base.Waken();
            if (particleSystem != null) particleSystem.Play(true);
        }
        public override void Hibernate()
        {
            base.Hibernate();
            if (particleSystem != null) particleSystem.Stop();
        }
    }
}
