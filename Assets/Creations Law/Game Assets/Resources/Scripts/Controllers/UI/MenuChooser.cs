using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//||=====================================||
//||                                                                                   ||
//||                            MENU SCRIPTS                                 ||
//||                                                                                   ||
//||                                                                                   ||
//||=====================================||
//||                                                                                   ||
//||    The one script that handles all the different menus.      ||           
//||                                                                                   ||
//||=====================================||

public class MenuChooser : SavePackage.SaveMenu
{


    /// <summary>
    /// Checks which menu this is and sets the relevant variables.
    /// </summary>
    private void Start()
    {
        currentMenuType = name;
        VariablesStart();
        UIStart();
        switch (currentMenuType)
        {
            default:
                break;

            case "Save Menu":
                SaveStart();
                break;

            case "Pause Menu":
                PauseStart();
                break;

            case "Command Controller":
                break;

        }
    }
    private void Update()
    {
        UIUpdate();
        {
            switch (currentMenuType)
            {

                default:
                    break;

                case "Save Menu":
                    if (menuActivator)
                    {
                        menuActivator = false;
                    }
                    SaveUpdate();
                    break;

                case "Pause Menu":
                    menuActivator = game.button_Start;
                    PauseUpdate();
                    break;

                case "Command Controller":
                    break;
            }
        }
    }
}

/// <summary>
/// Controls the save menu
/// </summary>
namespace SavePackage
{
    public class SaveMenu : PausePackage.PauseMenu
    {
        /// <summary>
        /// (Currently Empty)
        /// </summary>
        protected void SaveStart()
        {

        }
        protected void SaveUpdate()
        {
            SaveMenus();
        }
        protected void SaveMenus()
        {
            if (menuOpen)
            {
                UpDownHandler();
                MenuScroller(0, menuItemsCounted);

                HighlightPos();

                ConfirmMenu();
                CancelMenu();
            }
        }
    }
}

/// <summary>
/// Controls the pause menu.
/// </summary>
namespace PausePackage
{
    public class PauseMenu : UI.Main
    {
        /// <summary>
        /// (Currently Empty)
        /// </summary>
        protected void PauseStart()
        {

        }
        protected virtual void PauseUpdate()
        {
                    PauseMenus();
        }

        protected void InventoryMenu()
        {
            if (menuOpen)
            {
                HighlightPos();
            }
        }

    protected void PauseMenus()
        {
            if (menuOpen)
            {
                UpDownHandler();  
                MenuScroller(0, menuItemsCounted);
                
                HighlightPos();

                ConfirmMenu();
                CancelMenu();
            }
            if (GameObject.Find("Character Status"))
            {
                GameObject.Find("Character Status").GetComponent<Canvas>().enabled = menuOpen;
            }
        }

        /// <summary>
        /// Updates the stats on the pause screen.
        /// </summary>
        protected void CharacterStatsUpdate()
        {
            if (game.player[0])
            {
                if (GameObject.Find("Stats Panel"))
                {
                    GameObject.Find("Stats Panel").GetComponent<Canvas>().enabled = true;
                    int i = game.player.Length - 1;
                    foreach (PlayerController player in game.player)
                    {
                        GameObject.Find("Character " + (i + 1)).GetComponent<Canvas>().enabled = true;

                        if (player != null)
                        {
                            GameObject.Find("Player" + i + " Name").GetComponent<Text>().text = player.name;
                            GameObject.Find("Player" + i + " Stats").GetComponent<Text>().text = player.level + "\n" + player.health + " / " + player.healthMax + "\n" + player.magic + " / " + player.magicMax;
                        }
                        i--;
                    }
                }
            }
        }
    }
}

/// <summary>
/// Controls the command menu.
/// </summary>
namespace CommandPackage
{

}

/// <summary>
/// Things that are common among the menus.
/// </summary>
namespace UI
{
    /// <summary>
    /// Other functions go here.
    /// </summary>
    public class Main : Controls
    {
        protected void UIStart()
        {
            #region Read Relevant Items.
            MenuItemsCount();
            MenuItemsCountRelevant();
            #endregion
            menuTitle = GameObject.Find("Menu Title");
            menuChooser = FindObjectsOfType<MenuChooser>();
            foreach (Button button in FindObjectsOfType<Button>())
            {
                if (button.name == "Highlight")
                {
                    highlighter[0] = button.GetComponent<Image>();
                }
                if (button.name == "Highlight2")
                {
                    highlighter[1] = button.GetComponent<Image>();
                }
                if (button.name == "Highlight3")
                {
                    highlighter[2] = button.GetComponent<Image>();
                }
            }
        }

