//||=====================================||
//||                                                                                   ||
//||                            Game Manager                                 ||
//||                                                                                   ||
//||                                                                                   ||
//||=====================================||
//||                                                                                   ||
//||    Handles all of the variables that get used                    ||
//||    from scene to scene.                                                 ||
//||                                                                                   ||
//||=====================================||


using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using ScriptableObjects;



public class GameManager : Game.Player.Inventory
{
    void Awake()
    {
        if (game == null)
        {
            DontDestroyOnLoad(gameObject);
            game = this;
        }
        else if(game != this)
        {
            Destroy(gameObject);
        }
        UISetup();
    }
    void Start()
    {
        StartingVariables();
        CharacterDetector();
    }
    void Update()
    {
        ControllerCheck();

        FunctionKeys();
        UIUpdate();
        CharacterDetector();
    }
}
namespace Game
{
    namespace Player
    {
        public class Inventory : Amount
        {
            public delegate void OnItemChanged();
            public OnItemChanged onItemChangedCallback;
            /// <summary>
            /// Adds an item to your inventory.
            /// </summary>
            /// <param name="currentItem"></param>
            /// <returns></returns>
            public bool Add(_Item currentItem)
            {
                int i = 0;
                
                foreach (string names in itemNames)
                {
                    if (currentItem.name == names)
                    {
                        foreach (_Item item in items)
                        {
                            if (currentItem.name == item.name)
                            {
                                itemAmount[i]++;

                                foreach (GameObject menuItem in stockItems)
                                {
                                    if (menuItem.name == currentItem.name)
                                    {
                                        menuItem.GetComponentInChildren<Text>().text = itemNames[i] + ": " + itemAmount[i];
                                    }
                                }
                                return true;
                            }
                        }
                        items.Add(currentItem);
                        itemAmount[i]++;

                        newItem = Instantiate(slotAdd, inventoryBox);
                        stockItems.Add(newItem);


                        newItem.name = currentItem.name;
                        newItem.GetComponentInChildren<Text>().text = currentItem.name + ": " + itemAmount[i];
                        newItem.GetComponent<MouseMenu>().buttonNumber = stockItems.Count-1;
                        GameObject.Find("Pause Menu").GetComponent<MenuChooser>().menuItems.Add(newItem.GetComponent<MouseMenu>());

                        i = 0;
                        return true;
                    }
                    i++;
                }
                return false;
            }
            public void Remove(_Item item)
            {
                items.Remove(item);
            }
        }
        public class Amount : Misc.LevelLoaded
        {
            /// <summary>
            /// Counts all characters in party.
            /// </summary>
            protected void CharacterDetector()
            {
                if (player.Count < 1)
                {
                    player.AddRange(FindObjectsOfType<PlayerController>());
                    bot.AddRange(FindObjectsOfType<BotReciever>());
                }
            }
        }
    }
    namespace Controller
    {
        /// <summary>
        /// Handles all Input and organises it neatly into bools and floats.
        /// </summary>
        public class Gamepad : MonoBehaviour
        {
            #region Keycode Stuff
           readonly bool isTest = false;
            //float scroll = 0;

            #region Controllers //Variables for detecting how many controllers there are.
            public string controller;
            [HideInInspector] public bool isGamepad;
            protected string selectedController;
            [HideInInspector] public string[] currentControllers = new string[] { };
            #endregion
            #region Axis    //All analog movement is recorded as a float here.
            [HideInInspector] public string direction = "none";

            [HideInInspector] public float moveX;
            [HideInInspector] public float moveY;

            [HideInInspector] public float cameraX;
            [HideInInspector] public float cameraY;

            [HideInInspector] public float triggerL;
            [HideInInspector] public float triggerR;
            #endregion
            #region Buttons //All button feedback is recorded as a bool here.
            [HideInInspector] public KeyCode button_Jump;
            [HideInInspector] public KeyCode button_Attack;
            [HideInInspector] public KeyCode button_Action;
            [HideInInspector] public KeyCode button_Kick;
            [HideInInspector] public KeyCode button_Crouch;

            [HideInInspector] public bool d_Up;
            [HideInInspector] public bool d_Down;
            [HideInInspector] public bool d_Left;
            [HideInInspector] public bool d_Right;
            [HideInInspector] public KeyCode button_L;
            [HideInInspector] public KeyCode button_R;

            [HideInInspector] public KeyCode button_Start;
            [HideInInspector] public KeyCode button_Select;
            #endregion
            #endregion

