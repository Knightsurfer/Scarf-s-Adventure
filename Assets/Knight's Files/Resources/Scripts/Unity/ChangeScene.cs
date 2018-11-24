using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



//#################################//
//                                                                          //
//            CHANGE SCENE                                      //
//                                                                        //
//                                                                       //
//###############################//
//                                                                     //
//    This script currently contains data for           //           
//    the title screen but it will also control the     //
//    scene changing,                                       //        
//                                                                 //
///////////////////////////////////////////////////////








public class ChangeScene : MonoBehaviour
{

    public int selected;
    public Button[] menuButtons;
    public Gamepad gamepad;

    protected void Start()
    {
        gamepad = FindObjectOfType<Gamepad>();
    }

    protected void Update()
    {

        if (gamepad.controller == "Keyboard")
        {
            Cursor.visible = true;
        }
        if (gamepad.controller != "Keyboard")
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

                if(gamepad.button_Attack)
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
                menuButtons[1].GetComponent<Image>().color = menuButtons[1].colors.normalColor;
                menuButtons[2].GetComponent<Image>().color = menuButtons[2].colors.highlightedColor;
                break;

            case 3:
                selected = 0;
                break;
        }
    }








    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }



    void UpDownHandler()
    {
        
        if (gamepad.isGamepad)
        {
            if (gamepad.direction == "up")
            {
                if (gamepad.d_Up == false)
                {
                    gamepad.d_Up = true;
                    Debug.Log("Test");
                    selected--;
                }
            }
            if (gamepad.direction == "down")
            {
                if (gamepad.d_Down == false)
                {
                    gamepad.d_Down = true;

                    selected++;
                }
            }

        }
    }

    private void MouseMenu(int selection)
    {
        if (gamepad.controller == "Keyboard")
        {
            selected = selection;
        }
    }
}

