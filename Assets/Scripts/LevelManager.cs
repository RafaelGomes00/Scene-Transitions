using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public static class LevelManager
{
    private static List<string> currentLoadedScenes = new List<string>();
    private static List<string> previousLoadedScenes = new List<string>();

    public static void LoadAdditiveScene(string sceneName)
    {
        SetAdditiveScene(sceneName);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public static AsyncOperation LoadAdditiveSceneAsync(string sceneName)
    {
        SetAdditiveScene(sceneName);
        return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public static void UnloadSceneAsync(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
        RemoveAdditiveScene(sceneName);
    }

    public static void AddScene(string scene)
    {
        SetAdditiveScene(scene);
    }

    public static void DebugSceneHierarchy()
    {
        string scenes = "";
        foreach (string scene in currentLoadedScenes)
        {
            scenes = scenes + scene + ", ";
        }
        Debug.Log(scenes);
    }

    public static string GetCurrentSceneName()
    {
        if (currentLoadedScenes.Count < 1) return SceneManager.GetActiveScene().name;
        return currentLoadedScenes[currentLoadedScenes.Count - 1];
    }

    public static void ChangeScene(string sceneName, AnimationType animType)
    {
        MonolessCoroutine.Run(ChangeSceneRoutine(sceneName, animType));
    }

    public static void ChangeScene(string sceneName)
    {
        MonolessCoroutine.Run(ChangeSceneRoutine(sceneName));
    }

    private static IEnumerator ChangeSceneRoutine(string sceneName)
    {
        AsyncOperation async = LoadTransitioonScene();
        yield return new WaitUntil(() => async.isDone);
        TransitionController transitionController = Utilities.FindGameObjectByTagInSpecificScene("TransitionScene", "TransitionController").GetComponent<TransitionController>();

        transitionController.ShowLoadingScene(sceneName, delegate { UnloadSceneAsync("TransitionScene"); UnloadPreviousScenes(); });

    }

    private static IEnumerator ChangeSceneRoutine(string sceneName, AnimationType animType)
    {
        AsyncOperation async = LoadTransitioonScene();
        yield return new WaitUntil(() => async.isDone);

        TransitionController transitionController = Utilities.FindGameObjectByTagInSpecificScene("TransitionScene", "TransitionController").GetComponent<TransitionController>();

        AsyncOperation asyncOp = LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = false;
        transitionController.Animate(animType, () => MonolessCoroutine.Run(FinishLoadingSceneRoutine(asyncOp, transitionController, animType)));
    }

    private static AsyncOperation LoadTransitioonScene()
    {
        previousLoadedScenes = new List<string>(currentLoadedScenes);
        return LoadAdditiveSceneAsync("TransitionScene");
    }

    private static void RemoveAdditiveScene(string sceneName)
    {
        currentLoadedScenes.Remove(sceneName);
    }

    private static IEnumerator FinishLoadingSceneRoutine(AsyncOperation newSceneAsyncOp, TransitionController transitionController, AnimationType animType = AnimationType.Fade)
    {
        newSceneAsyncOp.allowSceneActivation = true;
        yield return new WaitUntil(() => newSceneAsyncOp.isDone);
        UnloadPreviousScenes();
        transitionController.AnimateFadeOut(() => UnloadSceneAsync("TransitionScene"));
    }

    private static AsyncOperation LoadSceneAsync(string sceneName)
    {
        return LoadAdditiveSceneAsync(sceneName);
    }

    private static void UnloadPreviousScenes()
    {
        foreach (string scene in previousLoadedScenes)
        {
            UnloadSceneAsync(scene);
        }
    }

    private static void SetAdditiveScene(string sceneName)
    {
        if (!currentLoadedScenes.Contains(sceneName))
            currentLoadedScenes.Add(sceneName);
    }
}
