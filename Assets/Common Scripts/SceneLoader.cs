using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;



public class SceneLoader : MonoBehaviour {
    
    

    public void LoadScene(string sceneName) {
        float startTime = Time.realtimeSinceStartup;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        Debug.Log($"Scene {sceneName} loaded in {Time.realtimeSinceStartup - startTime} seconds.");
    }



    public void LoadAddressablesScene(string sceneName) {
        float startTime = Time.realtimeSinceStartup;
        Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        Debug.Log($"Scene {sceneName} loaded in {Time.realtimeSinceStartup - startTime} seconds.");
    }



}
