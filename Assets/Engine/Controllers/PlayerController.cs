using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.PlayerControllers
{
    public class PlayerController : Controller
    {
        #region Fields
        public PlayerInput playerInput;
        public float speed = 1;
        #endregion

        #region Controller
        public override void Update(Pawn pawn)
        {
            if (playerInput == null) return;
            if (!pawn.characterController.enabled) return;
            if (!playerInput.enabled) return;


            if (Input.GetKeyDown(KeyCode.Escape)) playerInput.enabled = false;
            if (Input.GetKeyDown(KeyCode.Space)) playerInput.enabled = true;

            //  Move
            pawn.characterController.SimpleMove(pawn.transform.TransformDirection(playerInput.move) * speed);

            //  Look
            pawn.transform.Rotate(Vector3.up, playerInput.look.x);
        }
        #endregion
    }
}
