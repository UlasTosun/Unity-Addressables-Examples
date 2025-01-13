using System.Collections.Generic;
using UnityEngine;



public class PrefabContainer : MonoBehaviour, IPrefabLoader {

    [SerializeField] private List<GameObject> _prefabs = new();

    private GameObject _loadedPrefab;



    public void LoadRandomPrefab() {
        if (_prefabs.Count == 0) {
            Debug.LogError("No prefabs to load");
            return;
        }

        if (_loadedPrefab != null)
            Destroy(_loadedPrefab);

        _loadedPrefab = Instantiate(_prefabs[Random.Range(0, _prefabs.Count)], transform);
    }



}
