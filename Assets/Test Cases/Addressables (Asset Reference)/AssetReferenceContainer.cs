using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;



public class AssetReferenceContainer : MonoBehaviour, IPrefabLoader {

    [SerializeField] private List<AssetReferenceGameObject> _assetReferences = new();

    private AsyncOperationHandle<GameObject> _loadHandle;



    public void LoadRandomPrefab() {
        if (_assetReferences.Count == 0) {
            Debug.LogError("No assets to load");
            return;
        }

        if (_loadHandle.IsValid())
            Addressables.Release(_loadHandle);

        _loadHandle = _assetReferences[Random.Range(0, _assetReferences.Count)].InstantiateAsync(transform);
    }



    void OnDestroy() {
        if (_loadHandle.IsValid())
            Addressables.Release(_loadHandle);
    }



}
