using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    private Action onFinishLoading;

    public void Initialize(string sceneName, Action onFinishLoading)
    {
        //Add loading routine here.
        StartCoroutine(LoadSceneAsync(sceneName, onFinishLoading));
    }

    private IEnumerator LoadSceneAsync(string sceneName, Action onFinishLoading)
    {
        AsyncOperation async = LevelManager.LoadAdditiveSceneAsync(sceneName);
        yield return new WaitUntil(() => async.isDone);
        onFinishLoading?.Invoke();
    }
}
