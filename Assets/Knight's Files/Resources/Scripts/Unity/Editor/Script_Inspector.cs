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
    protected bool Initialized;
    ThirdPerson_Mode[] people = new ThirdPerson_Mode[] { };
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
        people = FindObjectsOfType<ThirdPerson_Mode>();


        SettingsViewer();
        if (people.Length > 0)
        {
            PartyViewer();
        }
    }
    private void PartyViewer()
    {
        if (GUILayout.Button("Party"))
        {
            party = !party;
        }
        if (party)
        {

            foreach (ThirdPerson_Mode p in people)
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
        if (GUILayout.Button("Game Settings"))
        {
            settings = !settings;
        }
        if (settings)
        {
            GUILayout.BeginVertical("In BigTitle");

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Controller: ", GUILayout.Width(80));
            if (FindObjectOfType<Gamepad>().controller != "")
            {
                GUILayout.Label(FindObjectOfType<Gamepad>().controller);
            }
            if (FindObjectOfType<Gamepad>().controller == "")
            {
                GUILayout.Label("None");
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Game Mode: ", GUILayout.Width(80));
            unit.modeSelector = EditorGUILayout.Popup(unit.modeSelector, new string[] { "Third Person", "RTS" });
            EditorGUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
    }
    private void BaseStats()
    {
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
        unit.selectedType = EditorGUILayout.Popup(unit.selectedType, new string[] { "Item", "Chest", "Door" });
        EditorGUILayout.EndHorizontal();

        switch (unit.selectedType)
        {
            case 0:
                unit.type = "Item";
                GUILayout.BeginVertical("In BigTitle");
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Item: ", GUILayout.Width(70));
                unit.item = (Item)EditorGUILayout.ObjectField(unit.item, typeof(Item), false);
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


                switch(unit.selectedItem)
                {
                    case 1:
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label("Contains: ", GUILayout.Width(70));
                        unit.item = (Item)EditorGUILayout.ObjectField(unit.item, typeof(Item), false);
                        EditorGUILayout.EndHorizontal();
                        break;
                }


                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Obtained: ", GUILayout.Width(70));
                unit.obtained = EditorGUILayout.Toggle(unit.obtained);
                EditorGUILayout.EndHorizontal();
                GUILayout.EndVertical();
                break;

            case 2:
                unit.type = "Door";
                GUILayout.BeginVertical("In BigTitle");
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Locked: ", GUILayout.Width(70));
                unit.selectedLocked = EditorGUILayout.Popup(unit.selectedLocked, new string[] { "Unlocked", "Locked" });
                EditorGUILayout.EndHorizontal();
                if (unit.selectedLocked == 1)
                {
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
                }
                GUILayout.EndVertical();
                break;
        }
        switch (unit.selectedLocked)
        {
            case 0:
                unit.locked = false;
                break;

            case 1:
                unit.locked = true;
                break;
        }
        switch (unit.selectedItem)
        {
            case 0:
                
                break;

            case 1:
                
                break;

            case 2:
                
                break;
        }
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