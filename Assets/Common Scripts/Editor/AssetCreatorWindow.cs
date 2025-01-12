using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class AssetCreatorWindow : EditorWindow {

    private AssetCreator _assetCreator;

    [SerializeField] private List<Texture2D> _textureSources = new();
    private int _textureCount = 0;

    [SerializeField] private List<Mesh> _meshSources = new();
    private int _meshCount = 0;

    private int _materialCount = 0;
    private int _prefabCount = 0;
    private int _compoundPrefabCount = 0;




    [MenuItem("Window/Asset Creator")]
    public static void ShowWindow() {
        GetWindow<AssetCreatorWindow>("Asset Creator");
    }



    private void OnGUI() {
        _assetCreator ??= new ("Assets/Created Assets");

        EditorGUILayout.BeginVertical();

        DisplayTextures();
        EditorGUILayout.Space(15);
        DisplayMeshes();
        EditorGUILayout.Space(15);
        DisplayMaterials();
        EditorGUILayout.Space(15);
        DisplayPrefabs();
        EditorGUILayout.Space(15);
        DisplayCompoundPrefabs();
        EditorGUILayout.Space(15);

        EditorGUILayout.BeginHorizontal();
        DisplayCreateButton();
        GUILayout.FlexibleSpace();
        DisplayRemoveButton();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();

    }



    private void DisplayTextures() {
        EditorGUILayout.LabelField("Texture Settings", EditorStyles.whiteLargeLabel);

        EditorGUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Number of textures to be created:", EditorStyles.boldLabel);
        _textureCount = EditorGUILayout.IntField(GUIContent.none, _textureCount, GUILayout.MaxWidth(50));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(5);

        SerializedProperty targets = new SerializedObject(this).FindProperty(nameof(_textureSources));
        GUIContent label = new("Select textures to create assets from:");
        EditorGUILayout.PropertyField(targets, label, true);
        targets.serializedObject.ApplyModifiedProperties();
    }



    private void DisplayMeshes() {
        EditorGUILayout.LabelField("Mesh Settings", EditorStyles.whiteLargeLabel);

        EditorGUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Number of meshes to be created:", EditorStyles.boldLabel);
        _meshCount = EditorGUILayout.IntField(GUIContent.none, _meshCount, GUILayout.MaxWidth(50));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(5);

        SerializedProperty targets = new SerializedObject(this).FindProperty(nameof(_meshSources));
        GUIContent label = new("Select meshes to create assets from:");
        EditorGUILayout.PropertyField(targets, label, true);
        targets.serializedObject.ApplyModifiedProperties();
    }



    private void DisplayMaterials() {
        EditorGUILayout.LabelField("Material Settings", EditorStyles.whiteLargeLabel);

        EditorGUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Number of materials to be created:", EditorStyles.boldLabel);
        _materialCount = EditorGUILayout.IntField(GUIContent.none, _materialCount, GUILayout.MaxWidth(50));
        EditorGUILayout.EndHorizontal();
    }



    private void DisplayPrefabs() {
        EditorGUILayout.LabelField("Prefab Settings", EditorStyles.whiteLargeLabel);

        EditorGUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Number of prefabs to be created:", EditorStyles.boldLabel);
        _prefabCount = EditorGUILayout.IntField(GUIContent.none, _prefabCount, GUILayout.MaxWidth(50));
        EditorGUILayout.EndHorizontal();
    }



    private void DisplayCompoundPrefabs() {
        EditorGUILayout.LabelField("Compound Prefab Settings", EditorStyles.whiteLargeLabel);

        EditorGUILayout.Space(5);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Number of compound prefabs to be created:", EditorStyles.boldLabel);
        _compoundPrefabCount = EditorGUILayout.IntField(GUIContent.none, _compoundPrefabCount, GUILayout.MaxWidth(50));
        EditorGUILayout.EndHorizontal();
    }



    private void DisplayCreateButton() {
        
        if (DisplayButton("Create Assets", Color.green)) {
            _assetCreator
                .SetTextures(_textureSources, _textureCount)
                .SetMeshes(_meshSources, _meshCount)
                .SetMaterials(_materialCount)
                .SetPrefabs(_prefabCount)
                .SetCompoundPrefabs(_compoundPrefabCount)
                .Create();
        }

    }



    private void DisplayRemoveButton() {
        if (DisplayButton("Remove Assets", Color.red))
            _assetCreator.Remove();
    }



    private bool DisplayButton(string text, Color color) {
        GUIStyle style = new(GUI.skin.button);
        style.fontSize = 16;
        style.fontStyle = FontStyle.Bold;
        Color originalColor = GUI.backgroundColor;
        GUI.backgroundColor = color;

        bool result = GUILayout.Button(text, style, GUILayout.MaxWidth(250), GUILayout.Height(30));

        GUI.backgroundColor = originalColor;

        return result;
    }



}