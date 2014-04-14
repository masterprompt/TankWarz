
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class Spawn : MonoBehaviour
    {
        #region Fields
        public static List<Spawn> list = new List<Spawn>();
        public int index;
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

        public static Spawn GetRandom()
        {
            return list[Random.Range(0,list.Count-1)];
        }
    }
}
