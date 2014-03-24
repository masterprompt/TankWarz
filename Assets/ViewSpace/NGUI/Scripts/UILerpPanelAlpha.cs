using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Tangatek.ViewManagement.NGUI
{
    [AddComponentMenu("ViewSpace/NGUI/Lerp/Panel Alpha")]
    public class UILerpPanelAlpha : LerpBehaviour
    {

        #region Fields
        public float target = 1;
        private UIPanel panel;
        private float valueStart, valueEnd;
        #endregion

        #region MonoBehaviour
        public void Awake()
        {
            panel = GetComponent<UIPanel>();
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
            value = Mathf.Lerp(valueStart, valueEnd, t);
        }
        #endregion

        #region Value
        public float value
        {
            get { return (panel == null ? 0 : panel.alpha); }
            set { if (panel == null) return; panel.alpha = value; }
        }
        #endregion
    }
}
