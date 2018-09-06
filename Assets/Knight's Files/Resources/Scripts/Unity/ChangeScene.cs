using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ChangeScene : Controller
{

    public int selected;
    public Button[] menuButtons;

    protected void Start()
    {
        ControllerDetect();
        if (controller != "Keyboard")
        {
            Cursor.visible = false;
        }

    }

    protected void Update()
    {
        ControllerCheck();
        if (controller == "Keyboard")
        {
            Cursor.visible = true;
        }
        if (controller != "Keyboard")
        {
            Cursor.visible = false;
        }

        UpDownHandler();

        switch (selected)
        {
            case -1:
                selected = 2;
                break;

            case 0:
                menuButtons[0].GetComponent<Image>().color = menuButtons[0].colors.highlightedColor;
                menuButtons[1].GetComponent<Image>().color = menuButtons[1].colors.normalColor;
                menuButtons[2].GetComponent<Image>().color = menuButtons[2].colors.normalColor;

                if (button_Attack)
                {
                    
                    StartGame();
                }
                break;

            case 1:
                menuButtons[0].GetComponent<Image>().color = menuButtons[0].colors.normalColor;
                menuButtons[1].GetComponent<Image>().color = menuButtons[1].colors.highlightedColor;
                menuButtons[2].GetComponent<Image>().color = menuButtons[2].colors.normalColor;
                break;

            case 2:
                menuButtons[0].GetComponent<Image>().color = menuButtons[0].colors.normalColor;
                menuButtons[1].GetComponent<Image>().color = menuButtons[2].colors.normalColor;
                menuButtons[2].GetComponent<Image>().color = menuButtons[1].colors.highlightedColor;
                break;

            case 3:
                selected = 0;
                break;
        }










        
    }



    public void StartGame ()
    {
        
        SceneManager.LoadScene(1);
    }



    void UpDownHandler()
    {
        if (direction == "up")
        {
            if (d_Up == false)
            {
                d_Up = true;

                selected--;
            }
        }

        if (direction == "down")
        {
            if (d_Down == false)
            {
                d_Down = true;

                selected++;
            }
        }
    }













    }
