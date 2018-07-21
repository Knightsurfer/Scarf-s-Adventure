using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class DebugWindow : EditorWindow
{
    #region Debug Variables
    #region Toggle variables
    public bool active;
    #endregion
    #region Design Variables
    bool foldDebug = false;
    public Transform selectedTransform;
    Texture cameraIcon;
    Texture controlIcon;
    Texture debugIcon;

    #endregion
    #region  Camera Variables
    GameObject camera;
    public string[] cameraOptions = new string[] { "Follow", "Fly" };
    int cameraSelection;
    #endregion
    #region Control Variables
    public string[] controlOptions = new string[] { "Keyboard", "Gamepad" };
    int controlStyle;
    #endregion
    #endregion
    #region Mesh Variables
    #region Title
     bool levelFold = false;
    protected Texture levelIcon;
    #endregion
    #region Grid
    protected int numberofShapes;
    protected int shapesDecrement;
    protected int shapesIncrement;
    protected int defaultSelected;
    protected int customSelected;
    protected string textureSelected = "None";
    protected string shapeSelected = "None";
    protected object shapes;
    protected Object materialSelected;
    protected Object meshSelected;
    protected GameObject primitive;
    protected GameObject shape;
    protected Texture cubeIcon;
    protected Mesh[] mesh;
    #endregion
    #endregion

    #region starters
    [MenuItem("Custom Window/Debug Window")]
    static void ShowWindow()
    {
        GetWindow<DebugWindow>("Debug Tools");
    }
    void Initialize()
    {
        InitializeDebug();
        InitializeLevel();
    }
    void Items()
    {
        Foldouts();
    }
    #endregion

    Vector3 cameraPosition;
    Camera sceneCamera;

    private void OnGUI()
    {
        Initialize();
        Items();
    }

    



    #region Debug
    void InitializeDebug()
    {
        camera = GameObject.Find("Main Camera");
        cameraIcon = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Knight's Files/Resources/Graphics/Textures/Icon1.png", typeof(Texture));
        controlIcon = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Knight's Files/Resources/Graphics/Textures/Icon2.png", typeof(Texture));
        debugIcon = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Knight's Files/Resources/Graphics/Textures/Debug Menu.png", typeof(Texture));
    }
    void DockedDebugItems()
    {

        try
        {
            sceneCamera = SceneView.lastActiveSceneView.camera;
            cameraPosition = new Vector3(sceneCamera.transform.position.x + 6, sceneCamera.transform.position.y - 3, sceneCamera.transform.position.z);
        }

        catch
        {

        }

       



        #region cameraMode
        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        GUILayout.Label(cameraIcon, GUILayout.Width(40));
        GUILayout.BeginVertical();
        EditorGUILayout.LabelField("Camera Mode:", GUILayout.Width(90));
        cameraSelection = EditorGUILayout.Popup(cameraSelection, cameraOptions, GUILayout.Width(90));
        GUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();


        if(!camera.GetComponent<CameraFly>())
        {
            camera.AddComponent<CameraFly>();
            camera.AddComponent<CameraFollow>();
        }

        switch (cameraSelection)
        {
            case 0:
                camera.GetComponent<CameraFly>().enabled = false;
                camera.GetComponent<CameraFollow>().enabled = true;
                break;



            case 1:
                camera.GetComponent<CameraFly>().enabled = true;
                camera.GetComponent<CameraFollow>().enabled = false;
                break;
        }
        #endregion

        #region Control Mode
        GUILayout.BeginHorizontal();
        GUILayout.Label(controlIcon, GUILayout.Width(40));
        GUILayout.BeginVertical();
        EditorGUILayout.LabelField("Control Mode:", GUILayout.Width(90));
        controlStyle = EditorGUILayout.Popup(controlStyle, controlOptions, GUILayout.Width(90));
        GUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        switch (controlStyle)
        {
            case 0:

                break;



            case 1:

                break;
        }
        #endregion
    }
    #endregion


    #region Level Creation
    void DockedLevelItems()
    {
        if (GameObject.Find("Primitive Objects") != true)
        {
            primitive = new GameObject("Primitive Objects");
        }
        primitive = GameObject.Find("Primitive Objects");
        numberofShapes = primitive.transform.childCount+1;
        shapesDecrement = numberofShapes -= 1;
        shapesIncrement = numberofShapes += 1;
        GUILayout.Space(10);
        GUILayout.Box(new GUIContent("Choose Mesh"), GUILayout.Width(415), GUILayout.Height(410));

        #region Area Begin
        GUILayout.BeginArea(new Rect(10, 80, 500, 380));
        if (foldDebug) { GUILayout.Space(70); }
        EditorGUILayout.LabelField("Default Meshes", GUILayout.Width(500));
        defaultSelected = GUILayout.SelectionGrid(defaultSelected, new Texture[] { cubeIcon, cameraIcon, cameraIcon, cameraIcon, cameraIcon }, 5, GUILayout.Width(400), GUILayout.Height(80));
        switch (defaultSelected)
        {
            default:
                break;

            case 0:
                customSelected = -1;
                shapes = PrimitiveType.Cube;
                shapeSelected = "Cube";
                break;

            case 1:
                customSelected = -1;
                shapes = PrimitiveType.Sphere;
                shapeSelected = "Sphere";
                break;

            case 2:
                customSelected = -1;
                shapes = PrimitiveType.Cylinder;
                shapeSelected = "Cylinder";
                break;

            case 3:
                customSelected = -1;
                shapes = PrimitiveType.Plane;
                shapeSelected = "Plane";
                break;

            case 4:
                customSelected = -1;
                shapes = PrimitiveType.Quad;
                shapeSelected = "Quad";
                break;
        }

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Custom Meshes", GUILayout.Width(500));
        customSelected = GUILayout.SelectionGrid(customSelected, new Texture[] { }, 5, GUILayout.Width(400), GUILayout.Height(80));
        GUILayout.EndArea();
        #endregion Area End

        switch (customSelected)
        {
            default:
                defaultSelected = -1;
                break;


            case -1:
                break;



        }

       materialSelected = EditorGUILayout.ObjectField(materialSelected, typeof(Material),true,GUILayout.Width(415));
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Spawn Shape", GUILayout.Width(130), GUILayout.Height(35)))
        {  GameObject.CreatePrimitive((PrimitiveType)shapes);

                
                
                GameObject.Find(shapeSelected).name = (shapeSelected + numberofShapes);
                GameObject.Find(shapeSelected + numberofShapes).transform.SetParent(primitive.transform);
                GameObject.Find(shapeSelected + numberofShapes).transform.position = cameraPosition;


                if (materialSelected != null)
                {
                GameObject.Find(shapeSelected + numberofShapes).GetComponent<MeshRenderer>().sharedMaterial = (Material)materialSelected;
                }

        }
        

        if (GUILayout.Button("Despawn Shape", GUILayout.Width(130), GUILayout.Height(35)))
        {
            if (numberofShapes > 0)
            {
                DestroyImmediate(GameObject.Find(shapeSelected + numberofShapes));
                DestroyImmediate(GameObject.Find("Cube" + shapesDecrement));
                DestroyImmediate(GameObject.Find("Sphere" + shapesDecrement));
                DestroyImmediate(GameObject.Find("Cylinder" + shapesDecrement));
                DestroyImmediate(GameObject.Find("Plane" + shapesDecrement));
                DestroyImmediate(GameObject.Find("Quad" + shapesDecrement));
            }


        }
        GUILayout.BeginVertical();
        meshSelected = EditorGUILayout.ObjectField(meshSelected, typeof(Mesh), true, GUILayout.Width(146));
        
        if (GUILayout.Button("Add Mesh...", GUILayout.Width(145)))
        {
           

        }
        GUILayout.EndVertical();
       




        GUILayout.BeginArea(new Rect(0, 610, 500, 20));
        EditorGUILayout.LabelField("Selected Item: " + shapeSelected + " | Selected Texture: " + textureSelected + " | Spawned Objects: " + shapesDecrement, GUILayout.Width(500));
        GUILayout.EndArea();
        EditorGUILayout.LabelField("Camera Position: " + cameraPosition, GUILayout.Width(500));
    }
    void InitializeLevel()
    {
        cubeIcon = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Knight's Files/Resources/Graphics/Editor/Cube.png", typeof(Texture));
        levelIcon = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Main Stuff/Objects/Terrain/New Terrain 1.asset", typeof(Texture));
    }




    #endregion





    void Foldouts()
    {
        #region Debug Tools
        foldDebug = EditorGUILayout.InspectorTitlebar(foldDebug, debugIcon);
        if (foldDebug)
        {
            DockedDebugItems();
        }
        #endregion

        #region  Level Tools
        levelFold = EditorGUILayout.InspectorTitlebar(levelFold, GameObject.Find("Main Camera").GetComponent<Terrain>());
        if (levelFold)
        {
            DockedLevelItems();
        }
        #endregion
    }



    void OnDrawGizmosSelected()
    {
        float radius = 1.5f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(cameraPosition + new Vector3(4, 0, 0), radius );

    }
















    void AbandonedProjects()
    {

        #region Active Toggle

        active = GUILayout.Toggle(active, "Active");
        foreach (GameObject obj in Selection.gameObjects)
        {
            obj.SetActive(active);
            active = obj.activeSelf;
        }

        #endregion
    }

}









