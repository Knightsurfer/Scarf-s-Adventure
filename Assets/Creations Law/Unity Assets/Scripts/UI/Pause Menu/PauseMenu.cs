using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;




//#################################//
//                                                                          //
//            PAUSE MENU SCRIPTS                            //
//                                                                        //
//                                                                       //
//###############################//
//                                                                     //
//    Pause menu related scripts are kept here.    //           
//                                                                   //
/////////////////////////////////////////////////////////









public class PauseMenu : Menus {

    private void Awake()
    {
        base.Start();
        BasicStart();
        WriteData();
    }


    #region Update
    protected new void Update()
    {
        BasicUpdate();
        MenuChoose();

        if (paused)
        {
                if (gamepad.button_Jump && paused == true)
                {
                    CancelMenu();
                }      
        }
    }
    protected void MenuChoose()
    {
        if (paused)
        {
            switch (currentMenu)
            {
                case "Main Menu":
                    MainMenu();
                    break;

                case "Items":
                    ItemsMenu();
                    break;

                    case "Equipment":
                    EquipMenu();
                    break;
            }
        }
    }

    #endregion

    //Menus

}

public class Menus : Inventory
{
    protected Vector3[] menuTitlePos = new Vector3[] {
        new Vector3(-418, 306, 0),
        new Vector3(-427.3f, 403.7f, 0) };

    protected void SelectMenu()
    {
        menuItems[8].SetActive(false);

        switch (currentMenu)
        {
            case "Main Menu":
                switch (selectedItem)
                {
                    case 0:
                        currentMenu = "Items";
                        menuNotifier.transform.localPosition = menuTitlePos[0];
                        menuNotifier.GetComponentInChildren<Text>().text = "    Items";

                        menuItems[0].GetComponentInChildren<Text>().text = "    Scarf";
                        menuItems[1].GetComponentInChildren<Text>().text = "    ClownFace";
                        menuItems[2].GetComponentInChildren<Text>().text = "    Lamp";
                        menuItems[3].GetComponentInChildren<Text>().text = "    Rover";
                        menuItems[4].GetComponentInChildren<Text>().text = "    Stock";

                        menuItems[5].GetComponentInChildren<Text>().text = "    ---";
                        menuItems[6].GetComponentInChildren<Text>().text = "    ---";


                        menuItems[5].transform.localPosition = new Vector3(195, 102, 0);
                        menuItems[6].transform.localPosition = new Vector3(195, 67.6f, 0);
                        menuItems[8].transform.localPosition = new Vector3(195, 32.3f, 0);


                        menuItems[5].GetComponent<Image>().color = new Color(0.160f, 0.122f, 0.122f, 0.635f);
                        menuItems[6].GetComponent<Image>().color = new Color(0.160f, 0.122f, 0.122f, 0.635f);



                        //print(menuItems[0].GetComponent<Image>().color);
                        break;

                    case 1:
                        currentMenu = "Equipment";
                        menuNotifier.transform.localPosition = new Vector3(-333, 197, 0);
                        menuNotifier.GetComponentInChildren<Text>().text = "    Equipment";

                        menuItems[0].GetComponentInChildren<Text>().text = "    Scarf";
                        menuItems[1].GetComponentInChildren<Text>().text = "    ClownFace";
                        menuItems[2].GetComponentInChildren<Text>().text = "    Lamp";
                        menuItems[3].GetComponentInChildren<Text>().text = "    Rover";


                        menuItems[5].GetComponentInChildren<Text>().text = "    ---";
                        menuItems[6].GetComponentInChildren<Text>().text = "    ---";

                        menuItems[4].transform.localPosition = new Vector3(-37, 266, 0);

                        menuItems[5].transform.localPosition = new Vector3(195, 102, 0);
                        menuItems[6].transform.localPosition = new Vector3(195, 67.6f, 0);
                        menuItems[8].transform.localPosition = new Vector3(195, 32.3f, 0);


                        menuItems[5].GetComponent<Image>().color = new Color(0.160f, 0.122f, 0.122f, 0.635f);
                        menuItems[6].GetComponent<Image>().color = new Color(0.160f, 0.122f, 0.122f, 0.635f);
                        break;

                    case 2:
                        break;

                    case 3:
                        break;

                    case 4:
                        break;

                    case 5:
                        break;

                    case 6:
                        break;

                    case 7:
                        SceneManager.LoadScene(0);
                        Destroy(GameObject.Find("Game Manager"));
                        break;
                }
                break;
        }
    }
    protected void CancelMenu()
    {
        switch (currentMenu)
        {
            case "Main Menu":
                pausePanel.enabled = false;
                paused = pausePanel.enabled;
                GamePause();
                break;

            case "Items":
                menuNotifier.transform.localPosition = menuTitlePos[1];
                currentMenu = "Main Menu";

                menuItems[0].GetComponentInChildren<Text>().text = "    Items";
                menuItems[1].GetComponentInChildren<Text>().text = "    Equipment";
                menuItems[2].GetComponentInChildren<Text>().text = "    Abilities";
                menuItems[3].GetComponentInChildren<Text>().text = "    Customize";
                menuItems[4].GetComponentInChildren<Text>().text = "    Status";

                menuItems[5].GetComponentInChildren<Text>().text = "    Journal";
                menuItems[6].GetComponentInChildren<Text>().text = "    Config";




                menuItems[5].transform.localPosition = new Vector3(2.7f, -71.8f, 0);
                menuItems[6].transform.localPosition = new Vector3(2.7f, -104.7f, 0);
                menuItems[8].transform.localPosition = new Vector3(-37, 266, 0);


                menuItems[5].GetComponent<Image>().color = new Color(0.160f, 0.122f, 0.122f, 1.000f);
                menuItems[6].GetComponent<Image>().color = new Color(0.160f, 0.122f, 0.122f, 1.000f);
                break;

            case "Equipment":
                currentMenu = "Main Menu";

                #region Titles
                menuItems[0].GetComponentInChildren<Text>().text = "    Items";
                menuItems[1].GetComponentInChildren<Text>().text = "    Equipment";
                menuItems[2].GetComponentInChildren<Text>().text = "    Abilities";
                menuItems[3].GetComponentInChildren<Text>().text = "    Customize";
                menuItems[4].GetComponentInChildren<Text>().text = "    Status";

                menuItems[5].GetComponentInChildren<Text>().text = "    Journal";
                menuItems[6].GetComponentInChildren<Text>().text = "    Config";
                #endregion
                #region Positions
                menuItems[4].transform.localPosition = new Vector3(2.7f, -37, 0);
                menuItems[5].transform.localPosition = new Vector3(2.7f, -71.8f, 0);
                menuItems[6].transform.localPosition = new Vector3(2.7f, -104.7f, 0);
                menuItems[8].transform.localPosition = new Vector3(-37, 266, 0);
                #endregion
                #region Colours
                menuItems[5].GetComponent<Image>().color = new Color(0.160f, 0.122f, 0.122f, 1.000f);
                menuItems[6].GetComponent<Image>().color = new Color(0.160f, 0.122f, 0.122f, 1.000f);
                #endregion

                break;

        }
    }


