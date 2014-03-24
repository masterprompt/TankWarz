using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Tangatek.ViewManagement.NGUI
{
    [AddComponentMenu("ViewSpace/NGUI/Events/On Click")]
    public class OnClickEvent : ViewSpaceEvent
    {
        #region NGUI
        public void OnClick()
        {
            TriggerEvent();
        }
        #endregion
    }
}
