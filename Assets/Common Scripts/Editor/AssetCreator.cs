using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class AssetCreator {

    private string _rootPath;






    public AssetCreator(string rootPath) {
        _rootPath = rootPath;
    }



    public void Create(int count, List<Texture2D> textureSources, List<Mesh> meshSources) {
        Debug.Log("Creating assets...");

        CreateTextures(textureSources, count);
        AssetDatabase.Refresh();

        CreateMaterials(count);
        AssetDatabase.Refresh();

        CreateMeshes(meshSources, count); 
        AssetDatabase.Refresh();

        CreatePrefabs(count);
        AssetDatabase.Refresh();

        CreateCompoundPrefabs(count);
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



    private void CreateTextures(List<Texture2D> textureSources, int count) {
        string path = _rootPath + "/Textures";
        Directory.CreateDirectory(path);

        for (int i = 0; i < count; i++) {
            int source = Random.Range(0, textureSources.Count);
            File.WriteAllBytes(path + "/Texture_" + i + ".png", textureSources[source].EncodeToPNG());
        }
    }



    private void CreateMaterials(int count) {
        string path = _rootPath + "/Materials";
        Directory.CreateDirectory(path);

        for (int i = 0; i < count; i++) {
            Material material = new (Shader.Find("Universal Render Pipeline/Lit"));

            // Load and assign a texture from the textures folder
            Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(_rootPath + "/Textures/Texture_" + i + ".png");
            material.mainTexture = texture;
            
            AssetDatabase.CreateAsset(material, path + "/Material_" + i + ".mat");
        }
    }



    private void CreateMeshes(List<Mesh> meshSources, int count) {
        string path = _rootPath + "/Meshes";
        Directory.CreateDirectory(path);

        for (int i = 0; i < count; i++) {
            int source = Random.Range(0, meshSources.Count);
            Mesh mesh = Object.Instantiate(meshSources[source]);
            AssetDatabase.CreateAsset(mesh, path + "/Mesh_" + i + ".asset");
        }
    }



    private void CreatePrefabs(int count) {
        string path = _rootPath + "/Prefabs";
        Directory.CreateDirectory(path);

        for (int i = 0; i < count; i++) {
            GameObject prefab = new ("Prefab_" + i);
            prefab.AddComponent<MeshFilter>();
            prefab.AddComponent<MeshRenderer>();

            // Assign a mesh from the meshes folder
            Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(_rootPath + "/Meshes/Mesh_" + i + ".asset");
            prefab.GetComponent<MeshFilter>().sharedMesh = mesh;

            // Assign a material from the materials folder
            Material material = AssetDatabase.LoadAssetAtPath<Material>(_rootPath + "/Materials/Material_" + i + ".mat");
            prefab.GetComponent<MeshRenderer>().sharedMaterial = material;

            PrefabUtility.SaveAsPrefabAsset(prefab, path + "/Prefab_" + i + ".prefab");
            Object.DestroyImmediate(prefab);
        }
    }



    private void CreateCompoundPrefabs(int count) {
        string path = _rootPath + "/Compound Prefabs";
        Directory.CreateDirectory(path);

        for (int i = 0; i < count; i++) {
            GameObject compoundPrefab = new ("CompoundPrefab_" + i);

            // Assign random number of prefabs to the compound prefab
            int prefabCount = Random.Range(1, 5);
            for (int j = 0; j < prefabCount; j++) {
                int prefabIndex = (i + j) % count;
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
