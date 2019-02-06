using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



//||############################||
//||                                                               ||
//||                  CHANGE SCENE                      ||
//||                                                               ||
//||                                                               ||
//||############################||
//||                                                               ||
//||    This script currently contains data for      ||           
//||    the title screen but it will also control the ||
//||    scene changing,                                    ||        
//||                                                               ||
//||============================||








public class ChangeScene : SceneChanger.Functions
{

    protected override void Update()
    {
        base.Update();
        UpDownHandler(0, 2);
        ConfirmSelection();
    }    
}

namespace SceneChanger
{
    /// <summary>
    /// Dictates where to go when the cancel or confirm button are pressed.
    /// </summary>
    public class Functions : Navigation
    {
        /// <summary>
        /// The confirm button.
        /// </summary>
        protected void ConfirmSelection()
        {
            if (game.button_Attack)
            {
                switch (selectedItem)
                {
                    case 0:
                        //gamepad.userInterface.SetActive(true);
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

    /// <summary>
    /// Button selection.
    /// </summary>
    public class Navigation : Variables
    {
        /// <summary>
        /// if a gamepad isn't present the mouse controls the menu.
        /// </summary>
        /// <param name="selection">When the mouse cursor hovers over a button, this tells which button that is.</param>
        protected void MouseMenu(int selection)
        {
            if (!game.isGamepad)
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

        /// <summary>
        /// Handles the up and down movement of the selection cursor.
        /// </summary>
        /// <param name="min">When the selection is over the stated index, it reverts to this index.</param>
        /// <param name="max">When the selection is under the stated index, it reverts to this index.</param>
        public override void UpDownHandler(int min, int max)
        {
            base.UpDownHandler(min, max);
            ButtonSwitcher();
            MenuScroller(min, max);
        }

        /// <summary>
        /// When the selected index changes, this changes the buttons to a certain colour to create the illusion of it being highlighted.
        /// </summary>
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
    }

    /// <summary>
    /// The starting point for variables.
    /// </summary>
    public class Variables : UIControls
    {
       private void Awake()
       {
            base.Start();
       }

       /// <summary>
        /// Determines if a button is being selected with the mouse cursor.
        /// </summary>
       protected bool mouseSelected;

       /// <summary>
       /// Dictates which buttons are to be buttons for the context of this script.
       /// </summary>
       public Image[] menuButtons;

        /// <summary>
        /// The colours to select when a button is highlighted and unhighlighted
        /// </summary>
       public Color[] buttonColours;
    }
}