using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;



public class SceneLoader : MonoBehaviour {



    public void LoadScene(string sceneName) {
        float startTime = Time.realtimeSinceStartup;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        Debug.Log($"Scene {sceneName} loaded in {Time.realtimeSinceStartup - startTime} seconds.");
    }



    public void LoadAddressablesScene(string sceneName) {
        float startTime = Time.realtimeSinceStartup;
        Application.backgroundLoadingPriority = ThreadPriority.High;
        AsyncOperationHandle<SceneInstance> loadHandle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        loadHandle.Completed += (handle) => {
            Debug.Log($"Scene {sceneName} loaded in {Time.realtimeSinceStartup - startTime} seconds.");
            Application.backgroundLoadingPriority = ThreadPriority.Normal;
        };
    }



}
