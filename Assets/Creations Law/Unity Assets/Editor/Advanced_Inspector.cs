
using UnityEngine;

using UnityEditor;
using ScriptableObjects;

public class Advanced_Inspector : EditorWindow
{



    [MenuItem("Custom Tools/Inspector")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<Advanced_Inspector>("Super Inspector");
    }





    void OnGUI()
    {

        if (Selection.activeGameObject.GetComponent<PlayerController>())
        {
            PlayerController player = Selection.activeGameObject.GetComponent<PlayerController>();

            GUILayout.Box(player.playerInfo.portrait.texture, GUILayout.Width(100), GUILayout.Height(100));
            //GUILayout.Space(20);
            GUILayout.Box("Health: " + player.health.ToString() + "\nMagic: " + player.magic.ToString(), GUILayout.Width(100));


        }
    }
      


}


