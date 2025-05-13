using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonolessCoroutine : MonoBehaviour
{
    private static GameObject gObject;
    private static MonolessCoroutine instance;

    public static Coroutine Run(IEnumerator enumerator)
    {
        if (gObject == null)
        {
            gObject = new GameObject("CoroutineRunner");
            instance = gObject.AddComponent<MonolessCoroutine>();
            DontDestroyOnLoad(gObject);
        }
        var coroutine = instance.StartCoroutine(enumerator);
        return coroutine;
    }

    public static void Stop(Coroutine coroutine)
    {
        if (gObject == null)
        {
            Debug.LogError("Coroutine not initialized");
            return;
        }

        instance.StopCoroutine(coroutine);
    }

    public static void Stop(IEnumerator coroutine)
    {
        if (gObject == null)
        {
            Debug.LogError("Coroutine not initialized");
            return;
        }

        instance.StopCoroutine(coroutine);
    }

    public static void StopAll()
    {
        if (instance == null)
            return;
        instance.StopAllCoroutines();
    }
}
