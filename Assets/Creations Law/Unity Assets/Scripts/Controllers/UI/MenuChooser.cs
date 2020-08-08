using System;
using System.Collections.Generic;
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
    public void Start()
    {
        switch (name)
        {
            case "Title Menu":
                TitleStart();
                break;
        }
        switch (name)
        {
            default:
                usesBackdrop = true;
                break;

            case "Command Menu":
                CommandStart();
                break;
        }


        if (usesBackdrop)
        {
            VariablesStart();
            UIStart();
           
        }
    }

    public void Update()
    {

        if (usesBackdrop)
        {
            UIUpdate();
            if (menuOpen)
            {
                switch (navigationMode)
                {
                    case "Vertical":
                        VerticalHandler();
                        break;

                    case "Grid":
                        GridHandler();
                        break;
                }
                MenuScroller(0, menuItemsCounted);
                HighlightPos();
                ConfirmMenu();
                CancelMenu();
            }
            switch (name)
            {
                case "Save Menu":
                    if (game.openMenus[0])
                    {
                        return;
                    }
                    if (menuActivator) menuActivator = false;
                    SaveUpdate();
                    break;

                case "Pause Menu":
                    if (game.openMenus[1])
                    {
                        return;
                    }
                    menuActivator = Input.GetKeyDown(game.button_Start);
                    PauseUpdate();
                    break;

                case "Title Menu":
                    TitleUpdate();
                    break;
            }
        }
        else
        {
            switch (name)
            {
                default:
                    print("Undefined Menu Type");
                    break;

                case "Command Menu":
                    CommandUpdate();
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
            game.openMenus[1] = menuOpen;
            SaveMenus();
        }
        protected void SaveMenus()
        {

        }
    }
}

/// <summary>
/// Controls the pause menu.
/// </summary>
namespace PausePackage
{
    public class PauseMenu : CommandPackage.CommandMenu
    {
       