    protected void MainMenu()
    {

        #region Player Stats
        foreach (ThirdPerson p in game.player)
        {
            characterStats[playercount] = GameObject.Find("Player" + playercount + " Stats").GetComponent<Text>();
            characterNames[playercount] = GameObject.Find("Player" + playercount + " Name").GetComponent<Text>();
        }
        #endregion
        #region Canvas Controls
        if (!mainCanvas.enabled && mainCanvas != null)
        {
            mainCanvas.enabled = !mainCanvas.enabled;
        }
        #endregion
        #region Menu Selection Handler
        UpDownHandler(0, 7);
        if (gamepad.isGamepad)
        {
            if (selectedItem == -100)
            {
                selectedItem = 0;
            }
            selectedMenuItem = menuItems[selectedItem].GetComponentInChildren<Text>().text;
            if (gamepad.button_Attack)
            {
                SelectMenu();
                selectedItem = 0;
                HighlightPos();
            }
        }
        else
        {
            if (!mouseSelected)
            {
                selectedItem = -100;
            }
            if (gamepad.button_Attack)
            {
                SelectMenu();
                selectedItem = 0;
                HighlightPos();
            }
        }
        #endregion
    }
    protected void ItemsMenu()
    {
        UpDownHandler(0, 4);
    }
    protected void EquipMenu()
    {
        UpDownHandler(0, 3);
    }
    protected void AbilityMenu()
    {
        UpDownHandler(0, 3);
    }
    protected void CustomMenu()
    {
        UpDownHandler(0, 3);
    }
    protected void StatusMenu()
    {
        UpDownHandler(0, 3);
    }
    protected void JournalMenu()
    {
        {
            UpDownHandler(0, 3);
        }
    }
    protected void ConfigMenu()
    {
        UpDownHandler(0, 3);
    }
}

