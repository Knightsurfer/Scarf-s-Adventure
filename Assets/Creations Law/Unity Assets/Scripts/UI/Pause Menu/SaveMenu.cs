using System;
using System.IO;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




//#################################//
//                                                                          //
//            SAVE MENU SCRIPTS                            //
//                                                                        //
//                                                                       //
//###############################//
//                                                                     //
//    Save menu related scripts are kept here.    //           
//                                                                   //
/////////////////////////////////////////////////////////



    /// <summary>
    /// Startup for the save menu script.
    /// </summary>
    public class SaveMenu : SavePackage.S_Menus {
     new void Start()
     {
        base.Start();
     }
     new void Update()
    {
        MenuCheck();
        MenuControls();
    }


    /// <summary>
    /// All the controls for the menu go here.
    /// </summary>
    protected void MenuControls()
    {
        MenuChoose();       //Which menu to display.
        MenuCancel();       //Back function
    }
}


namespace SavePackage
{
    /// <summary>
    /// The display for each type of menu.
    /// </summary>
    public class S_Menus : S_Checks
{
    /// <summary>
    /// Tells the game which menu to display.
    /// </summary>
    protected void MenuChoose()
    {
        if (menuOpen)
        {
            switch (currentMenu)
            {
                case "Save Menu":
                    SaveScreen();
                    break;
            }
        }
    }

    /// <summary>
    /// Handles the navigation for the main screen.
    /// </summary>
    protected void SaveScreen()
    {
        #region Menu Selection Handler
        UpDownHandler(0, 2);
        if (gamepad.isGamepad)
        {
            if (selectedItem == 4)
            {
                selectedItem = 0;
            }
            selectedMenuItem = menuItems[selectedItem].GetComponentInChildren<Text>().text;
            if (gamepad.button_Attack)
            {
                SelectMenu();
                selectedItem = 0;
                
            }
        }
        else
        {
            if (!mouseSelected)
            {
                selectedItem = 4;
            }
            if (gamepad.button_Attack)
            {
                SelectMenu();
                selectedItem = 0;
                
            }
        }
        #endregion
    }

}

    /// <summary>
    /// Checks for the name of the scene and if the menu is open.
    /// </summary>
    public class S_Checks : S_Navigation
    {
        /// <summary>
        /// //Checks if the save menu is open.
        /// </summary>
        protected void MenuCheck() 
        {
                menuOpen = savePanel.enabled;
                if (menuOpen)
                {
                    switch (SceneManager.GetActiveScene().buildIndex)
                    {
                        case 2:
                            sceneName.text = "|| Test: Gameplay Test";
                            worldName.text = "Test\nLevel";
                            break;

                        case 3:
                            sceneName.text = "|| Dream State: Tutorial";
                            worldName.text = "Dream\nState";
                            break;
                    }
                }
                if (!gamepad.isGamepad)
                {
                    Cursor.visible = savePanel.enabled;
                }
                GamePause();
            
        }

        /// <summary>
        /// If the save menu is open, disable the movement of all objects.
        /// </summary>
        protected void GamePause()
        {
            FindObjectOfType<PauseMenu>().enabled = !menuOpen;
            FindObjectOfType<Command_Controller>().enabled = !menuOpen;

            game.player[0].enabled = !menuOpen;
            foreach (Animator anim in FindObjectsOfType<Animator>())
                {
                    anim.enabled = !menuOpen;
                }
        }
    }

    /// <summary>
    /// Handles menu navigation.
    /// </summary>
    public class S_Navigation : S_Variables
    {
        Canvas[] saves;
        Text[] saveText;

