using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cameras
{
    public class ActionTarget : MonoBehaviour
    {
        #region Fields
        public static List<ActionTarget> list = new List<ActionTarget>();
        [System.NonSerialized]
        public new Transform transform;
        #endregion

        #region Monobehaviour
        public virtual void Awake()
        {
            transform = GetComponent<Transform>();
        }
        public virtual void OnEnable()
        {
            list.Add(this);
        }
        public virtual void OnDisable()
        {
            list.Remove(this);
        }
        #endregion

        #region Bounds
        public static Bounds GetBounds()
        {
            var min = new Vector3(Mathf.Infinity, 0, Mathf.Infinity);
            var max = new Vector3(Mathf.NegativeInfinity, 1, Mathf.NegativeInfinity);
            var bounds = new Bounds();
            foreach (var target in list)
            {
                if (target.transform.position.x < min.x) min.x = target.transform.position.x;
                if (target.transform.position.x > max.x) max.x = target.transform.position.x;
                if (target.transform.position.z < min.z) min.z = target.transform.position.z;
                if (target.transform.position.z > max.z) max.z = target.transform.position.z;
            }

            return new Bounds(min + ((max - min) * 0.5f), (max == min ? Vector3.one : max - min));
        }
        #endregion
    }
}
