﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCustom : MenuChooser
{
    protected override void PauseUpdate()
    {
        if (menuActivator && !menuOpen)
        {
            CharacterStatsUpdate();
        }
        base.PauseUpdate();
    }

    protected override void CancelMenu()
    {
        if (game.button_Jump)
        {
            if (game.isGamepad)
            {
                selectedItem = 0;
            }
            else
            {
                selectedItem = -100;
            }
            switch (currentMenu)
            {
                case "Main Menu":
                    menuOpen = false;
                    GameObject.Find("Menu Backdrop").GetComponent<Canvas>().enabled = menuOpen;
                    break;

                case "Save Menu":
                    lastMove--;
                    selectedHighlighter = 0;
                    highlighter[1].transform.position = new Vector3(-603, 635, 0);
                    currentMenu = lastMenuEntered[lastMove];
                    DisableMenuItems("MenuConfirm");
                    MenuItemsCountRelevant();
                    break;

                case "Stock Menu":
                    lastMove--;
                    selectedHighlighter = 0;
                    highlighter[2].transform.position = new Vector3(-603, 635, 0);
                    currentMenu = lastMenuEntered[lastMove];
                    DisableMenuItems("MenuConfirm");
                    MenuItemsCountRelevant();
                    break;



                default:
                    lastMove--;
                    currentMenu = lastMenuEntered[lastMove];
                    DisableMenuItems("MenuConfirm");
                    MenuItemsCountRelevant();
                    break;

            }
            if (currentMenu != "Main Menu")
            {
                menuTitle.transform.localPosition = menuTitlePos[1];
            }
            else
            {
                menuTitle.transform.localPosition = menuTitlePos[0];
            }
            menuTitle.GetComponentInChildren<Text>().text = currentMenu;
        }
    }
    bool menuChanges;
    protected override void ConfirmMenu()
    {
        if (game.button_Attack)
        {
            
            if (!menuChanges)
            {


                if (selectedItem >= 0 && selectedItem <= menuItemsCurrentContext.Length - 2 && selectedItem != -100)
                {
                    menuTitle.transform.localPosition = menuTitlePos[1];
                    switch (menuItemsCurrentContext[selectedItem].GetComponentInChildren<Text>().text)
                    {
                        default:
                            Debug.Log("Test");
                            lastMenuEntered[lastMove] = currentMenu;
                            lastMove++;

                            currentMenu = menuItemsCurrentContext[selectedItem].GetComponentInChildren<Text>().text + " Menu";
                            LoadMenu("MenuConfirm");
                            break;

                        case "Save":
                            highlighter[0].transform.position = new Vector3(-720.4601f, 531.5f, 0);
                            selectedHighlighter = 1;
                            lastMenuEntered[lastMove] = currentMenu;
                            lastMove++;

                            currentMenu = menuItemsCurrentContext[selectedItem].GetComponentInChildren<Text>().text + " Menu";
                            LoadMenu("MenuConfirm");
                            break;

                        case "Stock":
                            Debug.Log("Stock");
                            highlighter[0].transform.position = new Vector3(-720.4601f, 531.5f, 0);
                            selectedHighlighter = 2;
                            lastMenuEntered[lastMove] = currentMenu;
                            lastMove++;

                            currentMenu = menuItemsCurrentContext[selectedItem].GetComponentInChildren<Text>().text + " Menu";
                            LoadMenu("MenuConfirm");
                            break;
                    }

                    menuTitle.GetComponentInChildren<Text>().text = currentMenu;
                    if (game.isGamepad)
                    {
                        selectedItem = 0;
                    }
                    else
                    {
                        selectedItem = -100;
                    }

                }
            }
        }
    }
}
