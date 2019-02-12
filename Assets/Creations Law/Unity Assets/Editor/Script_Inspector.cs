using UnityEngine;
using UnityEditor;

//###################################//
//                                                                              //
//            SCRIPT INSPECTORS                                  //
//                                                                            //
//                                                                           //
//#################################//
//                                                                          //
//    When a script is being viewed as a component,  //
//    this script comes into action in order to make   //
//    it easier to use.                                            //
//                                                                     //
///////////////////////////////////////////////////////////



[CustomEditor(typeof(GameManager))]
public class Viewer_GameManager : Editor
{
    #region Main Layer

    protected int debugView;
    protected int defaultView;
    protected bool Initialized;

    GameManager unit;
    BotReciever[] bots = new BotReciever[] { };
    PlayerController[] people = new PlayerController[] { };
    #endregion
    #region Default Layer
    bool party;
    bool settings;
    bool controllerTest;
    #endregion

    void Init()
    {
        if (!Initialized)
        {
            unit = (GameManager)target;
            Initialized = true;
        }
    }
    protected void Inspector()
    {
        Repaint();
        #region Find Objects
        people = FindObjectsOfType<PlayerController>();
        bots = FindObjectsOfType<BotReciever>();
        #endregion
        defaultView = GUILayout.Toolbar(defaultView, new[] { "Controller Test", "Game Settings", "Party", "Inventory" });
        switch (defaultView)
        {
            case 0:
                party = false;
                settings = false;
                controllerTest = true;
                break;

            case 1:
                party = false;
                settings = true;
                controllerTest = false;
                break;

            case 2:
                party = true;
                settings = false;
                controllerTest = false;
                break;
        }
        PartyViewer();
        SettingsViewer();
        ControllerViewer();
        
    }
    public override void OnInspectorGUI()
    {
        Init();
        BaseStats();
    }
    private void BaseStats()
    {
        debugView = GUILayout.Toolbar(debugView, new[] { "Default", "Debug" }, GUILayout.Height(40));
        switch (debugView)
        {
            case 0:
                Inspector();
                break;


            case 1:
                base.OnInspectorGUI();
                break;
        }
    }
    private void PartyViewer()
    {
        if (party && people.Length > 0)
        {
            foreach (PlayerController p in people)
            {
                GUILayout.BeginVertical("In BigTitle");

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(p.name);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("HP: ", GUILayout.Width(70));
                GUILayout.Label(p.health.ToString());
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("MP: ", GUILayout.Width(70));
                GUILayout.Label(p.magic.ToString());
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Level: ", GUILayout.Width(70));
                GUILayout.Label(p.level.ToString());
                EditorGUILayout.EndHorizontal();

                GUILayout.EndVertical();
            }
            foreach (BotReciever b in bots)
            {
                GUILayout.BeginVertical("In BigTitle");

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(b.name);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("HP: ", GUILayout.Width(70));
                GUILayout.Label(b.health.ToString());
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("MP: ", GUILayout.Width(70));
                GUILayout.Label(b.magic.ToString());
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Level: ", GUILayout.Width(70));
                GUILayout.Label(b.level.ToString());
                EditorGUILayout.EndHorizontal();

                GUILayout.EndVertical();
            }
        }
    }
    private void SettingsViewer()
    {
        if (settings)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Controller: ", GUILayout.Width(80));

            switch (unit.controller)
            {
                default:
                    GUILayout.Label(unit.controller);
                    break;

                case "":
                    GUILayout.Label("None");
                    break;

                case "Wireless Controller":
                    GUILayout.Label("PS4");
                    break;

                case "Controller (Xbox 360 Wireless Receiver for Windows)":
                    GUILayout.Label("Xbox");
                    break;

                case "Keyboard":
                    GUILayout.Label("Keyboard");
                    break;

                case "SFC30 Joystick":
                    GUILayout.Label("Snes");
                    break;
            }
            EditorGUILayout.EndHorizontal();
        }
    }
    private void ControllerViewer()
    {
        bool[] button =
            {
            Input.GetKey(KeyCode.JoystickButton0),
            Input.GetKey(KeyCode.JoystickButton1),
            Input.GetKey(KeyCode.JoystickButton2),
            Input.GetKey(KeyCode.JoystickButton3),
            Input.GetKey(KeyCode.JoystickButton4),
            Input.GetKey(KeyCode.JoystickButton5),
            Input.GetKey(KeyCode.JoystickButton6),
            Input.GetKey(KeyCode.JoystickButton7),
            Input.GetKey(KeyCode.JoystickButton8),
            Input.GetKey(KeyCode.JoystickButton9),
            Input.GetKey(KeyCode.JoystickButton10),
            Input.GetKey(KeyCode.JoystickButton11),
            Input.GetKey(KeyCode.JoystickButton12)
            };
        string[] buttonName = new string[8];

        switch(unit.controller)
        {
            

            case "PS4":
                buttonName[0] = "";
                buttonName[1] = "Cross";
                buttonName[2] = "Circle";
                buttonName[3] = "Triangle";
                buttonName[4] = "L1";
                buttonName[5] = "R1";
                buttonName[9] = "Options";
                buttonName[13] = "Share";

                break;

            case "Xbox":
                buttonName[0] = "A";
                buttonName[1] = "B";
                buttonName[2] = "X";
                buttonName[3] = "Y";
                buttonName[4] = "LB";
                buttonName[5] = "RB";
                buttonName[7] = "Start";
                buttonName[6] = "Back";
                break;

            case "Snes":
                buttonName[1] = "A";
                buttonName[4] = "B";
                buttonName[5] = "";
                buttonName[6] = "L";
                buttonName[7] = "R";
                buttonName[10] = "Start";
                buttonName[11] = "Select";
                break;
        }
        if (controllerTest)
        {

            switch (unit.controller)
            {
                default:
                    GUILayout.BeginVertical("In BigTitle");                    
                    for (int i = 0; i < buttonName.Length; i++)
                    {
                        GUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(buttonName[i]);
                        EditorGUILayout.Toggle(button[i]);
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.EndVertical();

                    GUILayout.BeginVertical("In BigTitle");

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Left X");
                    EditorGUILayout.FloatField(Input.GetAxisRaw("Horizontal"));
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Left Y");
                    EditorGUILayout.FloatField(Input.GetAxisRaw("Vertical"));
                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();

                   
                    break;

                case "Keyboard":
                    GUILayout.BeginVertical("In BigTitle");
                    EditorGUILayout.LabelField("No Controller");
                    GUILayout.EndVertical();
                    break;

                case "":
                    GUILayout.BeginVertical("In BigTitle");
                    EditorGUILayout.LabelField("No Controller");
                    GUILayout.EndVertical();
                    break;
            }
            switch(unit.controller)
            {
                default:
                    break;

                case "PS4":
                    GUILayout.BeginVertical("In BigTitle");
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Right X");
                    EditorGUILayout.FloatField(Input.GetAxisRaw("Axis3"));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Right Y");
                    EditorGUILayout.FloatField(Input.GetAxisRaw("Axis6"));
                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                    break;

                case "Xbox":
                    GUILayout.BeginVertical("In BigTitle");
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Right X");
                    EditorGUILayout.FloatField(Input.GetAxisRaw("Axis4"));
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Right Y");
                    EditorGUILayout.FloatField(Input.GetAxisRaw("Axis5"));
                    GUILayout.EndHorizontal();
                    GUILayout.EndVertical();
                    break;
            }
        }
        
            
           
            
            
            
        
    }
}



