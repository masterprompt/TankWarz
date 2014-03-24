using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Tangatek.ViewManagement
{
    [AddComponentMenu("ViewSpace/Lerp Behaviours/Lerp Behaviour")]
    public abstract class LerpBehaviour : MonoBehaviour, LerpAgent
    {

        #region Fields
        public View.Phase phase = View.Phase.Show;
        public TimeSpace timeSpace = TimeSpace.Real;
        public float delay = 0;
        public float duration = 0.5f;
        #endregion

        #region Properties
        protected float time { get { return ViewSpace.CurrentTime(timeSpace); } }
        #endregion

        #region LerpBehaviour
        public virtual void OnBegin() { }
        public abstract void Lerp(float t);
        public virtual void OnEnd() { }
        #endregion

        #region LerpAgent
        public View.Phase LerpType { get { return phase; } }
        public IEnumerator LerpBegin(float speed)
        {
            OnBegin();
            if (delay > 0) yield return new WaitForSeconds(delay / speed);
            if (duration > 0) yield return StartCoroutine(LerpUpdate(speed));
            Lerp(1);
            OnEnd();
        }
        private IEnumerator LerpUpdate(float speed)
        {
            var start = time;
            var t = 0f;
            while (t < 1)
            {
                yield return null;
                //  Calculate the delta
                t = Mathf.Clamp01(((time - start) / duration) * speed);
                //  Apply easing

                //  Run through lerping
                Lerp(t);
            }
        }
        #endregion
    }
}
