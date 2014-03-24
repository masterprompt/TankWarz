using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Game.PlayerSpace
{
    [AddComponentMenu("Scripts/Spawn")]
    public class Spawn : MonoBehaviour
    {

        #region Fields
        public static List<Spawn> List = new List<Spawn>();
        [HideInInspector]
        public new Transform transform;
        #endregion

        #region MonoBehaviour
        public void Awake()
        {
            List.Add(this);
        }
        public void Reset()
        {
            transform = GetComponent<Transform>();
        }
        public void OnDestroy()
        {
            List.Remove(this);
        }
        #endregion

        #region Random
        public static Spawn GetRandom()
        {
            Spawn spawn = null;
            if (List.Count > 0) spawn = List[UnityEngine.Random.Range(0, List.Count)];
            return spawn;
        }
        #endregion
    }
}
