﻿using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Easing
{

    #region Public
    public float Ease(float start, float end, float value)
    {
        return ease(start, end, value);
    }
    public float Ease(float delta)
    {
        return ease(0f, 1f, delta);
    }
    public float Ease(float delta, bool clamped)
    {
        return ease(0f, 1f, (clamped ? Mathf.Clamp(delta, 0f, 1f) : delta));
    }
    public float SinEase(float delta)
    {
        return ease(0f, 1f, this.sin(0,1,delta));
    }
    public float CircEase(float delta)
    {
        delta *= 2f;
        if (delta > 1f)
            delta = 2f - delta;

        return ease(0f, 1f, delta);
    }
    #endregion

    #region Variables
    public enum EaseType
    {
        easeInQuad,
        easeOutQuad,
        easeInOutQuad,
        easeInCubic,
        easeOutCubic,
        easeInOutCubic,
        easeInQuart,
        easeOutQuart,
        easeInOutQuart,
        easeInQuint,
        easeOutQuint,
        easeInOutQuint,
        easeInSine,
        easeOutSine,
        easeInOutSine,
        easeInExpo,
        easeOutExpo,
        easeInOutExpo,
        easeInCirc,
        easeOutCirc,
        easeInOutCirc,
        linear,
        spring,
        bounce,
        easeInBack,
        easeOutBack,
        easeInOutBack,
        elastic,
        sineWave
    }
    private delegate float EasingFunction(float start, float end, float value);
    private EasingFunction ease = null;
    private EaseType _easeType = EaseType.linear;
    private AnimationCurve _easingCurve = null;
    //public AnimationCurve editorCurve = null;
    #endregion

    #region Constructors
    public Easing(EaseType easeType)
    {
        _AssignEaseType(easeType);
    }
    public Easing()
    {
        _AssignEaseType(EaseType.linear);
    }
    public AnimationCurve curve
    {
        get
        {
            if (_easingCurve == null) _easingCurve = CreateEasingCurve();
            return _easingCurve;
        }
    }
    public EaseType easeType
    {
        get
        {
            return _easeType;
        }
        set
        {
            _AssignEaseType(value);
        }
    }
    private AnimationCurve CreateEasingCurve()
    {
        Keyframe[] ks = new Keyframe[25];
        int i = 0;
        while (i < ks.Length)
        {
            float delta = Convert.ToSingle(i) / Convert.ToSingle(ks.Length - 1);
            ks[i] = new Keyframe(delta, Ease(delta));
            i++;
        }
        return new AnimationCurve(ks);
    }
    public void Init()
    {
        _AssignEaseType(_easeType);
    }
    #endregion

    #region Helpers
    private void _AssignEaseType(EaseType easeType)
    {
        _easingCurve = null;
        _easeType = easeType;
        switch (easeType)
        {
            case EaseType.easeInQuad:
                ease = new EasingFunction(easeInQuad);
                break;
            case EaseType.easeOutQuad:
                ease = new EasingFunction(easeOutQuad);
                break;
            case EaseType.easeInOutQuad:
                ease = new EasingFunction(easeInOutQuad);
                break;
            case EaseType.easeInCubic:
                ease = new EasingFunction(easeInCubic);
                break;
            case EaseType.easeOutCubic:
                ease = new EasingFunction(easeOutCubic);
                break;
            case EaseType.easeInOutCubic:
                ease = new EasingFunction(easeInOutCubic);
                break;
            case EaseType.easeInQuart:
                ease = new EasingFunction(easeInQuart);
                break;
            case EaseType.easeOutQuart:
                ease = new EasingFunction(easeOutQuart);
                break;
            case EaseType.easeInOutQuart:
                ease = new EasingFunction(easeInOutQuart);
                break;
            case EaseType.easeInQuint:
                ease = new EasingFunction(easeInQuint);
                break;
            case EaseType.easeOutQuint:
                ease = new EasingFunction(easeOutQuint);
                break;
            case EaseType.easeInOutQuint:
                ease = new EasingFunction(easeInOutQuint);
                break;
            case EaseType.easeInSine:
                ease = new EasingFunction(easeInSine);
                break;
            case EaseType.easeOutSine:
                ease = new EasingFunction(easeOutSine);
                break;
            case EaseType.easeInOutSine:
                ease = new EasingFunction(easeInOutSine);
                break;
            case EaseType.easeInExpo:
                ease = new EasingFunction(easeInExpo);
                break;
            case EaseType.easeOutExpo:
                ease = new EasingFunction(easeOutExpo);
                break;
            case EaseType.easeInOutExpo:
                ease = new EasingFunction(easeInOutExpo);
                break;
            case EaseType.easeInCirc:
                ease = new EasingFunction(easeInCirc);
                break;
            case EaseType.easeOutCirc:
                ease = new EasingFunction(easeOutCirc);
                break;
            case EaseType.easeInOutCirc:
                ease = new EasingFunction(easeInOutCirc);
                break;
            case EaseType.linear:
                ease = new EasingFunction(linear);
                break;
            case EaseType.spring:
                ease = new EasingFunction(spring);
                break;
            case EaseType.bounce:
                ease = new EasingFunction(bounce);
                break;
            case EaseType.easeInBack:
                ease = new EasingFunction(easeInBack);
                break;
            case EaseType.easeOutBack:
                ease = new EasingFunction(easeOutBack);
                break;
            case EaseType.easeInOutBack:
                ease = new EasingFunction(easeInOutBack);
                break;
            case EaseType.elastic:
                ease = new EasingFunction(elastic);
                break;
            case EaseType.sineWave:
                ease = new EasingFunction(sin);
                break;
        }
    }
    #endregion

    #region Easing Curves
    private float linear(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, value);
    }
    private float sin(float start, float end, float value)
    {
        float delta = (value / (end - start)) * 180;
        return Mathf.Sin(Mathf.Deg2Rad * delta);
    }

    private float clerp(float start, float end, float value)
    {
        float min = 0.0f;
        float max = 360.0f;
        float half = Mathf.Abs((max - min) / 2.0f);
        float retval = 0.0f;
        float diff = 0.0f;
        if ((end - start) < -half)
        {
            diff = ((max - start) + end) * value;
            retval = start + diff;
        }
        else if ((end - start) > half)
        {
            diff = -((max - end) + start) * value;
            retval = start + diff;
        }
        else retval = start + (end - start) * value;
        return retval;
    }

    private float spring(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
        return start + (end - start) * value;
    }

    private float easeInQuad(float start, float end, float value)
    {
        end -= start;
        return end * value * value + start;
    }

    private float easeOutQuad(float start, float end, float value)
    {
        end -= start;
        return -end * value * (value - 2) + start;
    }

    private float easeInOutQuad(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end / 2 * value * value + start;
        value--;
        return -end / 2 * (value * (value - 2) - 1) + start;
    }

    private float easeInCubic(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value + start;
    }

    private float easeOutCubic(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * (value * value * value + 1) + start;
    }

    private float easeInOutCubic(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end / 2 * value * value * value + start;
        value -= 2;
        return end / 2 * (value * value * value + 2) + start;
    }

    private float easeInQuart(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value * value + start;
    }

    private float easeOutQuart(float start, float end, float value)
    {
        value--;
        end -= start;
        return -end * (value * value * value * value - 1) + start;
    }

    private float easeInOutQuart(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end / 2 * value * value * value * value + start;
        value -= 2;
        return -end / 2 * (value * value * value * value - 2) + start;
    }

    private float easeInQuint(float start, float end, float value)
    {
        end -= start;
        return end * value * value * value * value * value + start;
    }

    private float easeOutQuint(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * (value * value * value * value * value + 1) + start;
    }

    private float easeInOutQuint(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end / 2 * value * value * value * value * value + start;
        value -= 2;
        return end / 2 * (value * value * value * value * value + 2) + start;
    }

    private float easeInSine(float start, float end, float value)
    {
        end -= start;
        return -end * Mathf.Cos(value / 1 * (Mathf.PI / 2)) + end + start;
    }

    private float easeOutSine(float start, float end, float value)
    {
        end -= start;
        return end * Mathf.Sin(value / 1 * (Mathf.PI / 2)) + start;
    }

    private float easeInOutSine(float start, float end, float value)
    {
        end -= start;
        return -end / 2 * (Mathf.Cos(Mathf.PI * value / 1) - 1) + start;
    }

    private float easeInExpo(float start, float end, float value)
    {
        end -= start;
        return end * Mathf.Pow(2, 10 * (value / 1 - 1)) + start;
    }

    private float easeOutExpo(float start, float end, float value)
    {
        end -= start;
        return end * (-Mathf.Pow(2, -10 * value / 1) + 1) + start;
    }

    private float easeInOutExpo(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return end / 2 * Mathf.Pow(2, 10 * (value - 1)) + start;
        value--;
        return end / 2 * (-Mathf.Pow(2, -10 * value) + 2) + start;
    }

    private float easeInCirc(float start, float end, float value)
    {
        end -= start;
        return -end * (Mathf.Sqrt(1 - value * value) - 1) + start;
    }

    private float easeOutCirc(float start, float end, float value)
    {
        value--;
        end -= start;
        return end * Mathf.Sqrt(1 - value * value) + start;
    }

    private float easeInOutCirc(float start, float end, float value)
    {
        value /= .5f;
        end -= start;
        if (value < 1) return -end / 2 * (Mathf.Sqrt(1 - value * value) - 1) + start;
        value -= 2;
        return end / 2 * (Mathf.Sqrt(1 - value * value) + 1) + start;
    }

    private float bounce(float start, float end, float value)
    {
        value /= 1f;
        end -= start;
        if (value < (1 / 2.75f))
        {
            return end * (7.5625f * value * value) + start;
        }
        else if (value < (2 / 2.75f))
        {
            value -= (1.5f / 2.75f);
            return end * (7.5625f * (value) * value + .75f) + start;
        }
        else if (value < (2.5 / 2.75))
        {
            value -= (2.25f / 2.75f);
            return end * (7.5625f * (value) * value + .9375f) + start;
        }
        else
        {
            value -= (2.625f / 2.75f);
            return end * (7.5625f * (value) * value + .984375f) + start;
        }
    }

    private float easeInBack(float start, float end, float value)
    {
        end -= start;
        value /= 1;
        float s = 1.70158f;
        return end * (value) * value * ((s + 1) * value - s) + start;
    }

    private float easeOutBack(float start, float end, float value)
    {
        float s = 1.70158f;
        end -= start;
        value = (value / 1) - 1;
        return end * ((value) * value * ((s + 1) * value + s) + 1) + start;
    }

    private float easeInOutBack(float start, float end, float value)
    {
        float s = 1.70158f;
        end -= start;
        value /= .5f;
        if ((value) < 1)
        {
            s *= (1.525f);
            return end / 2 * (value * value * (((s) + 1) * value - s)) + start;
        }
        value -= 2;
        s *= (1.525f);
        return end / 2 * ((value) * value * (((s) + 1) * value + s) + 2) + start;
    }

    private float punch(float amplitude, float value)
    {
        float s = 9;
        if (value == 0)
        {
            return 0;
        }
        if (value == 1)
        {
            return 0;
        }
        float period = 1 * 0.3f;
        s = period / (2 * Mathf.PI) * Mathf.Asin(0);
        return (amplitude * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * 1 - s) * (2 * Mathf.PI) / period));
    }

    private float elastic(float start, float end, float value)
    {
        //Thank you to rafael.marteleto for fixing this as a port over from Pedro's UnityTween
        end -= start;

        float d = 1f;
        float p = d * .3f;
        float s = 0;
        float a = 0;

        if (value == 0) return start;

        if ((value /= d) == 1) return start + end;

        if (a == 0f || a < Mathf.Abs(end))
        {
            a = end;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(end / a);
        }

        return (a * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * d - s) * (2 * Mathf.PI) / p) + end + start);
    }

    #endregion

}
