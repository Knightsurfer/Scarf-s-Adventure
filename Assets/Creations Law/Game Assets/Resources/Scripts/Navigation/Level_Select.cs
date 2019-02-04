using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_Select : UIControls {

    public  Color[] selected = new Color[2];
    public Image[] levelTitles;
    
    
    protected bool mouseSelected;
    public Image highlighter;
    Vector3 highlightPos;


    // Use this for initialization

	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();
        UpDownHandler(0, 4);
        ConfirmSelection();
	}





    public override void UpDownHandler(int min, int max)
    {
        base.UpDownHandler(min, max);
        
        highlighter.rectTransform.localPosition = highlightPos;
        HighlightPos();
        MenuScroller(min, max);
        SendMessage("HighlightPos");
    }

    protected void HighlightPos()
    {
        switch (selectedItem)
        {
            case 0:
                highlightPos = new Vector3(-2, 156, 0);
                break;

            case 1:
                highlightPos = new Vector3(-2, 43, 0);
                break;

            case 2:
                highlightPos = new Vector3(-2, -68, 0);
                break;

            case 3:
                highlightPos = new Vector3(-2, -180, 0);
                break;

            case 4:
                highlightPos = new Vector3(-2, -295, 0);
                break;

            case 5:
                highlightPos = new Vector3(244, 412, 0);
                break;

            case 6:
                highlightPos = new Vector3(244, 344, 0);
                break;

            case -100:
                selectedItem = -1;
                break;
        }
    }
    protected void ConfirmSelection()
    {
        if (gamepad.button_Attack)
        {
            switch (selectedItem)
            {
                default:

                    gamepad.userInterface.SetActive(true);
                    SceneManager.LoadScene(selectedItem + 1);
                    break;

                case 4:
                    Application.Quit();
                    break;

            }

        }
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
        }
        else
        {
            mouseSelected = false;
        }

            UpDownHandler(0, 6);
        
    }





}
