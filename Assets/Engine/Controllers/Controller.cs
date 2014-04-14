using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    /// <summary>
    /// Base class for a controller.  Controllers control pawns
    /// </summary>
    public abstract class Controller
    {
        #region Controller
        public abstract void Update(Pawn pawn);
        #endregion
    }
}
