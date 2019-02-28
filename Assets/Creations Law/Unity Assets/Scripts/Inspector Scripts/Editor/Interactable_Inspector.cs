using UnityEngine;
using UnityEditor;

//||================================||
//||                                                                        ||
//||                     SCRIPT INSPECTORS                    ||
//||                                                                        ||
//||                                                                        ||
//||================================||
//||                                                                        ||
//||    When a script is being viewed as a component, ||
//||    this script comes into action in order to make    ||
//||    it easier to use.                                              ||
//||                                                                        ||
//||================================||

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
                switch (unit.selectedLocked)
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

