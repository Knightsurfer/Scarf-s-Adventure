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








public class ChangeScene : UIControls
{
    bool mouseSelected;
    public Image[] menuButtons;

    public Color[] buttonColours;

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        UpDownHandler(0, 2);
   
        ConfirmSelection();
    }
    public override void UpDownHandler(int min, int max)
    {
        base.UpDownHandler(min, max);
        ButtonSwitcher();
        MenuScroller(min, max);
    }
    protected void MouseMenu(int selection)
    {
        if (!gamepad.isGamepad)
        {
            if (selection != -100)
            {
                selectedItem = selection;
                mouseSelected = true;
            }
            else
            {
                mouseSelected = false;
            }
        }
    }
    private void ButtonSwitcher()
    {
        switch (selectedItem)
        {
            default:
                menuButtons[0].color = buttonColours[0];
                menuButtons[1].color = buttonColours[0];
                menuButtons[2].color = buttonColours[0];
                break;

            case 0:
                menuButtons[0].GetComponent<Image>().color = buttonColours[1];
                menuButtons[1].GetComponent<Image>().color = buttonColours[0];
                menuButtons[2].GetComponent<Image>().color = buttonColours[0];

                break;

            case 1:
                menuButtons[0].GetComponent<Image>().color = buttonColours[0];
                menuButtons[1].GetComponent<Image>().color = buttonColours[1];
                menuButtons[2].GetComponent<Image>().color = buttonColours[0];
                break;

            case 2:
                menuButtons[0].GetComponent<Image>().color = buttonColours[0];
                menuButtons[1].GetComponent<Image>().color = buttonColours[0];
                menuButtons[2].GetComponent<Image>().color = buttonColours[1];
                break;

        }
    }

    protected void ConfirmSelection()
    {
        if (gamepad.button_Attack)
        {
            switch (selectedItem)
            {
                case 0:
                    gamepad.userInterface.SetActive(true);
                    SceneManager.LoadScene(2);
                    break;

                case 2:
                    Application.Quit();
                    break;

            }
            selectedItem = 0;
        }
    }






}