            protected void ControllerCheck()
            {
                //Controller mode switcher.
                #region Controller Detect
                //Starts checking number of controllers and then decides what controller mode to turn on.
                //Will Come back to this for a simpler version later.
                currentControllers = Input.GetJoystickNames();
                if (currentControllers.Length < 1 || currentControllers[0] == "")
                {
                    selectedController = "Keyboard";
                }
                if (currentControllers.Length > 0)
                {
                    selectedController = currentControllers[currentControllers.Length - 1];
                    if (currentControllers[0] != "")
                    {
                        selectedController = currentControllers[0];
                    }
                }
                switch (selectedController)
                {
                    case "Wireless Controller":
                        controller = "PS4";
                        break;

                    case "Controller (Xbox 360 Wireless Receiver for Windows)":
                        controller = "Xbox";
                        break;

                    case "Keyboard":
                        controller = "Keyboard";
                        break;

                    case "SFC30 Joystick":
                        controller = "Snes";
                        break;
                }

                if (selectedController == "Wireless Controller")
                {
                    controller = "PS4";
                }
                if (selectedController == "Controller (Xbox 360 Wireless Receiver for Windows)")
                {
                    controller = "Xbox";
                }
                if (selectedController == "")
                {
                    controller = "Keyboard";
                }
                #endregion
                switch (controller)
                {
                    case "PS4":
                        isGamepad = true;
                        Ps4Conversion();
                        break;

                    case "Xbox":
                        isGamepad = true;
                        XboxConversion();
                        break;

                    case "Keyboard":
                        isGamepad = false;
                        KeyboardConversion();
                        break;

                    case "Snes":
                        isGamepad = true;
                        SnesConversion();
                        break;
                }

            }

