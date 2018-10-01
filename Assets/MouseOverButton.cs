using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MouseOverButton : MonoBehaviour
{
    public ChangeScene title;
    public PauseMenu pause;
    public int buttonNumber;

	void Start ()
    {
        title = FindObjectOfType<ChangeScene>();
        pause = FindObjectOfType<PauseMenu>();
	}

    public void StartSwitcher()
    {
        title.SendMessage("MouseMenu", buttonNumber);
    }

    public void StartOff()
    {
        title.SendMessage("MouseMenu", buttonNumber);
    }

    public void Switcher()
    {
        pause.SendMessage("MouseMenu",buttonNumber);
    }

    public void MenuOff()
    {
        pause.SendMessage("MouseMenu", -100);
    }

}
