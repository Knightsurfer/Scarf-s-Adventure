using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscapeMenuActivator : MonoBehaviour {


    public GameObject escape;
    [HideInInspector]
    public bool EscapeMenuOpen;

   
    

    private void Start()
    {
        UnityEngine.Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        OpenMenu();
        CursorCheck();

    }


    void OpenMenu()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            escape.gameObject.SetActive(!escape.activeSelf);
        }


    }

    void CursorCheck()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            switch (EscapeMenuOpen)
            {
                case true:
                    EscapeMenuOpen = false;
                    UnityEngine.Cursor.visible = false;
                    break;

                case false:
                    EscapeMenuOpen = true;
                    UnityEngine.Cursor.visible = true;
                    System.Windows.Forms.Cursor.Position = new System.Drawing.Point(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2);
                    break;
            }
        }
    }


    


    public void Escape()
    {
        UnityEngine.Application.Quit();
    }
    public void RestartLevel()
    {
        if (!UnityEngine.Application.isEditor)
        {
            Scene loadedLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedLevel.buildIndex);
        }
    }
}

