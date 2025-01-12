using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneLoader : MonoBehaviour {
    
    

    public void LoadScene(string sceneName) {
        float startTime = Time.realtimeSinceStartup;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        Debug.Log($"Scene {sceneName} loaded in {Time.realtimeSinceStartup - startTime} seconds.");
    }



}
