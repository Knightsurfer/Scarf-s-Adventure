using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//###################################//
//                                                                              //
//            UI Controls                                                 //
//                                                                            //
//                                                                           //
//#################################//
//                                                                         //
//    A script responsible for the scrolling of menus.//
//                                                                     //
//                                                                    //
/////////////////////////////////////////////////////////

public class UIControls : MonoBehaviour
{

    protected int selectedItem;
    protected Gamepad gamepad;


    protected virtual void Start()
    {
        gamepad = FindObjectOfType<Gamepad>();
    }
    protected virtual void Update()
    {
        if (gamepad.controller == "Keyboard")
        {
            Cursor.visible = true;
        }
        if (gamepad.controller != "Keyboard")
        {
            Cursor.visible = false;
        }
    }

    protected virtual void UpDownHandler(int max, int min)
    {
        if (gamepad.isGamepad)
        {
            if (gamepad.direction == "up")
            {
                if (gamepad.d_Up == false)
                {
                    gamepad.d_Up = true;
                    selectedItem--;
                }
            }
            if (gamepad.direction == "down")
            {
                if (gamepad.d_Down == false)
                {
                    gamepad.d_Down = true;
                    selectedItem++;
                }
            }
        }
    }


    protected virtual void MenuScroller(int min, int max)
    {
        if (gamepad.isGamepad)
        {
            if (selectedItem < min)
            {
                selectedItem = max;
            }
            if (selectedItem > max)
            {
                selectedItem = min;
            }
        }
        SendMessage("HighlightPos");
    }
}



//###################################//
//                                                                              //
//            REPLACEMENT MOUSEOVER                         //
//                                                                            //
//                                                                           //
//#################################//
//                                                                         //
//    A better script for mousing over                      //
//    than the default one that unity provides.         //
//                                                                     //
//                                                                    //
/////////////////////////////////////////////////////////

public class MouseOverButton : MonoBehaviour
{
    public ChangeScene title;
    public PauseMenu pause;
    public Level_Select level;

    public int buttonNumber;

    void Start()
    {
        title = FindObjectOfType<ChangeScene>();
        pause = FindObjectOfType<PauseMenu>();
        level = FindObjectOfType<Level_Select>();
    }

    public void StartSwitcher()
    {
        if (title != null)
            title.SendMessage("MouseMenu", buttonNumber);
    }

    public void StartOff()
    {
        if (title != null)
            title.SendMessage("MouseMenu", buttonNumber);
    }

    public void Switcher()
    {


        if (pause != null)
            pause.SendMessage("MouseMenu", buttonNumber);


        if (level != null)
            level.SendMessage("MouseMenu", buttonNumber);
    }

    public void MenuOff()
    {


        if (pause != null)
            pause.SendMessage("MouseMenu", -100);

        if (level != null)
            level.SendMessage("MouseMenu", -100);
    }

}

public class Misc_Scripts : MouseOverButton
{

}