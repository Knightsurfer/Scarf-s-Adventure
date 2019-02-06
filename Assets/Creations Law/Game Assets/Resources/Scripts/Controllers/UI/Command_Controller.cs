
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


public class Command_Controller : CommandMenu.Navigation
{
    protected new void Update()
    {
        Menu();
    }
}
namespace CommandMenu
{
    public class Navigation : Variables
    {
        protected void Menu()
        {
            if (menuItems.Length > 0)
            {
                if (!GameObject.Find("Pause Menu").GetComponent<Canvas>().enabled)
                {
                    if (game.player[0].focus)
                    {
                        switch (game.player[0].focus.type)
                        {

                            default:
                                menuItems[6].GetComponent<Canvas>().enabled = true;
                                break;

                            case "Item":
                                break;
                        }
                    }
                }
                if (!game.player[0].focus)
                {
                    menuItems[6].GetComponent<Canvas>().enabled = false;
                }
                UpDownHandler(0, 3);
                for (int item = 0; item < 4; item++)
                {
                    menuItems[item].transform.localPosition = unselectedPos[item];
                }
                menuItems[selectedItem].transform.localPosition = selectedPos[selectedItem];
                switch (selectedItem)
                {
                    case 0:

                        menuItems[4].transform.localPosition = new Vector3(210, 133, 0);
                        menuItems[5].transform.localPosition = new Vector3(-180f, 71f, 0);
                        break;

                    case 1:

                        menuItems[4].transform.localPosition = new Vector3(211, 66, 0);
                        menuItems[5].transform.localPosition = new Vector3(-179f, 1f, 0);
                        break;

                    case 2:

                        menuItems[4].transform.localPosition = new Vector3(211, -2.7f, 0);
                        menuItems[5].transform.localPosition = new Vector3(-179f, -66.5f, 0);
                        break;

                    case 3:
                        menuItems[4].transform.localPosition = new Vector3(209, -70.8f, 0);
                        menuItems[5].transform.localPosition = new Vector3(-181, -135f, 0);

                        break;
                }
            }
        }
    }
    public class Variables : UIControls
    {
        /// <summary>
        /// Menu Items
        /// </summary>
        [HideInInspector] public GameObject[] menuItems = new GameObject[7];

        /// <summary>
        /// The position to take when the menu item in question is selected.
        /// </summary>
        protected Vector3[] selectedPos = new Vector3[]
        {
              new Vector3(30, 67.5f, 0),
              new Vector3(30, 0, 0),
              new Vector3(30, -69, 0),
              new Vector3(30, -136, 0)
        };

        /// <summary>
        /// The position to take when the menu item in question is not selected.
        /// </summary>
        protected Vector3[] unselectedPos = new Vector3[]
        {
            new Vector3(0, 67.5f, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, -69, 0),
            new Vector3(0, -136, 0),

        };





        private void Awake()
        {
            base.Start();
            #region Assign MenuItems
            for (int item = 0; item < 4; item++)
            {
                menuItems[item] = GameObject.Find("Command Item " + (item + 1));
            }

            menuItems[4] = GameObject.Find("SpikeUp");
            menuItems[5] = GameObject.Find("SpikeDown");
            menuItems[6] = GameObject.Find("Action Button");
            #endregion
        }
    }
}