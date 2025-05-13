using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utilities
{
    public static GameObject FindGameObjectByTagInSpecificScene(string sceneName, string tag)
    {
        return FindGameObject(SceneManager.GetSceneByName(sceneName), tag);
    }

    public static GameObject FindGameObjectByTagInSpecificScene(Scene scene, string tag)
    {
        return FindGameObject(scene, tag);
    }

    private static GameObject FindGameObject(Scene scene, string tag)
    {
        List<GameObject> goList = GetAllGameObjectsInAScene(scene);

        foreach (GameObject obj in goList)
        {
            if (obj.CompareTag(tag))
            {
                return obj;
            }
        }

        return null;
    }

    public static List<GameObject> GetAllGameObjectsInAScene(Scene scene)
    {
        GameObject[] objects = scene.GetRootGameObjects();

        List<GameObject> goList = new List<GameObject>();

        foreach (GameObject obj in objects)
        {
            Transform[] transform = obj.GetComponentsInChildren<Transform>();

            foreach (Transform trans in transform)
            {
                goList.Add(trans.gameObject);
            }
        }

        return goList;
    }

    public static Scene GetLastSceneOfName(string sceneName)
    {
        Scene scene = SceneManager.GetSceneAt(0);
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene tempScene = SceneManager.GetSceneAt(i);
            if (tempScene.name == sceneName)
            {
                scene = tempScene;
            }
        }

        return scene;
    }
}