        /// <summary>
        /// Event Handler for when you hit the confirm button.
        /// </summary>
        protected void SelectMenu()
        {
            switch (currentMenu)
            {
                case "Save Menu":
                    switch (selectedItem)
                    {
                        case 0:
                            GameObject.Find("Save Panel").GetComponent<Canvas>().enabled = true;
                            highlighter[0].enabled = false;
                            highlighter[1].enabled = true;
                            currentMenu = "Save Select";

                            saves = GameObject.Find("Save Panel").GetComponentsInChildren<Canvas>();

                            foreach (Canvas c in saves)
                            {
                                saveText = c.GetComponentsInChildren<Text>();

                                foreach (Text t in saveText)
                                {
                                    Debug.Log(t.text);
                                }
                            }

                            for (int i = 0; i == 4; i++)
                            {
                                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Scarfs Adventure\\Data" + "\\Saves\\Save" + i))
                                {
                                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Scarfs Adventure\\Data" + "\\Saves\\Save" + i);
                                }
                            }

                            foreach (string s in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Scarfs Adventure\\Data" + "\\Saves"))
                            {

                                //game.SavedStats[d].Read("Stats", "playername");
                                //d++;
                            }

                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Event Handler for when you hit the back button.
        /// </summary>
        protected void MenuCancel()
        {
            if (menuOpen && gamepad.button_Jump && menuOpen)
            {
                switch (currentMenu)
                {
                    case "Save Menu":
                        savePanel.enabled = false;
                        break;


                    case "Save Select":
                        currentMenu = "Save Menu";
                        GameObject.Find("Save Panel").GetComponent<Canvas>().enabled = false;
                        highlighter[0].enabled = true;
                        highlighter[1].enabled = false;
                        break;

                }
            }
        }

        /// <summary>
        /// If no controller exists, use the mouse to navigate the menus instead.
        /// </summary>
        protected void MouseMenu(int selection) //Mouse Navigation
        {
            if (!gamepad.isGamepad)
            {
                if (selection != 4)
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
        ///  The main controls behind menu scrolling on the pause menu.
        /// </summary>
        /// <param name="min">The minimum value of what the menu can count down to.</param>
        /// <param name="max">The maximum value of what the menu can count down to</param>
        public override void UpDownHandler(int min, int max)
        {
            base.UpDownHandler(min, max);
            highlighter[0].rectTransform.localPosition = highlightPos[selectedItem];
            MenuScroller(min, max);
        }
    }

    /// <summary>
    /// All the important variables are stored here.
    /// </summary>
    public class S_Variables : UIControls
    {
       
        #region Menu Variables //Events that exist in the pause menu.
        /// <summary>
        /// Grabs the canvas this script is running on so you can toggle it on or off.
        /// </summary>
        public Canvas savePanel;

        /// <summary>
        /// The current scene the player is in.
        /// </summary>
        protected Text sceneName;

        /// <summary>
        /// The current world the player is in.
        /// </summary>
        protected Text worldName;

        /// <summary>
        /// The titlecard for the selected menu.
        /// </summary>
        public GameObject menuTitle;

        /// <summary>
        /// The cursor for displaying the selected item.
        /// </summary>
        protected Image[] highlighter = new Image[2];

        /// <summary>
        /// Locations for the highlighter to land.
        /// </summary>
        protected Vector3[] highlightPos ={
        new Vector3(-417.7f, 258f, 0),
        new Vector3(-417.7f, 223.3f, 0),
        new Vector3(-417.7f, 187f, 0),
        new Vector3(-417.7f, 151.7f, 0),
        new Vector3(-427, 401, 0)

};

        /// <summary>
        /// If the menu titlecard needs to move these are the locations.
        /// </summary>
        protected Vector3[] menuTitlePos = new Vector3[] {
        new Vector3(-418, 306, 0),
        new Vector3(-427.3f, 403.7f, 0) };

        /// <summary>
        /// Contains the text of the selected menu item.
        /// </summary>
        protected string selectedMenuItem;

        /// <summary>
        /// If the cursor is hovering over a menu item then this will be true.
        /// </summary>
        protected bool mouseSelected;

        /// <summary>
        /// Tells if the menu is currently open.
        /// </summary>
        public bool menuOpen;

        /// <summary>
        /// The current menu the player is navigating.
        /// </summary>
        protected string currentMenu = "Save Menu";

        /// <summary>
        /// Stores the variables within all of the menu items.
        /// </summary>
        protected GameObject[] menuItems = new GameObject[3];
        #endregion
        #region Game Variables //Events that exist in the game world.
        /// <summary>
        /// For grabbing the all variables stored within the game manager.
        /// </summary>
        protected GameManager game;
        #endregion

        void Awake()
        {
            menuItems[0] = GameObject.Find("Save");
            menuItems[1] = GameObject.Find("World Map");
            menuItems[2] = GameObject.Find("Title Screen");

            highlighter[0] = GameObject.Find("Highlight Save").GetComponent<Image>();
            highlighter[1] = GameObject.Find("Highlight File").GetComponent<Image>();

            menuTitle = GameObject.Find("Save Menu Title");
            worldName = GameObject.Find("World Name").GetComponent<Text>();
            sceneName = GameObject.Find("Level Section").GetComponent<Text>();
            savePanel = GetComponent<Canvas>();
            game = FindObjectOfType<GameManager>();
        }
    }
}