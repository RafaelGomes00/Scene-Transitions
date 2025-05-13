using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftToRightAnimationInstance : AnimatorInstance
{
    [SerializeField] private RectTransform blackImage;
    private float initialPos;
    private float targetValue;

    public void Initialize(Action onFinish)
    {
        blackImage.sizeDelta = canvas.rect.size;
        this.initialPos = -canvas.rect.width;
        blackImage.anchoredPosition = new Vector2(initialPos, 0);
        base.Initialize(onFinish, duration, animationCurve);
        FadeIn();
    }

    public void FadeIn()
    {
        targetValue = 0;
        initialized = true;
    }

    public override void FadeOut(Action onFinish)
    {
        ResetValues(onFinish);
        blackImage.sizeDelta = canvas.rect.size;
        blackImage.anchoredPosition = Vector2.zero;
        this.initialPos = 0;
        targetValue = -canvas.rect.width;
        initialized = true;
    }

    protected override void Animate(float percentageComplete)
    {
        blackImage.anchoredPosition = new Vector2(Mathf.Lerp(initialPos, targetValue, animationCurve.Evaluate(percentageComplete)), 0);
    }
}
