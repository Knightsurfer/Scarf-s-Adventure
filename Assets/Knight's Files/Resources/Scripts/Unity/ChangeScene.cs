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
   

    public Button[] menuButtons;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();





        UpDownHandler(0,3);



        switch (selectedItem)
        {
            case -1:
                selectedItem = 2;
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
                selectedItem = 0;
                break;
        }
    }








    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }



   

    private void MouseMenu(int selection)
    {
        if (gamepad.controller == "Keyboard")
        {
            selectedItem = selection;
        }
    }
}