#region Third Person Stuff
[CustomEditor(typeof(Interactable))]
public class Viewer_Interactable : Editor
{
    protected int debugView;
    protected bool Initialized;
    Interactable unit;
    protected int currentPickerWindow;


    void Init()
    {
        if (!Initialized)
        {
            unit = (Interactable)target;
            Initialized = true;
        }
    }
    #region Debug View Tweaker
    public override void OnInspectorGUI()
    {
        Init();
        debugView = GUILayout.Toolbar(debugView, new[] { "Default", "Debug" });
        switch (debugView)
        {
            case 0:
                Inspector();
                break;


            case 1:
                base.OnInspectorGUI();
                break;
        }
    }
    #endregion

    protected void Inspector()
    {
  
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Object Type: ", GUILayout.Width(80));
        unit.selectedType = EditorGUILayout.Popup(unit.selectedType, new string[] { "Item", "Chest", "Door", "Save Point" });
        EditorGUILayout.EndHorizontal();

        switch (unit.selectedType)
        {
            case 0:
                unit.type = "Item";
                GUILayout.BeginVertical("In BigTitle");
                EditorGUILayout.BeginHorizontal();
                ItemSelection();
                EditorGUILayout.EndHorizontal();
                

                GUILayout.EndVertical();
                break;

               

            case 1:
                unit.type = "Chest";
                GUILayout.BeginVertical("In BigTitle");
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Contains: ", GUILayout.Width(70));
                unit.selectedItem = EditorGUILayout.Popup(unit.selectedItem, new string[] { "Empty", "Item", "NPC" });
                EditorGUILayout.EndHorizontal();
                switch (unit.selectedItem)
                {
                    default:
                        unit.item = null;
                        break;

                    case 1:

                        ItemSelection();
                        break;
                }
                
                GUILayout.EndVertical();
                break;

            case 2:
                unit.type = "Door";
                GUILayout.BeginVertical("In BigTitle");
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Locked: ", GUILayout.Width(70));
                unit.selectedLocked = EditorGUILayout.Popup(unit.selectedLocked, new string[] { "Unlocked", "Locked" });
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                switch(unit.selectedLocked)
                {
                    case 0:
                        unit.locked = false;
                        break;


                    case 1:
                        
                        LockedDoor();
                        break;
                }

                if (unit.selectedLocked == 1)
                {
                    
                    

                }
                    break;
                    
            case 3:
                unit.type = "Save Point";
                //GUILayout.BeginVertical("In BigTitle");
                //GUILayout.EndVertical();
                break;

        }










        #region test


        /*
        switch (unit.selectedType)
        {

            case 0:
                

            case 1:
               


               




            case 2:
                unit.type = "Door";
                GUILayout.BeginVertical("In BigTitle");
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Locked: ", GUILayout.Width(70));
                unit.selectedLocked = EditorGUILayout.Popup(unit.selectedLocked, new string[] { "Unlocked", "Locked" });
                EditorGUILayout.EndHorizontal();
                
                    EditorGUILayout.BeginHorizontal();
                    switch (unit.lockRequirement)
                    {
                        case 0:
                            GUILayout.Label("Items Required: ", GUILayout.Width(110));
                            unit.requiredAmount = EditorGUILayout.IntField(unit.requiredAmount);
                            break;


                        case 1:
                            break;

                    }
                    EditorGUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();
                break;

            case 3:
                
                break;




        }
        switch (unit.selectedLocked)
        {
            case 0:
                
                break;

            case 1:
                unit.locked = true;
                break;
        }
        switch (unit.selectedItem)
        {
            case 0:
                unit.item = null;
                break;

            case 1:
                
                break;

            case 2:
                
                break;
        }
        */
        #endregion
    }