public class Inventory : PauseBasic
{
    int index;
  

    protected string[] scarfItems = { "---" };
    protected string[] clownItems = { "---" };
    protected string[] stockItems = { "Potion", "Key" };

    
    

    public ThirdPerson[] chari = { };

    protected void WriteData()
    {
        chari = FindObjectsOfType<ThirdPerson>();
        



        
        foreach (ThirdPerson c in chari)
        {
            switch(c.name)
            {
                case "Scarf":
                    foreach (string item in scarfItems)
                    {
                        game.Items.Write(c.name, "Item" + index, item);
                        index++;
                    }
                    index = 0;
                    break;

                case "Clown":
                    foreach (string item in clownItems)
                    {
                        game.Items.Write(c.name, "Item" + index, item);
                        index++;
                    }
                    index = 0;
                    break;

                case "Stock":
                    foreach (string item in stockItems)
                    {
                        game.Items.Write(c.name, "Item" + index, item);
                        index++;
                    }
                    index = 0;
                    break;
            }

            foreach (string item in stockItems)
            {
                game.Items.Write("Stock", "Item" + index, item);
                index++;
            }
            index = 0;

        }






    }




}

public class PauseBasic : UIControls
{
    #region //Variables
    #region Menu Variables //Events that exist in the pause menu.
    protected Canvas pausePanel;
    protected GameObject[] menuItems = new GameObject[9];
    protected string selectedMenuItem;
    protected GameObject menuNotifier;
    protected Text[] characterStats = new Text[4];
    protected Text[] characterNames = new Text[4];
    protected bool mouseSelected;
    [HideInInspector] public bool paused;
    #endregion
    #region Game Variables //Events that exist in the game world.
    public bool canMove;
    public GameObject player;
    protected GameManager game;

    protected int animCount;
    protected Animator[] animators;
    protected NavMeshAgent[] agents = new NavMeshAgent[] { };

    protected ThirdPerson[] players = new ThirdPerson[4];
    protected int peoplecount;
    protected int playercount;
    #endregion

    #region Pause Menu     //Variables that dictates what item is selected or what world the player is in. 
    protected int selectedMin;
    protected int selectedMax;
    protected Vector3 highlightPos;

    protected Image highlighter;
    protected Text stageName;
    protected Text worldName;
    #endregion
    #region SelectedMenu   //Variables that tell which menu screen to show.
    protected string currentMenu = "Main Menu";

    protected Canvas mainCanvas;
    protected Canvas itemCanvas;
    protected Canvas equipCanvas;
    #endregion
    #endregion

    protected void BasicStart() //Asigns all the variables to their respective objects when the level starts.
    {
       if (gamepad.isGamepad) //Determines if a gamepad is plugged in
            {
                selectedItem = 0;
            }

        #region Other Scripts         //When the game needs to grab a variable from somewhere else these are where they end up.
        game = FindObjectOfType<GameManager>();
        animators = FindObjectsOfType<Animator>();
        #endregion
        #region Select All Menu Items //All of the menu assets should go here. 
        #region Buttons
        menuItems[0] = GameObject.Find("Items");
        menuItems[1] = GameObject.Find("Equipment");
        menuItems[2] = GameObject.Find("Abilities");
        menuItems[3] = GameObject.Find("Customize");
        menuItems[4] = GameObject.Find("Status");
        menuItems[5] = GameObject.Find("Journal");
        menuItems[6] = GameObject.Find("Config");
        menuItems[7] = GameObject.Find("Quit");
        menuItems[8] = GameObject.Find("Spare(7)");
        #endregion
        #region Menu Assets
        #region Selection Info
        menuNotifier = GameObject.Find("Menu Title");
        highlighter = GameObject.Find("Highlight").GetComponent<Image>();
        #endregion
        #region  World Based Assets
        worldName = GameObject.Find("World Name").GetComponent<Text>();
        stageName = GameObject.Find("Level Section").GetComponent<Text>();
        #endregion
        #endregion
        #endregion

        CanvasAssign();
    }
    protected void CanvasAssign() //Assigns all the canvases used in the pause menu.
    {
        pausePanel = GetComponent<Canvas>();
        mainCanvas = GameObject.Find("Main Menu").GetComponent<Canvas>();
    }

