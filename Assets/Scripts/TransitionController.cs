using UnityEngine;
using System;

public class TransitionController : MonoBehaviour
{
    [Header("Loading scene")]
    [SerializeField] private LoadingController loadingController;

    [Header("Animations")]
    [SerializeField] private ToonAnimInstance toonAnimGO;
    [SerializeField] private FadeAnimationInstance fadeAnimGO;
    [SerializeField] private RightToLeftAnimationInstance rightToLeftAnimGO;
    [SerializeField] private LeftToRightAnimationInstance leftToRightAnimGO;

    private AnimatorInstance currentAnimInstance;

    public void ShowLoadingScene(string sceneName, Action onFinish)
    {
        LoadingController loadingC = Instantiate(loadingController, transform);
        loadingC.Initialize(sceneName, onFinish);
    }

    public void Animate(AnimationType animType, Action onFinish)
    {
        switch (animType)
        {

            case AnimationType.Fade:
                FadeAnimation(onFinish);
                break;
            case AnimationType.Toon:
                ToonAnimation(onFinish);
                break;
            case AnimationType.LeftToRight:
                LeftToRightAnimation(onFinish);
                break;
            case AnimationType.RightToLeft:
                RightToLeftAnimation(onFinish);
                break;
            default:
                Debug.LogError($"Animation of type {animType} not supported.");
                break;
        }
    }

    public void AnimateFadeOut(Action onFinish)
    {
        currentAnimInstance.FadeOut(onFinish);
    }

    private void RightToLeftAnimation(Action onFinish)
    {
        RightToLeftAnimationInstance rightToLeftAnimation = Instantiate(rightToLeftAnimGO, transform);
        currentAnimInstance = rightToLeftAnimation;
        rightToLeftAnimation.Initialize(onFinish);
    }

    private void LeftToRightAnimation(Action onFinish)
    {
        LeftToRightAnimationInstance leftToRightAnimation = Instantiate(leftToRightAnimGO, transform);
        currentAnimInstance = leftToRightAnimation;
        leftToRightAnimation.Initialize(onFinish);
    }

    private void ToonAnimation(Action onFinish)
    {
        ToonAnimInstance toonAnimInstance = Instantiate(toonAnimGO, transform);
        currentAnimInstance = toonAnimInstance;
        toonAnimInstance.Initialize(onFinish);
    }

    private void FadeAnimation(Action onFinish)
    {
        FadeAnimationInstance fadeAnimInstance = Instantiate(fadeAnimGO, transform);
        currentAnimInstance = fadeAnimInstance;
        fadeAnimInstance.Initialize(onFinish);
    }
}
