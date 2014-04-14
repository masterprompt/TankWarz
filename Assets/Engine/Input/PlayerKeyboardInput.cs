using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class PlayerKeyboardInput : PlayerInput
    {
        private bool _enabled = false;
        public override bool attackPrimary 
        { 
            get 
            {
                return (enabled ? Input.GetKey(KeyCode.Space) : false); 
            } 
        }
        public override bool attackSecondary { get { return (enabled ? Input.GetMouseButton(2) : false); } }
        public override Vector3 move
        {
            get
            {
                
                var vector = new Vector3(0, 0, 0);
                if (!enabled) return vector;
                if (Input.GetKey(KeyCode.S)) vector.z = -1;
                if (Input.GetKey(KeyCode.W)) vector.z = 1;
                //if (Input.GetKey(KeyCode.A)) vector.x = -1;
                //if (Input.GetKey(KeyCode.D)) vector.x = 1;
                return vector;
            }
        }
        public override Vector3 look
        {
            get
            {
                var vector = new Vector3(0, 0, 0);
                if (!enabled) return vector;
                //if (Input.GetKey(KeyCode.S)) vector.z = -1;
                //if (Input.GetKey(KeyCode.W)) vector.z = 1;
                if (Input.GetKey(KeyCode.A)) vector.x = -1;
                if (Input.GetKey(KeyCode.D)) vector.x = 1;
                return vector;
            }
        }
        public override bool weaponNext { get { return (enabled ? Input.GetKeyDown(KeyCode.Q) : false); } }
        public override bool weaponPrev { get { return (enabled ? Input.GetKeyDown(KeyCode.E) : false); } }
        public override bool enabled { get { return _enabled; } set { _enabled = value; Screen.lockCursor = _enabled; } }
    }
}
