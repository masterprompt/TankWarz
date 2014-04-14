using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tangatek.Caching
{
    public class StateController
    {
        #region Fields
        private delegate void Delegate();
        private event Delegate sleepEvent, wakenEvent;
        private List<StateCollection> list = new List<StateCollection>();
        #endregion

        #region Add
        public void Add(StateCollection stateCollection)
        {
            list.Add(stateCollection);
            sleepEvent += new Delegate(stateCollection.Sleep);
            wakenEvent += new Delegate(stateCollection.Waken);
        }
        #endregion

        #region Sleep / Waken
        internal void Sleep()
        {
            if (sleepEvent != null) sleepEvent();
        }

        internal void Waken()
        {
            if (wakenEvent != null) wakenEvent();
        }
        #endregion
    }


}
