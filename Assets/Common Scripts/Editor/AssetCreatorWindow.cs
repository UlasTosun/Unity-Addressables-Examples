using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class AssetCreatorWindow : EditorWindow {

    private AssetCreator _assetCreator;
    int _count = 100;
    [SerializeField] private List<Texture2D> _textureSources = new();
    [SerializeField] private List<Mesh> _meshSources = new();



    [MenuItem("Window/Asset Creator")]
    public static void ShowWindow() {
        GetWindow<AssetCreatorWindow>("Asset Creator");
    }



    private void OnGUI() {
        _assetCreator ??= new ("Assets/Created Assets");

        EditorGUILayout.BeginVertical();
        
        EditorGUILayout.Space(15);
        DisplayCount();
        EditorGUILayout.Space(15);
        DisplayTextures();
        EditorGUILayout.Space(15);
        DisplayMeshes();
        EditorGUILayout.Space(15);

        EditorGUILayout.BeginHorizontal();
        DisplayCreateButton();
        GUILayout.FlexibleSpace();
        DisplayRemoveButton();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();

    }



    private void DisplayCount() {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Number of assets to be created:", EditorStyles.boldLabel);
        _count = EditorGUILayout.IntField(GUIContent.none, _count, GUILayout.MaxWidth(50));
        EditorGUILayout.EndHorizontal();
    }



    private void DisplayTextures() {
        DisplayList(nameof(_textureSources), "Select textures to create assets from:");
    }



    private void DisplayMeshes() {
        DisplayList(nameof(_meshSources), "Select meshes to create assets from:");
    }



    private void DisplayList(string nameOfList, string label) {
        SerializedProperty targets = new SerializedObject(this).FindProperty(nameOfList);
        GUIContent labelField = new(label);
        EditorGUILayout.PropertyField(targets, labelField, true);
        targets.serializedObject.ApplyModifiedProperties();
    }



    private void DisplayCreateButton() {
        if (DisplayButton("Create Assets", Color.green))
            _assetCreator.Create(_count, _textureSources, _meshSources);
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