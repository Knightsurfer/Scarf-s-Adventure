using UnityEngine;

//###################################//
//                                                                              //
//            COMMAND MENU                                         //
//                                                                            //
//                                                                           //
//#################################//
//                                                                          //
//    This handles the Kingdom Hearts style              //
//    command menu for executing actions.             //
//                                                                      //
//                                                                     //
///////////////////////////////////////////////////////////


public class Command_Controller : UIControls
{

    protected GameObject[] menuItems = new GameObject[6];






    protected override void Start()
    {
        base.Start();
        AssignMenu();
    }


    protected new void Update()
    {
        if (menuItems.Length > 0)
        {
            Menu();
        }
    }

    void AssignMenu()
    {
        menuItems[0] = GameObject.Find("Command Item 1");
        menuItems[1] = GameObject.Find("Command Item 2");
        menuItems[2] = GameObject.Find("Command Item 3");
        menuItems[3] = GameObject.Find("Command Item 4");
        menuItems[4] = GameObject.Find("SpikeUp");
        menuItems[5] = GameObject.Find("SpikeDown");
    }
    protected void Menu()
    {
        if (!GameObject.Find("Pause Menu").GetComponent<Canvas>().enabled)
        {
            UpDownHandler(0, 4);

            switch (selectedItem)
            {

                case -1:
                    selectedItem = 3;
                    break;

                case 0:
                    menuItems[4].transform.localPosition = new Vector3(210, 133, 0);
                    menuItems[5].transform.localPosition = new Vector3(-180f, 71f, 0);

                    menuItems[0].transform.localPosition = new Vector3(30, 67.5f, 0);
                    menuItems[1].transform.localPosition = new Vector3(0, 0, 0);
                    menuItems[2].transform.localPosition = new Vector3(0, -69, 0);
                    menuItems[3].transform.localPosition = new Vector3(0, -136, 0);


                    break;

                case 1:
                    menuItems[4].transform.localPosition = new Vector3(211, 66, 0);
                    menuItems[5].transform.localPosition = new Vector3(-179f, 1f, 0);

                    menuItems[0].transform.localPosition = new Vector3(0, 67.5f, 0);
                    menuItems[1].transform.localPosition = new Vector3(30, 0, 0);
                    menuItems[2].transform.localPosition = new Vector3(0, -69, 0);
                    menuItems[3].transform.localPosition = new Vector3(0, -136, 0);


                    break;

                case 2:
                    menuItems[4].transform.localPosition = new Vector3(211, -2.7f, 0);
                    menuItems[5].transform.localPosition = new Vector3(-179f, -66.5f, 0);

                    menuItems[0].transform.localPosition = new Vector3(0, 67.5f, 0);
                    menuItems[1].transform.localPosition = new Vector3(0, 0, 0);
                    menuItems[2].transform.localPosition = new Vector3(30, -69, 0);
                    menuItems[3].transform.localPosition = new Vector3(0, -136, 0);
                    break;

                case 3:
                    menuItems[4].transform.localPosition = new Vector3(209, -70.8f, 0);
                    menuItems[5].transform.localPosition = new Vector3(-181, -135f, 0);

                    menuItems[0].transform.localPosition = new Vector3(0, 67.5f, 0);
                    menuItems[1].transform.localPosition = new Vector3(0, 0, 0);
                    menuItems[2].transform.localPosition = new Vector3(0, -69, 0);
                    menuItems[3].transform.localPosition = new Vector3(30, -136, 0);
                    break;

                case 4:
                    selectedItem = 0;
                    break;
            }
        }
    }
}

public class UIControls : MonoBehaviour
{

    public int selectedItem;
    public GameManager gamepad;


    protected virtual void Start()
    {
        gamepad = FindObjectOfType<GameManager>();
        if (gamepad.isGamepad) //Determines if a gamepad is plugged in
        {
            selectedItem = 0;
        }
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

    /// <summary>
    ///  The main controls behind menu scrolling on the pause menu.
    /// </summary>
    /// <param name="min">The minimum value of what the menu can count down to.</param>
    /// <param name="max">The maximum value of what the menu can count down to.</param>
    public virtual void UpDownHandler(int max, int min)
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
    /// <summary>
    /// If the value goes over or under a certain threshold, select the respective max or min value.
    /// </summary>
    /// <param name="min">The minimum value of what the menu can count down to.</param>
    /// <param name="max">The maximum value of what the menu can count down to.</param>
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
    }
}
