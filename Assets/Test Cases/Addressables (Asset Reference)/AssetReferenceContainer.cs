using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;



public class AssetReferenceContainer : MonoBehaviour, IPrefabLoader {

    [SerializeField] private List<AssetReferenceGameObject> _assetReferences = new();

    private AsyncOperationHandle<GameObject> _loadedAsset;



    public void LoadRandomPrefab() {
        if (_assetReferences.Count == 0) {
            Debug.LogError("No assets to load");
            return;
        }

        if (_loadedAsset.IsValid())
            Addressables.Release(_loadedAsset);

        _loadedAsset = _assetReferences[Random.Range(0, _assetReferences.Count)].InstantiateAsync(transform);
    }



    void OnDestroy() {
        if (_loadedAsset.IsValid())
            Addressables.Release(_loadedAsset);
    }



}