            void KeyboardConversion()
            {
                //Keyboard Controls
                #region Move
                #region MoveX
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    moveY = 1;
                }
                if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
                {
                    moveY = 0;
                }
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    moveY = -1;
                }
                if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
                {
                    moveY = 0;
                }
                #endregion
                #region MoveY
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    moveX = -1;
                }
                if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    moveX = 0;
                }

                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    moveX = 1;
                }
                if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    moveX = 0;
                }
                #endregion
                #endregion
                #region Camera
                cameraX = Input.GetAxis("Mouse X");
                cameraY = Input.GetAxis("Mouse Y");
                #endregion
                #region D-Y //Menu scroll (up/down)
                //if (Input.GetAxis("Mouse ScrollWheel") > 0)
                //{
                //    direction = "up";
                //}
                //if (Input.GetAxis("Mouse ScrollWheel") < 0)
                //{
                //    direction = "down";
                //}
                //if (Input.GetAxis("Mouse ScrollWheel") == 0)
                //{
                //    d_Up = false;
                //    d_Down = false;
                //    direction = "none";
                //}


                if (Input.GetAxis("Mouse ScrollWheel") * 10 > 0)
                {
                    //scroll = 1;
                }
                if (Input.GetAxis("Mouse ScrollWheel") * 10 < 0)
                {
                    //scroll = -1;
                }
                if (Input.GetAxis("Mouse ScrollWheel") * 10 == 0)
                {
                    //scroll = 0;
                }



                #endregion
                #region Actions
                button_Jump = KeyCode.Mouse1;
                button_Attack = KeyCode.Mouse0;
                button_Action = KeyCode.E;
                button_Crouch = KeyCode.LeftControl;

                button_Start = KeyCode.Escape;
                button_Select = KeyCode.Backspace;
                #endregion
            }
            void XboxConversion()
            {
                #region Axis
                moveX = Input.GetAxis("Horizontal");
                moveY = Input.GetAxis("Vertical");

                cameraX = Input.GetAxis("Axis4");
                cameraY = -Input.GetAxis("Axis5");

                if (Input.GetAxis("Axis7") == 0 && Input.GetAxis("Axis6") == 0)
                {
                    d_Up = false;
                    d_Down = false;

                    d_Left = false;
                    d_Right = false;
                    direction = "none";
                }

                #region D-X //Horizontal D-pad input.
                if (Input.GetAxis("Axis6") == -1)
                {
                    direction = "left";
                }
                if (Input.GetAxis("Axis6") == 1)
                {
                    direction = "right";
                }


                if (!d_Right)
                {
                    if (direction == "right")
                    {

                    }

                }


                if (!d_Left)
                {
                    if (direction == "left")
                    {

                    }

                }

                #endregion
                #region D-Y //Vertical D-pad input.
                if (!d_Up)
                {
                    if (Input.GetAxis("Axis7") > 0)
                    {
                        direction = "up";
                        if (isTest)
                        {
                            d_Up = true;
                        }
                    }
                }
                if (!d_Down)
                {
                    if (Input.GetAxis("Axis7") < 0)
                    {
                        direction = "down";
                        if (isTest)
                        {
                            d_Down = true;
                        }
                    }
                }

                #endregion
                #region Triggers
                if (Input.GetAxis("Axis3") > 0)
                {
                    triggerL = Input.GetAxis("Axis3");
                }
                if (Input.GetAxis("Axis3") < 0)
                {
                    triggerR = -Input.GetAxis("Axis3");
                }
                if (Input.GetAxis("Axis3") == 0)
                {
                    triggerL = 0;
                    triggerR = 0;
                }
                #endregion

                #endregion
                #region Buttons
                button_Jump = KeyCode.JoystickButton1;
                button_Attack = KeyCode.JoystickButton0;
                button_Kick = KeyCode.JoystickButton2;
                button_Action = KeyCode.JoystickButton3;

                button_L = KeyCode.JoystickButton4;
                button_R = KeyCode.JoystickButton5;

                button_Start = KeyCode.JoystickButton7;
                button_Select = KeyCode.JoystickButton6;
                #endregion
            }
            void Ps4Conversion()
            {
                #region Axis
                #region Control Sticks
                moveX = Input.GetAxis("Horizontal");
                moveY = Input.GetAxis("Vertical");

                cameraX = Input.GetAxis("Axis3");
                cameraY = -Input.GetAxis("Axis6");
                #endregion
                #region Triggers 
                triggerL = Input.GetAxis("Axis4");
                triggerR = Input.GetAxis("Axis5");
                #endregion
                #endregion
                #region Buttons
                #region D-X //Horizontal D-pad input.
                if (Input.GetAxis("Axis7") < 0)
                {
                    direction = "left";
                }
                if (Input.GetAxis("Axis7") > 0)
                {
                    direction = "right";
                }
                if (Input.GetAxis("Axis7") == 0)
                {
                    d_Left = false;
                    d_Right = false;
                    direction = "none";
                }
                #endregion
                #region D-Y //Vertical D-pad input.
                if (Input.GetAxis("Axis8") > 0)
                {
                    direction = "up";
                }
                if (Input.GetAxis("Axis8") < 0)
                {
                    direction = "down";
                }
                if (Input.GetAxis("Axis8") == 0)
                {
                    d_Up = false;
                    d_Down = false;
                    direction = "none";
                }
                #endregion
                #region Main Buttons
                button_Jump = KeyCode.JoystickButton2;
                button_Attack = KeyCode.JoystickButton1;
                button_Action = KeyCode.JoystickButton3;

                button_L = KeyCode.JoystickButton4;
                button_R = KeyCode.JoystickButton5;

                button_Start = KeyCode.JoystickButton9;
                button_Select = KeyCode.JoystickButton13;
                #endregion
                #endregion
            }
            void SnesConversion()
            {
                button_Attack = KeyCode.JoystickButton4; //ButtonY
                button_Kick = KeyCode.JoystickButton0; //ButtonA
                button_Jump = KeyCode.JoystickButton1; //ButtonB
                button_Action = KeyCode.JoystickButton3; //ButtonX

                button_L = KeyCode.JoystickButton6; //ButtonL
                button_R = KeyCode.JoystickButton7; //ButtonR

                button_Select = KeyCode.JoystickButton10; //ButtonSelect
                button_Start = KeyCode.JoystickButton11; //ButtonStart

                moveX = Input.GetAxis("Horizontal");
                moveY = Input.GetAxis("Vertical");

                d_Down = Input.GetKeyDown(KeyCode.JoystickButton0); //ButtonA

                if (Input.GetKey(KeyCode.JoystickButton6))
                {
                    cameraX = -1;
                }
                if (Input.GetKey(KeyCode.JoystickButton7))
                {
                    cameraX = 1;
                }
                if (!Input.GetKey(KeyCode.JoystickButton6) && !Input.GetKey(KeyCode.JoystickButton7))
                {
                    cameraX = 0;
                }
            }
        }
    }
    namespace Misc
    {
        public class LevelLoaded : Shortcuts
        {
            private void OnEnable()
            {
                SceneManager.sceneLoaded += OnLevelFinishedLoading;
            }
            private void OnDisable()
            {
                SceneManager.sceneLoaded -= OnLevelFinishedLoading;
            }
            public AudioClip[] Music = new AudioClip[] { };
            public AudioClip[] sfx;
            private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
            {
                Start();
            }
            private void Start()
            {
                GetComponent<AudioSource>().clip = Music[startMusic];
                if (GetComponent<AudioSource>().enabled)
                {
                    GetComponent<AudioSource>().Play();
                }
                player.Clear();
                bot.Clear();
            }
        }

        public class Shortcuts : UI.Dialogue_Manager
        {
           readonly SaveEncrypt save = new SaveEncrypt();
            protected void FunctionKeys()
            {
                if (Input.GetKeyDown(KeyCode.F6))
                {
                    save.player = player;
                    save.Save("Saves", "AutoSave", "playerStats");
                }

                if (Input.GetKeyDown(KeyCode.F7))
                {
                    save.player = player;
                    save.Load("Saves", "AutoSave", "playerStats");
                }
            }
        }
    }
    namespace UI
    {
        public class Dialogue_Manager : Variables
        {
           [HideInInspector] public List<Text> dialogue = new List<Text>(2);

            [HideInInspector] public Canvas dialogueBox;
            [HideInInspector] public Image actorPortrait;
            [HideInInspector] public int currentPage;

            protected void UISetup()
            {
                if (SceneManager.GetActiveScene().name != "Title Screen")
                {
                    dialogue[0] = GameObject.Find("ActorName").GetComponent<Text>();
                    dialogue[1] = GameObject.Find("SpeechText").GetComponent<Text>();
                    dialogueBox = GameObject.Find("Text Box").GetComponent<Canvas>();
                    actorPortrait = GameObject.Find("Character Portrait").GetComponent<Image>();
                }
            }

            protected void UIUpdate()
            {
                paused = false;
                foreach(bool menus in openMenus)
                {
                    if(menus)
                    {
                        paused = true;
                    }
                }

                if (SceneManager.GetActiveScene().name != "Title Screen")
                {

                    if (actorPortrait.sprite == null)
                    {
                        actorPortrait.color = Color.clear;
                    }
                    else
                    {
                        actorPortrait.color = Color.white;
                    }
                }
            }
        }
    }

    public class Variables : Controller.Gamepad
    {
        protected void StartingVariables()
        {
            if (SceneManager.GetActiveScene().name != "Title Screen")
            {
                inventoryBox = GameObject.Find("Inventory Box").transform;
            }
        }
        public int startMusic;
        protected static GameManager game;

        public GameObject[] characters;

        [HideInInspector] public List<bool> openMenus = new List<bool>(3);
        [HideInInspector] public List<int> sound = new List<int>(3);
        public List<Sprite> actorPortraits;


        public Mesh[] meshes = new Mesh[4];
        public Material[] materials = new Material[4];
        public _Character[] playerInfo = new _Character[4];



        #region Characters
        /// <summary>
        /// Contains all of the playable characters.
        /// </summary>
        [HideInInspector] public List<PlayerController> player = new List<PlayerController>();

        /// <summary>
        /// Contains all of the CPU players.
        /// </summary>
        [HideInInspector] public List<BotReciever> bot = new List<BotReciever>();

        /// <summary>
        /// A variable that holds the inventory box in the menus.
        /// </summary>
        protected Transform inventoryBox;
        #endregion
        #region Inventory
        #region Items
        /// <summary>
        /// The list of items for the inventory box to display.
        /// </summary>
        [HideInInspector] public List<_Item> items = new List<_Item>();
        [HideInInspector] public List<GameObject> stockItems = new List<GameObject>();

        /// <summary>
        /// The slot prefab to add to the inventory box.
        /// </summary>
        [HideInInspector] public GameObject slotAdd;

        /// <summary>
        /// The new item slot added.
        /// </summary>
        protected GameObject newItem;

        /// <summary>
        /// The names of all the available items.
        /// </summary>
        [HideInInspector] public string[] itemNames =
        {
            "Key",
            "Potion"
        };

        /// <summary>
        /// The amount of an item is stored here.
        /// </summary>
        [HideInInspector] public int[] itemAmount = new int[2];
        #endregion
        #region To Be Scrapped
        /// <summary>
        /// How much space you have left in your inventory. (Becoming Redundant)
        /// </summary>
        protected int space = 10;
        #endregion
        #endregion
        [HideInInspector] public bool paused;
    }

 

}