        protected void PauseUpdate()
        {
            game.openMenus[0] = menuOpen;

            if (menuActivator && !menuOpen)
            {
                CharacterStatsUpdate();
            }
            else if (menuActivator && menuOpen)
            {
                selectedHighlighter = 0;
                highlighter[1].transform.position = new Vector3(-603, 635, 0);
                highlighter[2].transform.position = new Vector3(-603, 635, 0);
            }
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
                    int i = game.player.Count;
                    foreach (PlayerController player in game.player)
                    {
                        GameObject.Find("Character " + (i)).GetComponent<Canvas>().enabled = true;

                        if (player != null)
                        {
                            GameObject.Find("Player" + (i - 1) + " Name").GetComponent<Text>().text = player.playerInfo.name;
                            GameObject.Find("Player" + (i - 1) + " Stats").GetComponent<Text>().text = player.level + "\n" + player.health + " / " + player.healthMax + "\n" + player.magic + " / " + player.magicMax;
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
    public class CommandMenu : TitlePackage.TitleMenu
    {
        /// <summary>
        /// (Currently Empty)
        /// </summary>
        protected void CommandStart()
        {
            game = FindObjectOfType<GameManager>();
            MenuItemsCount();
            menuItemsCounted = menuItems.Count - 1;
        }
        protected virtual void CommandUpdate()
        {
           if (game.paused)
           {
                return;
           }
               
            VerticalHandler();

            MenuScroller(0, menuItemsCounted);
            if (menuItems.Count > 0)
            {
                Menu();
            }

        }

        protected void Menu()
        {
            switch (selectedItem)
            {

                case 0:
                    //menuItems[4].transform.localPosition = new Vector3(210, 133, 0);
                    //menuItems[5].transform.localPosition = new Vector3(-180f, 71f, 0);

                    menuItems[0].transform.localPosition = new Vector3(30, 67.5f, 0);
                    menuItems[1].transform.localPosition = new Vector3(0, 0, 0);
                    menuItems[2].transform.localPosition = new Vector3(0, -69, 0);
                    menuItems[3].transform.localPosition = new Vector3(0, -136, 0);
                    break;

                case 1:
                    //menuItems[4].transform.localPosition = new Vector3(211, 66, 0);
                    //menuItems[5].transform.localPosition = new Vector3(-179f, 1f, 0);

                    menuItems[0].transform.localPosition = new Vector3(0, 67.5f, 0);
                    menuItems[1].transform.localPosition = new Vector3(30, 0, 0);
                    menuItems[2].transform.localPosition = new Vector3(0, -69, 0);
                    menuItems[3].transform.localPosition = new Vector3(0, -136, 0);
                    break;

                case 2:
                    //menuItems[4].transform.localPosition = new Vector3(211, -2.7f, 0);
                    //menuItems[5].transform.localPosition = new Vector3(-179f, -66.5f, 0);

                    menuItems[0].transform.localPosition = new Vector3(0, 67.5f, 0);
                    menuItems[1].transform.localPosition = new Vector3(0, 0, 0);
                    menuItems[2].transform.localPosition = new Vector3(30, -69, 0);
                    menuItems[3].transform.localPosition = new Vector3(0, -136, 0);
                    break;

                case 3:
                    //menuItems[4].transform.localPosition = new Vector3(209, -70.8f, 0);
                    //menuItems[5].transform.localPosition = new Vector3(-181, -135f, 0);

                    menuItems[0].transform.localPosition = new Vector3(0, 67.5f, 0);
                    menuItems[1].transform.localPosition = new Vector3(0, 0, 0);
                    menuItems[2].transform.localPosition = new Vector3(0, -69, 0);
                    menuItems[3].transform.localPosition = new Vector3(30, -136, 0);
                    break;

            }
        }
    }
}

namespace TitlePackage
{
    public class TitleMenu : UI.Main
    {
        protected void TitleStart()
        {
            game = FindObjectOfType<GameManager>();
            menuOpen = true;
            player = GameObject.Find("Prefab Spawner");
            modLastPages[1] = game.characters.Length;
            modelPrefab = game.characters[0];
            
        }
        public bool continueDeleted;
        protected void TitleUpdate()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/" + Application.productName + "/Data/" + "Saves"))
            {
                Destroy(GameObject.Find("Continue"));
                continueDeleted = true;
                saveExists = false;
            }
            else
            {
                saveExists = true;
            }
            switch (currentMenu)
            {
                case "Mods Menu":
                    if (game.isGamepad)
                    {
                        player.transform.Rotate(0, -Input.GetAxis("Axis4") * 5, 0);
                        ModIndexer();
                    }
                    else
                    {
                        ModIndexer();
                    }
                    break;
            }

        }


        public void DragPlayer()
        {
            if (!game.isGamepad)
            {
                player.transform.Rotate(0, -Input.GetAxis("Mouse X") * 5, 0);
            }
        }

        protected void ModIndexer()
        {
            if (game.characters[modPages[1]])
            {
                GameObject.Find("Character").GetComponent<Text>().text = game.characters[modPages[1]].name;
            }
            if (selectedItem != -100)
            {

                if (Input.GetMouseButtonDown(0))
                {
                    
                        foreach (SkinnedMeshRenderer skin in FindObjectsOfType<SkinnedMeshRenderer>())
                        {
                            Destroy(skin.transform.parent.gameObject);
                        }
                        foreach (MeshRenderer mesh in FindObjectsOfType<MeshRenderer>())
                        {
                            Destroy(mesh.transform.parent.gameObject);
                        }
                    
                   
                       
                    
                    

                    if (modPages[selectedItem] < modLastPages[selectedItem] - 1)
                    {
                        Instantiate(game.characters[modPages[1]+1], player.transform);
                        modPages[selectedItem]++;
                    }
                    else
                    {
                        modPages[selectedItem] = 0;
                    }
                }
            }
            
            if (game.isGamepad)
            {
                if (!game.d_Left && modPages[selectedItem] > 0)
                {
                    if (game.direction == "left")
                    {
                        game.d_Left = true;
                        modPages[selectedItem]--;
                        if (modPages[selectedItem] == 0)
                        {
                            //left notifier disappears
                        }
                    }
                }
                if (!game.d_Right && modPages[selectedItem] < modLastPages[selectedItem] - 1)
                {
                    if (game.direction == "right")
                    {
                        game.d_Right = true;
                        modPages[selectedItem]++;
                        if (modPages[selectedItem] == modLastPages[selectedItem])
                        {
                            //Right notifier disappears
                        }
                    }
                }

                
                if (!game.d_Left || !game.d_Right)
                {
                    if (!FindObjectOfType<SkinnedMeshRenderer>() || !FindObjectOfType<MeshRenderer>())
                    {
                        Instantiate(game.characters[modPages[1]], player.transform);
                    }

                    if (modPages[selectedItem] > -1 && game.d_Left || modPages[selectedItem] != modLastPages[selectedItem] && game.d_Right)
                    {
                        switch (selectedItem)
                        {
                            default:
                                foreach (SkinnedMeshRenderer skin in FindObjectsOfType<SkinnedMeshRenderer>())
                                {
                                    Destroy(skin.transform.parent.gameObject);
                                }
                                foreach (MeshRenderer mesh in FindObjectsOfType<MeshRenderer>())
                                {
                                    Destroy(mesh.transform.parent.gameObject);
                                }
                                break;
                            case 3: break;

                            case 4: break;
                        }
                    }
                }
            }
        }

    }
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
            #region MenuPushed
            if (menuActivator)
            {
                OpenMenu();
            }
            #endregion
            #region Gamepad Detection
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
            #endregion
            #region Reset Main Variables
            
                    if (menuActivator && menuOpen)
                    {
                        currentMenu = "Main Menu";
                        selectedHighlighter = 0;
                        menuTitle.transform.localPosition = menuTitlePos[0];
                        LoadMenu("MenuConfirm");
                    }
                

            
            if (menuActivator && menuOpen && selectedItem != 0 && game.isGamepad)
            {
                selectedItem = 0;
            }
            if (menuActivator && menuOpen && selectedItem != 0 && !game.isGamepad)
            {
                selectedItem = -100;
            }
            #endregion
            DisableMenuItems("MenuOpen");
        }

        /// <summary>
        /// Game pause determinator.
        /// </summary>
        protected void Pause()
        {
            foreach (Animator anim in FindObjectsOfType<Animator>())
            {
                anim.enabled = !game.paused;
            }
            foreach (PlayerController player in game.player)
            {
                if (player)
                {
                    player.enabled = !game.paused;

                }
            }
            foreach (BotReciever bot in game.bot)
            {

            }
            if (!menuOpen && menuActivator)
            {
                MenuItemsCount();
            }
        }

        /// <summary>
        /// Moves the highlighters when required.
        /// </summary>
        protected void HighlightPos()
        {
            if (name != "Title Menu")
            {
                if (selectedItem == -100)
                {
                    highlighter[selectedHighlighter].transform.position = new Vector3(-603, 635, 0);
                }
                if (selectedItem != -100)
                {
                    if (menuItemsCurrentContext[0])
                    {
                        highlighter[selectedHighlighter].transform.position = menuItemsCurrentContext[selectedItem].transform.position;
                    }
                }
            }
            else
            {
                foreach (GameObject item in menuItemsCurrentContext)
                {
                    if (item == null)
                    { return; }

                    if (item.GetComponent<MouseMenu>().buttonNumber == selectedItem)
                    {
                        item.GetComponent<Image>().color = colours[1];
                    }
                    else
                    {
                        item.GetComponent<Image>().color = colours[0];
                    }
                }
            }
        }

        /// <summary>
    }
    /// <summary>
    /// The controls aspect of the userinterface.
    /// </summary>
    public class Controls : Math
    {
        protected void ConfirmMenu()
        {
            if (Input.GetKeyDown(game.button_Attack))
            {

                switch (currentMenu)
                {
                    case "Mods Menu":

                        return;
                        

                    case "Items Menu":
                        if (selectedItem == menuItemsCounted)
                        {
                            navigationMode = "Grid";
                        }
                        break;

                    case "Level Select Menu":
                        
                        if (SceneManager.GetSceneByName(menuItemsCurrentContext[selectedItem].GetComponentInChildren<Text>().text).buildIndex != -1)
                        {
                            SceneManager.LoadScene(menuItemsCurrentContext[selectedItem].GetComponentInChildren<Text>().text);
                            Destroy(FindObjectOfType<GameManager>().gameObject);
                        }
                        else
                        {
                            return;
                        }
                        break;
                }

                if (selectedItem >= 0 && selectedItem <= menuItemsCurrentContext.Length - 2 && selectedItem != -100)
                {

                    switch (menuItemsCurrentContext[selectedItem].GetComponentInChildren<Text>().text)
                    {
                        default:
                            lastMenuEntered[lastMove] = currentMenu;
                            lastMove++;

                            currentMenu = menuItemsCurrentContext[selectedItem].GetComponentInChildren<Text>().text + " Menu";
                            LoadMenu("MenuConfirm");
                            break;

                        case "New Game":
                            DestroyImmediate(FindObjectOfType<GameManager>().gameObject);
                            SceneManager.LoadScene(1);
                            return;

                        

                        case "Quit Game":
                            Application.Quit();
                            return;


                        case "Save":
                            highlighter[0].transform.position = new Vector3(-720.4601f, 531.5f, 0);
                            selectedHighlighter = 1;
                            lastMenuEntered[lastMove] = currentMenu;
                            lastMove++;

                            currentMenu = menuItemsCurrentContext[selectedItem].GetComponentInChildren<Text>().text + " Menu";
                            LoadMenu("MenuConfirm");
                            break;

                        case "Stock":
                            highlighter[0].transform.position = new Vector3(-720.4601f, 531.5f, 0);
                            selectedHighlighter = 2;
                            lastMenuEntered[lastMove] = currentMenu;
                            lastMove++;

                            currentMenu = menuItemsCurrentContext[selectedItem].GetComponentInChildren<Text>().text + " Menu";
                            LoadMenu("MenuConfirm");
                            break;

                    }

                    if (SceneManager.GetActiveScene().name == "Title Screen")
                    {
                        switch (menuItemsCurrentContext[selectedItem].GetComponentInChildren<Text>().text)
                        {
                            case "Mods":
                                if (!FindObjectOfType<SkinnedMeshRenderer>() || !FindObjectOfType<MeshRenderer>())
                                {
                                    Instantiate(game.characters[0], player.transform);
                                }
                                break;
                        }
                    }
                    if (game.isGamepad)
                    {
                        selectedItem = 0;
                    }
                    else
                    {
                        selectedItem = -100;
                    }
                    if (name != "Title Menu")
                    {
                        menuTitle.transform.localPosition = menuTitlePos[1];
                        menuTitle.GetComponentInChildren<Text>().text = currentMenu;
                    }
                }
               

            }
        }

