using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class SuperInspector : EditorWindow
{
    string transform = "";
    string camera = "";
    string spriteRenderer = "";
    string meshRenderer = "";
    string meshFilter = "";
    string boxCollider = "";

    int selectedComponent;
    

    [MenuItem("Custom Window/Super Inspector", priority = 100)]
    static void Init()
    {
        SuperInspector window = GetWindow<SuperInspector>();
        window.Show();
    }
    private void OnGUI()
    {
        string[] component = new string[100];

        selectedComponent = EditorGUILayout.Popup(selectedComponent, new string[] { transform, camera, spriteRenderer, meshRenderer,meshFilter, boxCollider }, GUILayout.Width(90));
    }












}