        /// <summary>
        /// Frame Updates for all UI.
        /// </summary>
        protected void UIUpdate()
        {
            Pause();
            ActivatorPushed();
            GamepadDetection();
            ResetMainVariables();
            DisableMenuItems("MenuOpen");
        }

        /// <summary>
        /// Game pause determinator.
        /// </summary>
        protected void Pause()
        {
            foreach (Animator anim in FindObjectsOfType<Animator>())
            {
                anim.enabled = !paused;
            }
            foreach (PlayerController player in game.player)
            {
                player.enabled = !paused;
            }

            if (menuChooser[0].menuOpen || menuChooser[1].menuOpen)
            {
                paused = true;

            }
            if (!menuChooser[0].menuOpen && !menuChooser[1].menuOpen)
            {
                paused = false;
            }
            if(!menuOpen && menuActivator)
            {
                MenuItemsCount();
            }
        }

        /// <summary>
        /// Moves the highlighters when required.
        /// </summary>
        protected void HighlightPos()
        {
            if (selectedItem == -100)
            {
                highlighter[selectedHighlighter].transform.position = new Vector3(-603, 635, 0);
            }
            if (selectedItem != -100)
            {
                highlighter[selectedHighlighter].transform.position = menuItemsCurrentContext[selectedItem].transform.position;
            }
        }

        /// <summary>
        /// If the activator of the menu is triggered, this fires.
        /// </summary>
        protected void ActivatorPushed()
        {
            if (menuActivator)
            {
                OpenMenu();
                CloseOtherMenus();
            }
        }

        /// <summary>
        /// Resets the position of the title card and also resets the selected item.
        /// </summary>
        protected void ResetMainVariables()
        {
            foreach (MenuChooser mc in menuChooser)
            {
                if (mc != this)
                {
                    if (mc.menuActivator && menuOpen)
                    {
                        currentMenu = "Main Menu";
                        menuTitle.transform.localPosition = menuTitlePos[0];
                        LoadMenu("MenuConfirm");
                    }
                }
                else
                {
                    if (menuActivator && menuOpen)
                    {
                        currentMenu = "Main Menu";
                        menuTitle.transform.localPosition = menuTitlePos[0];
                        LoadMenu("MenuConfirm");
                    }
                }

            }
            if (menuActivator && menuOpen && selectedItem != 0 && game.isGamepad)
            {
                selectedItem = 0;
            }
            if (menuActivator && menuOpen && selectedItem != 0 && !game.isGamepad)
            {
                selectedItem = -100;
            }

        }

