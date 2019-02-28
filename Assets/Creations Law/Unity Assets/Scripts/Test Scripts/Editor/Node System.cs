using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


public class DataViewer : EditorWindow
{
    bool started;
    Rect window1;
    Rect window2;
    List<Rect> windows = new List<Rect>() { };

    GameManager game;

    int dataPreview;

    [MenuItem("Window/Data Viewer")]
    static void ShowEditor()
    {
        DataViewer editor = EditorWindow.GetWindow<DataViewer>();
        editor.Init();
    }

    public void Init()
    {
        int num = 0;
        float x = 1;
        //float y = 1;
        windows.Clear();
        game = FindObjectOfType<GameManager>();

        foreach (PlayerController player in FindObjectsOfType<PlayerController>())
        {
            if (position.width < 600)
            {

                windows.Add(new Rect(10 + x, 20, 200, 200));
                x += 100 * 2.1f;
            }


            //if (num < 2)
            //{
            //    windows.Add(new Rect(10 + x, 20, 200, 200));
            //    x += 100 * 2.1f;
            //}
            //if (num == 2)
            //{
            //    x = 1;
            //    y += 100 * 2.1f;
            //}
            //if (num > 1)
            //{
            //    windows.Add(new Rect(10 + x, 20 + y, 200, 200));
            //    x += 100 * 2.1f;

            //}





            num++;
        }

        window1 = new Rect(10, 10, 200, 500);
        window2 = new Rect(210, 210, 100, 100);

        started = true;


    }

    void OnGUI()
    {
        
        dataPreview = EditorGUI.Popup(new Rect(0,0,position.width,15), dataPreview, new string[] { "Player Stats", "test2" });

        switch(dataPreview)
        {
            default:
                break;

            case 0:
                //DrawNodeCurve(window1, window2); // Here the curve is drawn under the windows

                BeginWindows();

                int i = game.player.Count -1;
                int windownum = 0;
                foreach(PlayerController player in FindObjectsOfType<PlayerController>())
                {
                    windows[windownum] = GUI.Window(windownum, windows[windownum], DrawNodeWindow, game.player[i].playerInfo.name);   // Updates the Rect's when these are dragged
                    i--;
                    windownum++;
                }

                //window1 = GUI.Window(1, window1, DrawNodeWindow, game.player[0].playerInfo.name);   // Updates the Rect's when these are dragged
                //window2 = GUI.Window(2, window2, DrawNodeWindow, "Window 2");
                EndWindows();
                Repaint();
                break;
            case 1:
                break;
        }
        
    }

    void DrawNodeWindow(int id)
    {
        //GUI.DragWindow();
    }

    void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);
        for (int i = 0; i < 3; i++) // Draw a shadow
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }
}
