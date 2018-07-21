
//using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;


using System.Collections;

using UnityEngine;
using UnityEditor;

public class ScriptWriter : EditorWindow
{

    Vector2 scriptScroll;

    string path;
    string voidName;
    string voidContents;
    string variables;
    string variableName = "";
    string variableType;
    string voidResult;
    protected string scriptName;
    protected string scriptFolder;
    protected string scriptPath;
    protected string scriptBody = null;

    protected Texture script = null;
    protected StreamWriter writer;
    protected StreamReader reader;
    protected TextAsset asset;
  

    static ScriptWriter window;
    [UnityEditor.MenuItem("Custom Window/Script Writer")]
    static void ShowWindow()
    {
        window = GetWindow<ScriptWriter>("ScriptWriter");
        window.Initialize();
    }







    void Initialize()
    {
        
    }



    private void OnGUI()

    {

        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Script Name: ", GUILayout.Width(80));
        scriptName = GUILayout.TextField(scriptName, GUILayout.Width(80));
        scriptPath = "Assets/Script Writer/" + scriptName + ".cs";


        if (GUILayout.Button("Save Script"))
        {
            if (scriptName != "")
            {

                writer = new StreamWriter(File.Create(scriptPath));
                DefaultScript(scriptName, scriptPath, "write");

            }
        }

        GUILayout.EndHorizontal();


 


       

        DefaultScript(scriptName, scriptPath, "read");
        


       

    }

    public void PlayGame()
    {
        string filePath = System.IO.Path.Combine(UnityEngine.Application.streamingAssetsPath, "C:/Program Files (x86)/Microsoft Visual Studio/2017/Community/Common7/IDE/devenv.exe");
        System.Diagnostics.Process.Start(scriptPath);
    }



    void DefaultScript(string scriptName, string scriptPath, string action)
    {


        string scriptBody =
        "using UnityEngine;                               \n" +
        "public class " + scriptName + " : MonoBehaviour  \n" +
        "{                                                \n" +
        "                                                 \n" +
          variables +                                    "\n" +
        "       void Start()                              \n" +
        "       {                                         \n" +
        "                                                 \n" +
        "       }                                         \n" +
        "                                                 \n" +
        "                                                 \n" +
        "                                                 \n" +
        "       void Update()                             \n" +
        "       {                                         \n" +
        "                                                 \n" +
        "       }                                         \n" +
        "                                                 \n" +
        "                                                 \n" +
        "                                                 \n" +
          voidResult +                                 "\n" +
        "}                                                  ";







        
        if (action == "read")
        {
            scriptScroll = GUILayout.BeginScrollView(scriptScroll, GUILayout.Width(460), GUILayout.Height(350));
            scriptBody = GUILayout.TextArea(scriptBody);
            GUILayout.EndScrollView();
        }





        else if (action == "write")
        {
            WriteScript(scriptBody, scriptPath);
        }

    
        GUILayout.BeginHorizontal();
        variableType = GUILayout.TextField(variableType, GUILayout.Width(80));
        variableName = GUILayout.TextField(variableName, GUILayout.Width(80));
        
        if (GUILayout.Button("Add Variable"))
        {
            if (variableName != "")
            {
                variables += "  " + variableType + " " + variableName + ";\n";
            }

        }
        if (GUILayout.Button("Clear Variables"))
        {
            variables = "";
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Void Name: ", GUILayout.Width(80));
        voidName = GUILayout.TextField(voidName, GUILayout.Width(80));
        GUILayout.EndHorizontal();
        voidContents = GUILayout.TextArea(voidContents, GUILayout.Height(150));
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Void"))
        {
            if (voidName != "")
            {
                voidResult += "   " + "   void " + voidName + "()\n      {\n\n         " + voidContents + "\n\n       }\n\n";
            }

        }
        if (GUILayout.Button("Clear Voids"))
        {
            voidResult = "";
        }
        GUILayout.EndHorizontal();




    }

    void WriteScript(string scriptBody, string scriptPath)
    {

        
        writer.Write(scriptBody);
        writer.Dispose();






        scriptBody = GUILayout.TextArea(scriptBody);

        //reader = new StreamReader(scriptPath);
        //string read = reader.ReadToEnd();
        //GUILayout.TextArea(read);






    }


}
   
    































