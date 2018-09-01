using UnityEngine;
using UnityEditor;


/*
[CustomEditor(typeof(Item_Info))]
public class Item_Viewer : Editor
{
    
    protected GUIStyle titleStyle = new GUIStyle();
    protected GUIStyle m_TitleStyle;
    protected GUIStyle TitleStyle { get { return m_TitleStyle; } }
    protected Texture empty = null;
    protected bool Initialized;





    Item_Info item;

    void Init()
    {
        if (!Initialized)
        {
            item = (Item_Info)target;
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