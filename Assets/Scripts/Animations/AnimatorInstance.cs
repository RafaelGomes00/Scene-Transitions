using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class AnimatorInstance : MonoBehaviour
{
    [SerializeField] protected RectTransform canvas;
    [SerializeField] protected float duration = 0.75f;
    [SerializeField] protected AnimationCurve animationCurve;
    protected Action onFinish;

    protected bool initialized = false;
    protected float elapsedTime = 0;

    protected virtual void Initialize(Action onFinish, float duration, AnimationCurve animationCurve)
    {
        this.onFinish = onFinish;
        this.duration = duration;
        this.animationCurve = animationCurve;
    }

    protected void ResetValues(Action onFinish)
    {
        elapsedTime = 0;
        this.onFinish = onFinish;
    }

    private void Update()
    {
        if (!initialized) return;

        if (elapsedTime / duration <= 1)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / duration;

            Animate(percentageComplete);
        }
        else
        {
            onFinish?.Invoke();
            initialized = false;
        }
    }

    protected virtual void Animate(float percentageComplete) { }

    public virtual void FadeOut(Action onFinish) {}
}