    void ItemSelection()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Item: ", GUILayout.Width(70));
        unit.item = (Item)EditorGUILayout.ObjectField(unit.item, typeof(Item), false);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Obtained: ", GUILayout.Width(70));
        unit.obtained = EditorGUILayout.Toggle(!unit.obtained);
        EditorGUILayout.EndHorizontal();
    }

    void LockedDoor()
    {
        unit.locked = true;
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Lock Requirment: ", GUILayout.Width(110));
        unit.lockRequirement = EditorGUILayout.Popup(unit.lockRequirement, new string[] { "Item Requirement", "Enemies Defeated" });
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        switch (unit.lockRequirement)
        {
            case 0:
                GUILayout.Label("Items Required: ", GUILayout.Width(110));
                unit.requiredAmount = EditorGUILayout.IntField(unit.requiredAmount);
                break;


            case 1:
                break;

        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        
        EditorGUILayout.EndHorizontal();


    }

        void ObjectPicked()
    {
        Item unit = (Item)target;
        string commandName = Event.current.commandName;
        if (commandName == "ObjectSelectorUpdated")
        {
            unit = (Item)EditorGUIUtility.GetObjectPickerObject();
            Repaint();
        }
        
    }

}

#endregion

/*
[CustomEditor(typeof(_Item_Info))]
public class Item_Viewer : Editor
{
    protected GUIStyle titleStyle = new GUIStyle();
    protected GUIStyle m_TitleStyle;
    protected GUIStyle TitleStyle { get { return m_TitleStyle; } }
    protected Texture empty = null;
    protected bool Initialized;

    _Item_Info item;
    void Init()
    {
        if (!Initialized)
        {
            item = (_Item_Info)target;
            m_TitleStyle = titleStyle;
            m_TitleStyle.fontSize = 35;
            m_TitleStyle.padding = new RectOffset(0, 0, 60, 0);
            Initialized = true;
        }
    }
    public override void OnInspectorGUI()
    {
        Init();
        base.OnInspectorGUI();
    }

}
*/