        protected void CancelMenu()
        {
            if (Input.GetKeyDown(game.button_Jump))
            {
                if (name == "Title Menu" && currentMenu == "Main Menu")
                {
                    return;
                }

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
                        navigationMode = "Vertical";
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
                if (name == "Title Menu")
                {
                    return;
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
            switch (name)
            {
                case "Save Menu":
                    menuOpen = !menuOpen;
                    
                    break;

                case "Pause Menu":
                    if (!menuOpen && !game.paused)
                    {
                        
                        menuOpen = !menuOpen;
                    }
                    else if (menuOpen && game.paused)
                    {
                        menuOpen = !menuOpen;
                        
                    }
                    break;
            }

            GameObject.Find("Menu Backdrop").GetComponent<Canvas>().enabled = menuOpen;

            GetComponent<Canvas>().enabled = menuOpen;
        }

        /// <summary>
        /// Handles vertical scrolling.
        /// </summary>
        protected virtual void VerticalHandler()
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
        /// Handles menus that work off a grid like format such as the item stock menu.
        /// </summary>
        protected virtual void GridHandler()
        {

            if (game.isGamepad)
            {
                if (game.direction == "up")
                {
                    switch (selectedItem)
                    {
                        default:
                            if (!game.d_Up)
                            {
                                game.d_Up = true;
                                selectedItem -= 3;
                            }
                            break;

                        case 0:
                            break;

                        case 1:
                            break;

                        case 2:
                            break;
                    }
                }
                if (game.direction == "down")
                {
                    if (!game.d_Down)
                    {
                        game.d_Down = true;
                        selectedItem += 3;
                    }
                }

                if (game.direction == "left")
                {
                    switch (selectedItem)
                    {
                        default:
                            if (!game.d_Left)
                            {
                                game.d_Left = true;
                                selectedItem--;
                            }
                            break;

                        case 0:

                            break;

                        case 3:
                            break;

                        case 6:
                            break;

                        case 9:
                            break;
                    }

                }


                if (game.direction == "right")
                {
                    if (selectedItem != menuItemsCounted)
                    {
                        switch (selectedItem)
                        {
                            default:

                                if (!game.d_Right)
                                {
                                    game.d_Right = true;
                                    selectedItem++;
                                }
                                break;

                            case 2:
                                break;

                            case 5:
                                break;

                            case 8:
                                break;

                            case 11:
                                break;

                        }
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
                menuItems.Add(m);
                mouseMenuAmount++;
            }
        }
        /// <summary>
        /// Counts how many mousemenu items this menu has in the context of the selected menu.
        /// </summary>
        protected void MenuItemsCountRelevant()
        {
            //Clears the menu items context array to make room for the new items.
			menuItemsCurrentContext = new GameObject[10];
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
        /// Tells the game if the menu in question requires a menu backdrop.
        /// </summary>
        protected bool usesBackdrop;

        /// <summary>
        /// The trigger for the menu opening.
        /// </summary>
        [HideInInspector] public bool menuActivator;

        /// <summary>
        /// Tells if the menu is currently open.
        /// </summary>
        protected bool menuOpen;

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
        /// The current menu the player is navigating.
        /// </summary>
        protected string currentMenu = "Main Menu";

        /// <summary>
        /// stores which menu's were previously opened so that you can go back.
        /// </summary>
        public string[] lastMenuEntered = new string[5];

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
        public int menuItemsCounted;

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
        public GameObject[] menuItemsCurrentContext = new GameObject[99];

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
        public List<MouseMenu> menuItems = new List<MouseMenu>();

        /// <summary>
        /// If there are other menus this will store it.
        /// </summary>
        public MenuChooser[] menuChooser = new MenuChooser[2];

        /// <summary>
        /// Grabs variables from the game manager.
        /// </summary>
        public GameManager game;
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
        
        protected string navigationMode = "Vertical";

        

        public Color[] colours = new Color[2];
        protected GameObject modelPrefab;
        protected GameObject player;
        protected bool saveExists;
        public int[] modPages = new int[4];
        public int[] modLastPages = new int[4];
        #endregion
    }
}
