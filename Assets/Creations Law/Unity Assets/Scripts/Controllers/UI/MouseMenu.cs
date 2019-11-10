using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMenu : MonoBehaviour
{
    public MenuChooser menu;
    public int buttonNumber;

    public string parentName;

    public bool selected;
    
    void Start()
    {
        if(transform.parent.name == "Menu Back Panel")
        {
            //           Menu Back Panel, Backpanel, Canvas
            parentName = transform.parent.transform.parent.transform.parent.name;
            menu = GameObject.Find(parentName).GetComponent<MenuChooser>();
        }
        else
        {
            // Buttons, MenuName, MenuType,Pause Menu
            parentName = transform.parent.transform.parent.transform.parent.transform.parent.name;
            menu = GameObject.Find(parentName).GetComponent<MenuChooser>();

            
        }
        
    }


    public void MouseOn()
    {
        if (menu)
        {
            menu.SendMessage("MouseMenu", buttonNumber);
            selected = true;
        }
    }
    public void MouseOff()
    {
        if (menu)
        {
            menu.SendMessage("MouseMenu", -100);
            selected = false;
        }
    }
}