    protected void BasicUpdate() //Sets variable values every frame.
    {
        if (FindObjectOfType<ThirdPerson>()) //Checks if there is a player present and if they can move.
        {
            canMove = (FindObjectOfType<ThirdPerson>().canMove);
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (canMove) //Checks if the player can make any game related movements.
        {
            PauseCheck();
        }
        CharacterStats();
    } 
    protected void PauseCheck() //If start is pressed, pause or unpause the game and assign the title of the world.
    {
        foreach (ThirdPerson player in game.player)
        {
            player.anim.enabled = !paused;
        }
        if (gamepad.button_Start)
        {
            if (!paused)
            {
                switch (SceneManager.GetActiveScene().buildIndex)
                {
                    case 2:
                        stageName.text = "|| Test: Gameplay Test";
                        worldName.text = "Test\nLevel";
                        break;

                    case 3:
                        stageName.text = "|| Dream State: Tutorial";
                        worldName.text = "Dream\nState";
                        break;
                }
            }
            if (!gamepad.isGamepad)
            {
                Cursor.visible = pausePanel.enabled;
            }
            pausePanel.enabled = !pausePanel.enabled;
            paused = pausePanel.enabled;
            GamePause();
        }
    }
    protected void GamePause()  //Disables all movement of the player when paused.
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            game.player[0].GetComponent<Thirdperson_Mode>().enabled = !paused;
            foreach (Animator anim in animators)
            {
                animators[animCount].enabled = !paused;
                animCount++;
            }
            animCount = 0;
        }
    }
    
    protected void CharacterStats()
    {
        players = FindObjectsOfType<ThirdPerson>();
        if (game.player.Length > 0)
        {
            foreach (ThirdPerson p in players)
            {
                characterStats[playercount] = GameObject.Find("Player" + playercount + " Stats").GetComponent<Text>();
                characterNames[playercount] = GameObject.Find("Player" + playercount + " Name").GetComponent<Text>();


                if (game.title[playercount] != "")
                {
                   // Debug.Log(game.title[playercount]);
                    characterNames[playercount].GetComponentInParent<Canvas>().enabled = true;
                }



                characterNames[playercount].text = game.title[playercount];
                characterStats[playercount].GetComponent<Text>().text = 1 + "\n" + game.player[playercount].health + " / " + game.player[playercount].healthMax + "\n0 / 0";

                
                playercount++;
            }
            
            playercount = 0;

           

        }
    }
    protected void MouseMenu(int selection) //Mouse Navigation
    {
        if (!gamepad.isGamepad)
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

    #region Controls //The main controls behind menu scrolling on the pause menu.
    public override void UpDownHandler(int min, int max)
    {
        base.UpDownHandler(min,max);
        highlighter.rectTransform.localPosition = highlightPos;
        MenuScroller(min, max);
        SendMessage("HighlightPos");

    }

    protected void HighlightPos()
    {
        switch (selectedItem)
        {
            case 0:
                highlightPos = new Vector3(-417.7f, 258f, 0);
                break;

            case 1:
                highlightPos = new Vector3(-417.7f, 223.3f, 0);
                break;

            case 2:
                highlightPos = new Vector3(-417.7f, 187f, 0);
                break;

            case 3:
                highlightPos = new Vector3(-417.7f, 151.7f, 0);
                break;

            case 4:
                highlightPos = new Vector3(-417.7f, 118f, 0);
                break;

            case 5:
                highlightPos = new Vector3(-417.7f, 83.4f, 0);
                break;

            case 6:
                highlightPos = new Vector3(-417.7f, 51.1f, 0);
                break;

            case 7:
                highlightPos = new Vector3(-417.7f, -12.7f, 0);
                break;

            case -100:
                highlightPos = new Vector3(-427, 401, 0);
                break;
        }
    }
    #endregion
    #region Unimplemented //Includes restarting the level and quitting the game.
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}