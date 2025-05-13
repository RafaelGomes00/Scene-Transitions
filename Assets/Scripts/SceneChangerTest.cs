using UnityEngine;

public class SceneChangerTest : MonoBehaviour
{    
    public void ChangeScene(string sceneName)
    {
        //First scene of the game must be initialized on LevelManager
        LevelManager.AddScene(LevelManager.GetCurrentSceneName());
        
        LevelManager.ChangeScene(sceneName);
    }
}
