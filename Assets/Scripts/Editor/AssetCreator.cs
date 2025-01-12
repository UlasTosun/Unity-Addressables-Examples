using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class AssetCreator {

    private string _rootPath;

    private List<Texture2D> _textureSources = new();
    private int _textureCount = 0;

    private List<Mesh> _meshSources = new();
    private int _meshCount = 0;

    private int _materialCount = 0;
    private int _prefabCount = 0;
    private int _compoundPrefabCount = 0;




    public AssetCreator(string rootPath) {
        _rootPath = rootPath;
    }



    public AssetCreator SetTextures(List<Texture2D> source, int count) {
        _textureSources = source;
        _textureCount = count;
        return this;
    }



    public AssetCreator SetMaterials(int count) {
        _materialCount = count;
        return this;
    }



    public AssetCreator SetMeshes(List<Mesh> source, int count) {
        _meshSources = source;
        _meshCount = count;
        return this;
    }



    public AssetCreator SetPrefabs(int count) {
        _prefabCount = count;
        return this;
    }



    public AssetCreator SetCompoundPrefabs(int count) {
        _compoundPrefabCount = count;
        return this;
    }



    public void Create() {
        Debug.Log("Creating assets...");

        CreateTextures();
        AssetDatabase.Refresh();

        CreateMaterials();
        AssetDatabase.Refresh();

        CreateMeshes(); 
        AssetDatabase.Refresh();

        CreatePrefabs();
        AssetDatabase.Refresh();

        CreateCompoundPrefabs();
        AssetDatabase.Refresh();

        Debug.Log("Assets created successfully.");
    }



    public void Remove() {
        if (Directory.Exists(_rootPath)) {

            Directory.Delete(_rootPath, true);
            AssetDatabase.Refresh();
            Debug.Log("Assets removed successfully.");

        } else {
            Debug.Log("No assets to remove.");
        }
    }



    private void CreateTextures() {
        string path = _rootPath + "/Textures";
        Directory.CreateDirectory(path);

        for (int i = 0; i < _textureCount; i++) {
            int source = Random.Range(0, _textureSources.Count);
            File.WriteAllBytes(path + "/Texture_" + i + ".png", _textureSources[source].EncodeToPNG());
        }
    }



    private void CreateMaterials() {
        string path = _rootPath + "/Materials";
        Directory.CreateDirectory(path);

        for (int i = 0; i < _materialCount; i++) {
            Material material = new (Shader.Find("Universal Render Pipeline/Lit"));

            // Load and assign a random texture from the textures folder
            int textureIndex = Random.Range(0, _textureCount);
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(_rootPath + "/Textures/Texture_" + textureIndex + ".png");
            material.mainTexture = texture;
            
            AssetDatabase.CreateAsset(material, path + "/Material_" + i + ".mat");
        }
    }



    private void CreateMeshes() {
        string path = _rootPath + "/Meshes";
        Directory.CreateDirectory(path);

        for (int i = 0; i < _meshCount; i++) {
            int source = Random.Range(0, _meshSources.Count);
            Mesh mesh = Object.Instantiate(_meshSources[source]);
            AssetDatabase.CreateAsset(mesh, path + "/Mesh_" + i + ".asset");
        }
    }



    private void CreatePrefabs() {
        string path = _rootPath + "/Prefabs";
        Directory.CreateDirectory(path);

        for (int i = 0; i < _prefabCount; i++) {
            GameObject prefab = new GameObject("Prefab_" + i);
            prefab.AddComponent<MeshFilter>();
            prefab.AddComponent<MeshRenderer>();

            // Assign a random mesh from the meshes folder
            int meshIndex = Random.Range(0, _meshCount);
            Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(_rootPath + "/Meshes/Mesh_" + meshIndex + ".asset");
            prefab.GetComponent<MeshFilter>().sharedMesh = mesh;

            // Assign a random material from the materials folder
            int materialIndex = Random.Range(0, _materialCount);
            Material material = AssetDatabase.LoadAssetAtPath<Material>(_rootPath + "/Materials/Material_" + materialIndex + ".mat");
            prefab.GetComponent<MeshRenderer>().sharedMaterial = material;

            PrefabUtility.SaveAsPrefabAsset(prefab, path + "/Prefab_" + i + ".prefab");
            Object.DestroyImmediate(prefab);
        }
    }



    private void CreateCompoundPrefabs() {
        string path = _rootPath + "/Compound Prefabs";
        Directory.CreateDirectory(path);

        for (int i = 0; i < _compoundPrefabCount; i++) {
            GameObject compoundPrefab = new GameObject("CompoundPrefab_" + i);

            // Assign a random number of prefabs to the compound prefab
            int prefabCount = Random.Range(1, 5);
            for (int j = 0; j < prefabCount; j++) {
                int prefabIndex = Random.Range(0, _prefabCount);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(_rootPath + "/Prefabs/Prefab_" + prefabIndex + ".prefab");
                GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                instance.transform.SetParent(compoundPrefab.transform);
                instance.transform.localPosition = Random.insideUnitSphere * 5;
            }

            PrefabUtility.SaveAsPrefabAsset(compoundPrefab, path + "/CompoundPrefab_" + i + ".prefab");
            Object.DestroyImmediate(compoundPrefab);
        }
        
    }



}
