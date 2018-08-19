using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;



[CustomEditor(typeof(Unit_Info))]
public class Unit_Stats_Viewer : Editor
{
    protected GUIStyle titleStyle = new GUIStyle();
    protected GUIStyle m_TitleStyle;
    protected GUIStyle TitleStyle { get { return m_TitleStyle; } }
    protected Texture empty = null;



    protected Unit_Info unit;

    protected bool Initialized;
    protected int currentPickerWindow;

   








    void Init()
    {
        if (!Initialized)
        {
            unit = (Unit_Info)target;
            m_TitleStyle = titleStyle;
            m_TitleStyle.fontSize = 35;
            m_TitleStyle.padding = new RectOffset(0, 0, 60, 0);
            Initialized = true;
        }
    }
    void ObjectPicked()
    {
        Unit_Info unit = (Unit_Info)target;
        string commandName = Event.current.commandName;
        if (commandName == "ObjectSelectorUpdated")
        {

            //Texture newtexture = (Texture)EditorGUIUtility.GetObjectPickerObject();


            unit.unitPortrait = (Sprite)EditorGUIUtility.GetObjectPickerObject();//Sprite.Create((Texture2D)newtexture, new Rect(0, 0, newtexture.width, newtexture.height), new Vector2(0.5f, 0.5f));
            Repaint();
        }
    }
    










    protected override void OnHeaderGUI()
    {
        Init();

        #region Portrait Stuff
            GUILayout.BeginHorizontal("In BigTitle");
            if (unit.unitPortrait != null)
            {
                if (GUILayout.Button(unit.unitPortrait.texture, GUILayout.Width(100), GUILayout.Height(100)))
                {
                    currentPickerWindow = EditorGUIUtility.GetControlID(FocusType.Passive);
                    EditorGUIUtility.ShowObjectPicker<Sprite>(null, false, unit.name, currentPickerWindow);
                }
                ObjectPicked();
            }
            if (unit.unitPortrait == null)
            {
                if (GUILayout.Button(empty, GUILayout.Width(100), GUILayout.Height(100)))
                {
                    currentPickerWindow = EditorGUIUtility.GetControlID(FocusType.Passive);
                    EditorGUIUtility.ShowObjectPicker<Sprite>(null, false, unit.name, currentPickerWindow);
                }
                ObjectPicked();
            }
        #endregion
        #region Name Stuff
            GUILayout.Label(unit.name, titleStyle);
            GUILayout.EndHorizontal();
        #endregion
    }
    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("In BigTitle");
        #region HP 
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("HP: "/*+ unit.default_HP*/, GUILayout.Width(40));
            unit.default_HP = EditorGUILayout.IntField(unit.default_HP, GUILayout.Width(35));
          //unit.default_HP = (int)GUILayout.HorizontalScrollbar(unit.default_HP, 1, 0, 1001);
       }
            GUILayout.EndHorizontal();
        #endregion
        #region MP 
        GUILayout.BeginHorizontal();

        GUILayout.Label("MP: "/*+ unit.default_MP*/, GUILayout.Width(40));
        unit.default_MP = EditorGUILayout.IntField(unit.default_MP, GUILayout.Width(35));
        //unit.default_MP = (int)GUILayout.HorizontalScrollbar(unit.default_MP, 1, 0, 1001);
        GUILayout.EndHorizontal();
        #endregion
        #region __ 
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        #endregion

        #region EXP
        GUILayout.BeginHorizontal();

        GUILayout.Label("EXP: "/*+ unit.default_EXP*/, GUILayout.Width(40));
        unit.default_EXP = EditorGUILayout.IntField(unit.default_EXP, GUILayout.Width(35));
        //unit.default_EXP = (int)GUILayout.HorizontalScrollbar(unit.default_EXP, 1, 0, 1001);

        GUILayout.EndHorizontal();
        #endregion
        #region Level
        GUILayout.BeginHorizontal();
        GUILayout.Label("Level: "/*+ unit.startingLevel*/, GUILayout.Width(40));
        unit.startingLevel = EditorGUILayout.IntField(unit.startingLevel, GUILayout.Width(35));
        //unit.startingLevel = EditorGUILayout.IntField(unit.startingLevel);

        GUILayout.EndHorizontal();
        #endregion
        GUILayout.EndVertical();

            GUILayout.BeginVertical("In BigTitle");
        GUILayout.BeginHorizontal();
        GUILayout.Label("Is Prop: ", GUILayout.Width(45));
        unit.isProp = EditorGUILayout.Toggle(unit.isProp);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        GUILayout.EndHorizontal();
            GUILayout.EndVertical();
    }
}
