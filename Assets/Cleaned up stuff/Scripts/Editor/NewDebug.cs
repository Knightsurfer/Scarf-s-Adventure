using UnityEngine;
using UnityEditor;

public class NewDebug : EditorWindow
{
    #region Variables
   
    int gameType = 4;

    CameraMode camMode;

    protected bool debugToggle;
    protected string[] gameOptions = new string[] { "Spectate", "First Person", "Third Person", "Real Time Strategy", "Misc" };

    protected GameObject camera;
    

    protected Texture debugIcon;
    protected Texture controllerIcon;

    

    #endregion



    [MenuItem("Debug/Game Settings")]
    static void ShowWindow()
    {
        
        
        GetWindow<NewDebug>("Debug");


    }

    #region Default Functions
 
    void OnGUI()
    {
        if (Application.isEditor)
        {
            StartDebug();
            UpdateDebug();
        }
    }
    #endregion



    void StartDebug()
    {
        #region Textures


        debugIcon = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Cleaned up stuff/Scripts/Editor/Textures/Game Settings.png", typeof(Texture));
        controllerIcon = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Cleaned up stuff/Scripts/Editor/Textures/Controller.png", typeof(Texture));

        #endregion
        #region GameObjects
        camMode = GameObject.Find("Game Manager").GetComponent<CameraMode>();
        #endregion
    }

    void UpdateDebug()
    {
        #region Debug Tools
        GameSettings();
        #endregion

    }


        void GameSettings()
        {
            GUILayout.Box(new GUIContent(""), GUILayout.Width(215), GUILayout.Height(30));
            GUILayout.BeginArea(new Rect(0, 0, 200, 200));
            EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.Label(controllerIcon, GUILayout.Width(37), GUILayout.Height(20));
            EditorGUILayout.LabelField("Game Settings", EditorStyles.boldLabel, GUILayout.Width(100));
            GUILayout.EndArea();
            EditorGUILayout.EndHorizontal();
            GUILayout.BeginArea(new Rect(3.5f, 31, 415, 200));
        
            GUILayout.Box(new GUIContent(""), GUILayout.Width(215), GUILayout.Height(100));
            GUILayout.BeginArea(new Rect(5,10,200,200));
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Game Type:", GUILayout.Width(75));
            gameType = EditorGUILayout.Popup(gameType, gameOptions, GUILayout.Width(115));
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            GUILayout.EndArea();


        switch (gameType)
        {
            default:
                camMode.gameType = "FirstPerson";
                break;

            case 0:
                camMode.gameType = "Spectate";
                break;

            case 1:
                camMode.gameType = "FirstPerson";
                break;


            case 2:
                camMode.gameType = "ThirdPerson";
                break;

            case 3:
                camMode.gameType = "RealTimeStratedgy";
                break;

            case 4:
                camMode.gameType = "Misc";
                break;

        }





    }
    













}
