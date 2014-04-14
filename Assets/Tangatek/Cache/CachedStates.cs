using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tangatek.Caching
{
    internal abstract class CachedStates : StateCollection
    {
        #region Fields
        protected Dictionary<object, bool> catelog = new Dictionary<object, bool>();
        #endregion

        #region Properties
        public virtual bool sleepState { get { return false; } }
        #endregion

        protected abstract void SetState(object o, bool state);
        protected abstract bool GetState(object o);

        #region Add
        protected void Add(object o)
        {
            if (o == null) return;
            if (catelog.ContainsKey(o)) return;
            catelog.Add(o, GetState(o));
        }
        #endregion

        public void Sleep()
        {
            foreach (var record in catelog)
                if(record.Key != null)
                    SetState(record.Key, sleepState);
        }
        public void Waken()
        {
            foreach (var record in catelog)
                if (record.Key != null)
                    SetState(record.Key, record.Value);
        }
        
    }

    internal class Renderers : CachedStates
    {
        public Renderers(MonoBehaviour mb)
        {
            var objects = mb.GetComponentsInChildren<Renderer>();
            foreach (var o in objects) Add(o);
        }
        protected override bool GetState(object o)
        {
            return ((Renderer)o).enabled;
        }
        protected override void SetState(object o, bool state)
        {
            ((Renderer)o).enabled = state;
        }
    }


    internal class Colliders : CachedStates
    {
        public Colliders(MonoBehaviour mb)
        {
            var objects = mb.GetComponentsInChildren<Collider>();
            foreach (var o in objects) Add(o);
        }
        protected override bool GetState(object o)
        {
            return ((Collider)o).enabled;
        }
        protected override void SetState(object o, bool state)
        {
            ((Collider)o).enabled = state;
        }
    }

    internal class Monobehaviours : CachedStates
    {
        public Monobehaviours(MonoBehaviour mb)
        {
            var objects = mb.GetComponentsInChildren<MonoBehaviour>();
            foreach (var o in objects) Add(o);
        }
        protected override bool GetState(object o)
        {
            return ((MonoBehaviour)o).enabled;
        }
        protected override void SetState(object o, bool state)
        {
            ((MonoBehaviour)o).enabled = state;
        }
    }

    internal class Animators : CachedStates
    {
        public Animators(MonoBehaviour mb)
        {
            var objects = mb.GetComponentsInChildren<Animator>();
            foreach (var o in objects) Add(o);
        }
        protected override bool GetState(object o)
        {
            return ((Animator)o).enabled;
        }
        protected override void SetState(object o, bool state)
        {
            ((Animator)o).enabled = state;
        }
    }

    internal class Rigidbodies : CachedStates
    {
        public Rigidbodies(MonoBehaviour mb)
        {
            var objects = mb.GetComponentsInChildren<Rigidbody>();
            foreach (var o in objects) Add(o);
        }
        public override bool sleepState { get { return true; } }
        protected override bool GetState(object o)
        {
            return ((Rigidbody)o).isKinematic;
        }
        protected override void SetState(object o, bool state)
        {
            ((Rigidbody)o).isKinematic = state;
        }
    }
}