        /// <summary>
        /// If a menu that already uses the menu backdrop is currently in use, closes the other one before opening another.
        /// </summary>
        protected void CloseOtherMenus()
        {
            foreach (MenuChooser menu in menuChooser)
            {
                if (menu.gameObject != gameObject)
                {
                    menu.menuOpen = false;
                }
            }
        }
    }
    /// <summary>
    /// The controls aspect of the userinterface.
    /// </summary>
    public class Controls : Math
    {
        /// <summary>
        /// Handles menu confirmation.
        /// </summary>
        protected virtual void ConfirmMenu()
        {
            
            if (game.button_Attack)
            {
                if (selectedItem >= 0 && selectedItem <= menuItemsCurrentContext.Length - 2 && selectedItem != -100)
                {
                    menuTitle.transform.localPosition = menuTitlePos[1];
                    switch (menuItemsCurrentContext[selectedItem].GetComponentInChildren<Text>().text)
                    {
                        default:
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

        /// <summary>
        /// Handles menu cancellation.
        /// </summary>
        protected virtual void CancelMenu()
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

        /// <summary>
        /// If a menu needs loaded up for any particular reason, fire this.
        /// </summary>
        protected void LoadMenu(string context)
        {
            DisableMenuItems(context);
            MenuItemsCountRelevant();
        }

        /// <summary>
        /// Opens the menu
        /// </summary>
        protected void OpenMenu()
        {
            currentMenu = "Main Menu";
            menuOpen = !menuOpen;
            GameObject.Find("Menu Backdrop").GetComponent<Canvas>().enabled = menuOpen;

            GetComponent<Canvas>().enabled = menuOpen;
        }

        /// <summary>
        /// Handles vertical scrolling.
        /// </summary>
        protected virtual void UpDownHandler()
        {
            if (game.isGamepad)
            {
                if (game.direction == "up")
                {
                    if (game.d_Up == false)
                    {
                        game.d_Up = true;
                        selectedItem--;
                    }
                }
                if (game.direction == "down")
                {
                    if (game.d_Down == false)
                    {
                        game.d_Down = true;
                        selectedItem++;
                    }
                }
            }
        }

        /// <summary>
        /// Keeps the selected item within a certain boundary.
        /// </summary>
        /// <param name="min">The selected item can't go lower than this.</param>
        /// <param name="max">The selected item can't go higher than this.</param>
        protected virtual void MenuScroller(int min, int max)
        {
            if (game.isGamepad)
            {
                if (selectedItem == -100)
                {
                    selectedItem = 0;
                }
                if (selectedItem != -100)
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

        ///<summary>
        ///If a menu item detects mouse hovering, it sends a message to this function.
        /// </summary>
        /// <param name="selection">The feedback that comes from the mouse menu script.</param>
        protected void MouseMenu(int selection)
        {
            if (menuOpen)
            {
                if (!game.isGamepad)
                {
                    if (selection == -100)
                    {
                        selectedItem = selection;
                        mouseSelected = false;
                    }
                    if (selection != -100)
                    {
                        selectedItem = selection;
                        mouseSelected = true;
                    }
                }
            }
        }

        /// <summary>
        /// Prevents the raycaster from other menus from interfering.
        /// </summary>
        /// <param name="context">if "Menu Open" it disbles all menu items apart from the main menu, if "MenuConfirm" it handles the navigation of menus.</param>
        protected void DisableMenuItems(string context)
        {
            switch (context)
            {
                case "MenuOpen":
                    foreach (MouseMenu mi in menuItems)
                    {
                        if (mi)
                        {
                            mi.gameObject.SetActive(menuOpen);
                        }
                    }
                    break;

                case "MenuConfirm":
                    foreach (MouseMenu c in menuItems)
                    {
                        //Buttons//Main Menu//Menu Type
                        if (c && c.transform.parent.transform.parent.transform.parent.name == "MenuType")
                        {
                            if (c.transform.parent.transform.parent.name == currentMenu)
                            {
                                c.transform.parent.transform.parent.gameObject.SetActive(true);
                                c.transform.parent.transform.parent.GetComponent<Canvas>().enabled = true;
                            }
                            else
                            {
                                c.transform.parent.transform.parent.gameObject.SetActive(false);
                                c.transform.parent.transform.parent.GetComponent<Canvas>().enabled = false;
                            }
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Upon a controller connecting or disconnecting, this will fire.
        /// </summary>
        protected void GamepadDetection()
        {
            if (game.isGamepad)
            {
                if (gamepadChanged)
                {
                    selectedItem = 0;
                    gamepadChanged = false;
                }
            }
            else
            {
                if (!gamepadChanged)
                {
                    selectedItem = -100;
                    gamepadChanged = true;
                }
            }
        }
    }
    /// <summary>
    /// Functions that contain more math than most others.
    /// </summary>
    public class Math : Variables
    {
        /// <summary>
        /// Counts how many mousemenu items this menu actually has.
        /// </summary>
        protected void MenuItemsCount()
        {
            int mouseMenuAmount = 0;
            foreach (MouseMenu m in GetComponentsInChildren<MouseMenu>())
            {
                menuItems[mouseMenuAmount] = m;
                mouseMenuAmount++;
            }
        }
        /// <summary>
        /// Counts how many mousemenu items this menu has in the context of the selected menu.
        /// </summary>
        protected void MenuItemsCountRelevant()
        {
            //Clears the menu items context array to make room for the new items.
            Array.Clear(menuItemsCurrentContext, 0, menuItemsCurrentContext.Length);

            int i = 0;
            foreach (MouseMenu m in menuItems)
            {
                if (m && m.transform.parent.transform.parent.name == currentMenu)
                {
                    //Debug.Log(m.name +"/" + m.transform.parent.name);
                    menuItemsCurrentContext[i] = m.gameObject;
                    i++;
                }
            }
            menuItemsCounted = i - 1;
            //Debug.Log(menuItemsCounted);
        }
    }
    /// <summary>
    /// Contains all the common variables in the script.
    /// </summary>
    public class Variables : MonoBehaviour
    {
        /// <summary>
        /// So far it only sets the variable for the game manager.
        /// </summary>
        protected void VariablesStart()
        {
            game = FindObjectOfType<GameManager>();
        }

        /// <summary>
        /// Does nothing yet, but eventually I hope to have this adjust the UI to the set resolution.
        /// </summary>
        protected void ResolutionFixer()
        {
            GameObject.Find("Display Info").GetComponent<Text>().text = "||\n||    " + Screen.currentResolution + "\n||\n||";
            switch (Screen.currentResolution.ToString())
            {
                case "800 x 600 @ 60Hz":
                    break;

                case "1024 x 768 @ 60Hz":
                    break;

                case "1152 x 864 @ 60Hz":
                    break;

                case "1280 x 600 @ 60Hz":
                    break;

                case "1280 x 720 @ 60Hz":
                    break;

                case "1280 x 768 @ 60Hz":
                    break;

                case "1280 x 800 @ 60Hz":
                    break;

                case "1280 x 960 @ 60Hz":
                    break;

                case "1280 x 1024 @ 60Hz":
                    break;

                case "1360 x 768 @ 60Hz":
                    break;

                case "1366 x 768 @ 60Hz":
                    break;

                case "1400 x 1050 @ 60Hz":
                    break;

                case "1440 x 900 @ 60Hz":
                    break;

                case "1600 x 900 @ 60Hz":
                    break;

                case "1680 x 1050 @ 60Hz":
                    break;

                case "1920 x 1080 @ 60Hz":
                    break;


            }
        }

        #region Common Variables
        #region Variables
        #region Bools
        /// <summary>
        /// The trigger for the menu opening.
        /// </summary>
        [HideInInspector] public bool menuActivator;

        /// <summary>
        /// Tells if the menu is currently open.
        /// </summary>
        protected bool menuOpen;

        /// <summary>
        /// If all menus are closed, then this will return false.
        /// </summary>
        public bool paused;

        /// <summary>
        /// If the cursor is hovering over a menu item then this will be true.
        /// </summary>
        protected bool mouseSelected;

        /// <summary>
        /// Used for events that need to happen when switching between gamepad and keyboard.
        /// </summary>
        protected bool gamepadChanged;
        #endregion
        #region Vectors
        /// <summary>
        /// If the menu titlecard needs to move these are the locations.
        /// </summary>
        protected Vector3[] menuTitlePos = new Vector3[] {
        new Vector3(-444, 594, 0),
        new Vector3(-415, 426, 0) };
        #endregion
        #region Strings
        /// <summary>
        /// The type of menu this menu is.
        /// </summary>
        [HideInInspector] public string currentMenuType;

        /// <summary>
        /// The current menu the player is navigating.
        /// </summary>
        protected string currentMenu = "Main Menu";

        /// <summary>
        /// stores which menu's were previously opened so that you can go back.
        /// </summary>
        protected string[] lastMenuEntered = new string[20];
        #endregion
        #region Intergers
        /// <summary>
        /// The index of the selected item within a menu.
        /// </summary>
        public int selectedItem = -100;

        /// <summary>
        /// Which highlighter to use.
        /// </summary>
        public int selectedHighlighter;

        /// <summary>
        /// The amount of menu items counted.
        /// </summary>
        protected int menuItemsCounted;

        /// <summary>
        /// The index of the last menu entered.
        /// </summary>
        protected int lastMove = 0;
        #endregion
        #endregion
        #region Components
        /// <summary>
        /// The highlighter objects that will move when navigating menus.
        /// </summary>
        public Image[] highlighter = new Image[3];

        /// <summary>
        /// Stores information on the relevant menu items.
        /// </summary>
        protected GameObject[] menuItemsCurrentContext = new GameObject[99];

        /// <summary>
        /// The titlecard for the selected menu.
        /// </summary>
        protected GameObject menuTitle;

        /// <summary>
        /// The current world the player is in.
        /// </summary>
        protected Text worldName;

        /// <summary>
        /// The current scene the player is in.
        /// </summary>
        protected Text sceneName;

        /// <summary>
        /// Camera component.
        /// </summary>
        protected Transform cam;
        #endregion
        #region Other Scripts.
        /// <summary>
        /// Stores the variables within all of the menu items.
        /// </summary>
        [HideInInspector] public MouseMenu[] menuItems = new MouseMenu[99];

        /// <summary>
        /// If there are other menus this will store it.
        /// </summary>
        protected MenuChooser[] menuChooser = new MenuChooser[2];

        /// <summary>
        /// Grabs variables from the game manager.
        /// </summary>
        protected GameManager game;
        #endregion
        #endregion
        #region Unused
        /// <summary>
        /// The animator attached to the character.
        /// </summary>
        protected Animator anim;

        /// <summary>
        /// Contains the text of the selected menu item.
        /// </summary>
        protected string selectedMenuItem;
        #endregion
    }
}