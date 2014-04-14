using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public abstract class PlayerInput
    {
        #region Input
        public abstract bool attackPrimary { get; }
        public abstract bool attackSecondary { get; }
        public abstract Vector3 move { get; }
        public abstract Vector3 look { get; }
        public abstract bool weaponNext { get; }
        public abstract bool weaponPrev { get; }
        public abstract bool enabled { get; set; }
        #endregion
    }
}
