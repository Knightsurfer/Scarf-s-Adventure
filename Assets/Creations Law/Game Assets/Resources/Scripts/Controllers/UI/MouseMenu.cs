using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMenu : MonoBehaviour
{
    public MenuChooser menu;
    public int buttonNumber;

    public string parentName;

    public GameObject scriptObject;
    public bool selected;
    
    void Start()
    {
        // Buttons, MenuName, MenuType,Pause Menu
        parentName = transform.parent.transform.parent.transform.parent.transform.parent.name;
        //Debug.Log(parentName + "/" + name);
        menu = GameObject.Find(parentName).GetComponent<MenuChooser>();
    }

    private void Update()
    {
        if (menu)
        {

        }
    }

    public void MouseOn()
    {
        if (menu)
            menu.SendMessage("MouseMenu", buttonNumber);
        selected = true;
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