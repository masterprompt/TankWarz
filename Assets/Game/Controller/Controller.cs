using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
	public abstract class Controller
    {
        #region Properties
        public abstract Vector3 move { get; }
        public abstract Vector2 turn { get; }
        public abstract bool attack { get; }
        #endregion
    }
}
