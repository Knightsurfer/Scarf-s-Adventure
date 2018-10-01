using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : PauseBasic {

    #region Start
    protected void Start()
    {
        BasicStart();
    }
    #endregion
    #region Update
    protected void Update()
    {
        BasicUpdate();
        MenuChoose();
        if (paused)
        {
            if (gamepad.isGamepad)
            {
                if (gamepad.button_Jump && paused == true)
                {
                    CancelMenu();
                }
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
    void MainMenu()
    {

        UpDownHandler(0, 6);
        if (!mainCanvas.enabled && mainCanvas != null)
        {
            mainCanvas.enabled = !mainCanvas.enabled;
        }
        if (gamepad.isGamepad)
        {
            if (selected == -100)
                selected = 0;
            selectedItem = menuItems[selected].GetComponentInChildren<Text>().text;

            if (gamepad.isGamepad && gamepad.button_Attack)
            {
                SelectedMenu();
                selected = 0;
                HighlightPos();

            }


        }
        else
        {
            if(!mouseSelected)
            {
                selected = -100;
            }
        }

    }







    void ItemsMenu()
    {
        UpDownHandler(0, 4);
    }
    void EquipMenu()
    {
        UpDownHandler(0, 3);
    }
    void AbilityMenu()
    {
        UpDownHandler(0, 3);
    }
    void CustomMenu()
    {
        UpDownHandler(0, 3);
    }
    void StatusMenu()
    {
        UpDownHandler(0, 3);
    }
    void JournalMenu()
    {
        {
            UpDownHandler(0, 3);
        }
    }
    void ConfigMenu()
    {
        UpDownHandler(0, 3);
    }

}
public class PauseBasic : MonoBehaviour
{
    protected GameObject[] menuItems = new GameObject[8];
    protected string selectedItem;
    protected GameObject menuNotifier;
    public GameObject[] characterStats = new GameObject[1];
    public GameObject[] characterNames = new GameObject[1];
    protected GameManager game;

    public bool canMove;

    protected Gamepad gamepad;
    protected bool mouseSelected;

    


    #region Pause Check  //Variables for checking if the game is paused.
    [HideInInspector] public bool paused;
    protected Canvas pausePanel;
    public GameObject player;
    #endregion
    #region Pause Menu   //Variables that dictates what item is selected or what world the player is in.
    protected int selected = -100;
    protected int selectedMin;
    protected int selectedMax;
    protected Vector3 highlightPos;

    protected Image highlighter;
    protected Text stageName;
    protected Text worldName;

    readonly NavMeshAgent[] agents = new NavMeshAgent[] { };


    #endregion
    #region SelectedMenu //Variables that tell which menu screen to show.
    protected string currentMenu = "Main Menu";

    protected Canvas mainCanvas;
    protected Canvas itemCanvas;
    protected Canvas equipCanvas;
    #endregion
    protected void BasicStart()
    {

        gamepad = FindObjectOfType<Gamepad>();

        
            if (gamepad.isGamepad)
            {
                selected = 0;
            }
        





        game = FindObjectOfType<GameManager>();
        if (game.mode == "ThirdPerson")
        {
            characterStats[0] = GameObject.Find("Player1 Stats");
            characterNames[0] = GameObject.Find("Player1 Name");
        }

            menuItems[0] = GameObject.Find("Items");
            menuItems[1] = GameObject.Find("Equipment");
            menuItems[2] = GameObject.Find("Abilities");
            menuItems[3] = GameObject.Find("Customize");
            menuItems[4] = GameObject.Find("Status");
            menuItems[5] = GameObject.Find("Journal");
            menuItems[6] = GameObject.Find("Config");
            menuItems[7] = GameObject.Find("Spare(7)");

            menuNotifier = GameObject.Find("Menu Title");
        

        //Makes sure all the assets are in the level.


        worldName = GameObject.Find("World Name").GetComponent<Text>();
        stageName = GameObject.Find("Level Section").GetComponent<Text>();

        highlighter = GameObject.Find("Highlight").GetComponent<Image>();
        highlighter.rectTransform.position = new Vector3(244, 775, 0);

        CanvasAssign();
    }
    protected void CanvasAssign()
    {
        //Assigns all the canvases used in the pause menu.
        pausePanel = GetComponent<Canvas>();
        mainCanvas = GameObject.Find("Main Menu").GetComponent<Canvas>();
    }

    protected void BasicUpdate()
    {
        //Checks for a controller and then checks if the game is paused.
        if (FindObjectOfType<ThirdPersonController>())
        {
            canMove = (FindObjectOfType<ThirdPersonController>().canMove);
            player = GameObject.FindGameObjectWithTag("Player");
        }

        CharacterStats();
        if (canMove)
        {
            PauseCheck();
        }
    }

    protected void PauseCheck()
    {
        //If start is pressed, pause or unpause the game and assign the title of the world.

        if (gamepad.button_Start)
        {
            if (!paused)
            {
                switch (SceneManager.GetActiveScene().buildIndex)
                {
                    case 2:
                        stageName.text = "|| Dream State: Tutorial";
                        worldName.text = "Dream\nState";
                        break;
                }
            }
            pausePanel.enabled = !pausePanel.enabled;
            paused = pausePanel.enabled;
            if (gamepad.controller == "Keyboard")
            {
                Cursor.visible = pausePanel.enabled;
            }
            GamePause();

        }
    }
    protected void GamePause()
    {
        //Disables all movement of the player when paused.
        if (GameObject.FindGameObjectWithTag("Player"))
        {
           foreach(Animator anim in FindObjectsOfType<Animator>())
            {
                anim.enabled = !paused;
            }

                if (FindObjectOfType<RTS_Controls>().enabled == false)
                {
                 player.GetComponent<ThirdPerson_Mode>().enabled = !paused;
                  
                }
                if (FindObjectOfType<RTS_Controls>().enabled == true)
                {
                    player.GetComponent<Unit_Controller>().enabled = !paused;
                }
        }
    }


    protected void SelectedMenu()
    {
        switch (currentMenu)
        {
            case "Main Menu":
                switch (selected)
                {
                    case 0:
                        currentMenu = "Items";
                        menuNotifier.transform.localPosition = new Vector3(-333, 197, 0);
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
                        menuItems[7].transform.localPosition = new Vector3(195, 32.3f, 0);


                        menuItems[5].GetComponent<Image>().color = new Color(0.160f, 0.122f, 0.122f, 0.635f);
                        menuItems[6].GetComponent<Image>().color = new Color(0.160f, 0.122f, 0.122f, 0.635f);



                        print(menuItems[0].GetComponent<Image>().color);
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
                        menuItems[7].transform.localPosition = new Vector3(195, 32.3f, 0);


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




                }
                break;


        }

    }
    protected void CancelMenu()
    {
        menuNotifier.transform.localPosition = new Vector3(-200, 279, 0);
        switch (currentMenu)
        {
            case "Main Menu":
                pausePanel.enabled = false;
                paused = pausePanel.enabled;
                GamePause();
                break;

            case "Items":
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
                menuItems[7].transform.localPosition = new Vector3(-37, 266, 0);


                menuItems[5].GetComponent<Image>().color = new Color(0.160f, 0.122f, 0.122f, 1.000f);
                menuItems[6].GetComponent<Image>().color = new Color(0.160f, 0.122f, 0.122f, 1.000f);
                break;

            case "Equipment":
                currentMenu = "Main Menu";

                menuItems[0].GetComponentInChildren<Text>().text = "    Items";
                menuItems[1].GetComponentInChildren<Text>().text = "    Equipment";
                menuItems[2].GetComponentInChildren<Text>().text = "    Abilities";
                menuItems[3].GetComponentInChildren<Text>().text = "    Customize";
                menuItems[4].GetComponentInChildren<Text>().text = "    Status";

                menuItems[5].GetComponentInChildren<Text>().text = "    Journal";
                menuItems[6].GetComponentInChildren<Text>().text = "    Config";



                menuItems[4].transform.localPosition = new Vector3(2.7f, -37, 0);
                menuItems[5].transform.localPosition = new Vector3(2.7f, -71.8f, 0);
                menuItems[6].transform.localPosition = new Vector3(2.7f, -104.7f, 0);
                menuItems[7].transform.localPosition = new Vector3(-37, 266, 0);


                menuItems[5].GetComponent<Image>().color = new Color(0.160f, 0.122f, 0.122f, 1.000f);
                menuItems[6].GetComponent<Image>().color = new Color(0.160f, 0.122f, 0.122f, 1.000f);
                break;

        }
    }

    protected void CharacterStats()
    {
        if (game)
        {
            if (game.player.Length > 0)
            {
                if (game.mode == "ThirdPerson")
                {
                    characterNames[0].GetComponent<Text>().text = game.player[0].name;
                    characterStats[0].GetComponent<Text>().text = 1 + "\n" + game.player[0].health + " / " + game.player[0].healthMax + "\n0 / 0";
                }
            }
        }
    }

    protected void MouseMenu(int selection)
    {

        if (selection != -100)
        {
            selected = selection;
            mouseSelected = true;
        }
        else
        {
            mouseSelected = false;
        }

        UpDownHandler(0, 6);
    }

    #region Controls //The main controls behind menu scrolling on the pause menu.
    protected void UpDownHandler(int min, int max)
    {

        if (gamepad.isGamepad)
        {
            if (gamepad.direction == "up")
            {
                if (gamepad.d_Up == false)
                {
                    gamepad.d_Up = true;

                    selected--;
                }
            }
            if (gamepad.direction == "down")
            {
                if (gamepad.d_Down == false)
                {
                    gamepad.d_Down = true;

                    selected++;
                }
            }
        }
        highlighter.rectTransform.position = highlightPos;
        MenuScroller(min, max);
    }
    protected void MenuScroller(int min, int max)
    {
        if (selected == min - 1)
        {
            selected = max;
        }
        if (selected == max + 1)
        {
            selected = min;
        }

        HighlightPos();
    }
    protected void HighlightPos()
    {
        switch (selected)
        {
            case 0:
                highlightPos = new Vector3(244, 775, 0);
                break;

            case 1:
                highlightPos = new Vector3(244, 703, 0);
                break;

            case 2:
                highlightPos = new Vector3(244, 629, 0);
                break;

            case 3:
                highlightPos = new Vector3(244, 555, 0);
                break;

            case 4:
                highlightPos = new Vector3(244, 485, 0);
                break;

            case 5:
                highlightPos = new Vector3(244, 412, 0);
                break;

            case 6:
                highlightPos = new Vector3(244, 344, 0);
                break;

            case -100:
                highlightPos = new Vector3(-382, 276, 0);
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