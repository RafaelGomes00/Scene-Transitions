using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToonAnimInstance : AnimatorInstance
{
    [SerializeField] private RectTransform circleImage;
    [SerializeField] private RectTransform blackImage;
    private Vector2 initialImageSize;
    private Vector2 targetSize;

    public void Initialize(Action onFinish)
    {
        circleImage.sizeDelta = new Vector2(canvas.rect.height * 2, canvas.rect.height * 2);
        blackImage.sizeDelta = new Vector2(canvas.rect.width, canvas.rect.height);
        this.initialImageSize = circleImage.sizeDelta;
        base.Initialize(onFinish, duration, animationCurve);
        FadeIn();
    }

    public void FadeIn()
    {
        targetSize = Vector2.zero;
        initialized = true;
    }

    public override void FadeOut(Action onFinish)
    {
        ResetValues(onFinish);
        circleImage.sizeDelta = Vector2.zero;
        this.initialImageSize = Vector2.zero;
        targetSize = new Vector2(canvas.rect.height * 2, canvas.rect.height * 2);
        initialized = true;
    }

    protected override void Animate(float percentageComplete)
    {
        circleImage.sizeDelta = Vector2.Lerp(initialImageSize, targetSize, animationCurve.Evaluate(percentageComplete));
    }
}
