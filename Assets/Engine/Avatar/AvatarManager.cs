using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine
{
    public class AvatarManager : MonoBehaviour
    {
        #region Fields
        public Avatar avatar;
        private new Transform transform;
        #endregion

        #region Properties
        public int avatarHash
        {
            get
            {
                if (avatar == null) return 0;
                return avatar.hashKey;
            }
            set
            {
                if (avatarHash == value) return;
                ReturnAvatar();
                RetrieveAvatar(value);
            }
        }
        #endregion

        #region Monobehaviour
        public void Awake()
        {
            this.transform = GetComponent<Transform>();
        }
        #endregion

        private void ReturnAvatar()
        {
            if (avatar == null) return;
            avatar.ReturnToCache();
            avatar = null;
        }

        private void RetrieveAvatar(int hashKey)
        {
            if (avatar != null) return;
            avatar = (Avatar)Avatar.RetrieveFromCache(hashKey);
            if (avatar == null) return;
            avatar.transform.parent = transform;
            avatar.transform.localPosition = Vector3.zero;
            avatar.transform.localRotation = Quaternion.identity;
            avatar.Waken();
        }

        public void Clear()
        {
            ReturnAvatar();
        }

    }
}
