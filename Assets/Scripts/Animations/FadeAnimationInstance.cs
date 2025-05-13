using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimationInstance : AnimatorInstance
{
    [SerializeField] private Image fadeImage;
    private float initialImageAlpha;
    private float targetValue;

    public void Initialize(Action onFinish)
    {
        fadeImage.color = new Color(0, 0, 0, 0);
        this.initialImageAlpha = 0;
        base.Initialize(onFinish, duration, animationCurve);
        FadeIn();
    }

    public void FadeIn()
    {
        targetValue = 1;
        initialized = true;
    }

    public override void FadeOut(Action onFinish)
    {
        ResetValues(onFinish);
        fadeImage.color = new Color(0, 0, 0, 1);
        this.initialImageAlpha = 1;
        targetValue = 0;
        initialized = true;
    }

    protected override void Animate(float percentageComplete)
    {
        fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(initialImageAlpha, targetValue, animationCurve.Evaluate(percentageComplete)));
    }
}
