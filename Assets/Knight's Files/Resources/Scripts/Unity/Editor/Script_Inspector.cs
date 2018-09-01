using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;




[CustomEditor(typeof(Unit_Controller))]
public class Viewer_UnitController : Editor
{
    
    #region TitleBar Nonsense
    protected GUIStyle titleStyle = new GUIStyle();
    protected GUIStyle m_TitleStyle;
    protected GUIStyle TitleStyle { get { return m_TitleStyle; } }
    protected Texture empty = null;
    #endregion


    protected int debugView;
    protected int unitCharacter;

    protected int currentPickerWindow;



        protected bool Initialized;
        Unit_Controller unit;
        Unit_Info stats;





    void Init()
    {
        if (!Initialized)
        {
            unit = (Unit_Controller)target;
            Initialized = true;
        }
    }



        public override void OnInspectorGUI()
        {
            Init();


        debugView = GUILayout.Toolbar(debugView, new[] { "Default","Debug" });

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

    protected void Inspector()
    {
        //unit.stats = (Unit_Info)EditorGUILayout.ObjectField(unit.stats)




        unit.stats = (Unit_Info)EditorGUILayout.ObjectField(unit.stats, typeof(Unit_Info), false);




        if (!unit.stats.isProp)
        {
            GUILayout.BeginVertical("In BigTitle");

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("HP: ", GUILayout.Width(40));
            unit.HP = EditorGUILayout.IntField(unit.HP);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("MP: ", GUILayout.Width(40));
            unit.MP = EditorGUILayout.IntField(unit.MP);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("EXP: ", GUILayout.Width(40));
            unit.EXP = EditorGUILayout.IntField(unit.EXP);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("LVL: ", GUILayout.Width(40));
            unit.level = EditorGUILayout.IntField(unit.level);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

        }
        



    


    }

    void ObjectPicked()
    {
        Unit_Info unit = (Unit_Info)target;
        string commandName = Event.current.commandName;
        if (commandName == "ObjectSelectorUpdated")
        {

            //Texture newtexture = (Texture)EditorGUIUtility.GetObjectPickerObject();


            unit = (Unit_Info)EditorGUIUtility.GetObjectPickerObject();//Sprite.Create((Texture2D)newtexture, new Rect(0, 0, newtexture.width, newtexture.height), new Vector2(0.5f, 0.5f));
            Repaint();
        }
    }



}



    



    
    
