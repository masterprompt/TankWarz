using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
	public class Keyboard : Controller
	{
        public override Vector3 move
        {
            get
            {
                var vector = Vector3.zero;
                if (Input.GetKey(KeyCode.W)) vector.z = 1;
                if (Input.GetKey(KeyCode.S)) vector.z = -1;
                return vector;
            }
        }

        public override Vector2 turn
        {
            get 
            {
                var vector = Vector2.zero;
                if (Input.GetKey(KeyCode.A)) vector.x = -1;
                if (Input.GetKey(KeyCode.D)) vector.x = 1;
                return vector;
            }
        }

        public override bool attack
        {
            get
            {
                return Input.GetKey(KeyCode.Space);
            }
        }
	}
}
