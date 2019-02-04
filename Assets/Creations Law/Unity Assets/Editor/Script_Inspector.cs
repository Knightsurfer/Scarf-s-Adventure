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


#region Main Stuff
[CustomEditor(typeof(GameManager))]
public class Viewer_GameManager : Editor
{
    protected int debugView;
    protected int defaultView;
    

    protected int testInt = 1;

    protected bool Initialized;
    Thirdperson_Mode[] people = new Thirdperson_Mode[] { };
    BotReciever[] bots = new BotReciever[] { };
    GameManager unit;



    void Init()
    {
        if (!Initialized)
        {
            unit = (GameManager)target;
            Initialized = true;
        }
    }

    public override void OnInspectorGUI()
    {
        Init();

        BaseStats();

    }

    bool party;
    bool settings;

    protected void Inspector()
    {
        
        defaultView = GUILayout.Toolbar(defaultView, new[] { "Game Settings", "Party", "Inventory" });

        switch (defaultView)
        {
            case 0:
                settings = true;
                party = false;
                break;

            case 1:
                settings = false;
                party = true;
                break;
        }











        SettingsViewer();






        people = FindObjectsOfType<Thirdperson_Mode>();
        bots = FindObjectsOfType<BotReciever>();
        if (people.Length > 0)
        {
            PartyViewer();
        }
    }
    private void PartyViewer()
    {

        if (party)
        {
            foreach (Thirdperson_Mode p in people)
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
            foreach (BotReciever p in bots)
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
        }
    }

    private void SettingsViewer()
    {
        if (settings)
        {
          

            for (int i = 0; i <= Input.GetJoystickNames().Length - 1; i++)
            {
               
            }
            GUILayout.BeginVertical("In BigTitle");
            foreach (string c in Input.GetJoystickNames())
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Controller: ", GUILayout.Width(80));
                switch (c)
                {
                    default:
                        GUILayout.Label(c);
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
            GUILayout.EndVertical();

            GUILayout.BeginVertical("In BigTitle");
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Game Mode: ", GUILayout.Width(80));
            unit.modeSelector = EditorGUILayout.Popup(unit.modeSelector, new string[] { "Third Person", "RTS" });
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Test Mode: ", GUILayout.Width(80));
            switch (testInt)
            {
                case 0:
                    unit.isTest = true;
                    break;
                case 1:
                    unit.isTest = false;
                    break;
            }
            testInt = EditorGUILayout.Popup(testInt, new string[] { "True", "False" });
            EditorGUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
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
        switch (unit.modeSelector)
        {
            case 0:
                unit.mode = "Thirdperson";
                break;

            case 1:
                unit.mode = "RTS";
                break;

        }
    }
}
#endregion


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