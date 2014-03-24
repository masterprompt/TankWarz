using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Tangatek.ViewManagement.NGUI
{
    [AddComponentMenu("ViewSpace/NGUI/Lerp/Position")]
    public class UILerpPosition : LerpBehaviour
    {

        #region Fields
        private UIRect mRect;
        private Transform cachedTransform;
        public Vector3 target;
        private Vector3 valueStart, valueEnd;
        #endregion

        #region MonoBehaviour
        public void Awake()
        {
            mRect = GetComponent<UIRect>();
            cachedTransform = transform;
        }
        public void Reset()
        {
            target = transform.localPosition;
        }
        #endregion

        #region LerpBehaviour
        public override void OnBegin()
        {
            valueStart = value;
            valueEnd = target;
        }
        public override void Lerp(float t)
        {
            value = Vector3.Lerp(valueStart, valueEnd, t);
        }
        #endregion

        #region Value
        public Vector3 value
        {
            get
            {
                return cachedTransform.localPosition;
            }
            set
            {
                if (mRect == null || !mRect.isAnchored)
                {
                    cachedTransform.localPosition = value;
                }
                else
                {
                    value -= cachedTransform.localPosition;
                    NGUIMath.MoveRect(mRect, value.x, value.y);
                }
            }
        }
        #endregion
    }
}
