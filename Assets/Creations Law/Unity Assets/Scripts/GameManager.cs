using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;

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

public class GameManager : Game.Player.Inventory
{
    public static GameManager game;

    private void Awake()
    {
        if(game == null)
        {
            DontDestroyOnLoad(gameObject);
            game = this;
        }
        else if(game != this)
        {
            Destroy(gameObject);
        }
       
        
    }

    private void Start()
    {
        inventoryBox = GameObject.Find("Inventory Box").transform;
        CharacterDetector();
    }
    private void Update()
    {
        GamepadUpdate();
        FunctionKeys();
    }
}

namespace Game
{
    namespace Player
    {
        public class Inventory : Amount
        {
            //Creates an event when an item is recieved.
            public delegate void OnItemChanged();
            public OnItemChanged onItemChangedCallback;

            public bool Add(Item currentItem)
            {
                int i = 0;
                foreach (string names in itemNames)
                {
                    if (currentItem.name == names)
                    {
                        foreach (Item item in items)
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
                        newItem.GetComponent<MouseMenu>().buttonNumber = i;
                        GameObject.Find("Pause Menu").GetComponent<MenuChooser>().menuItems.Add(newItem.GetComponent<MouseMenu>());

                        i = 0;
                        return true;
                    }
                    i++;
                }
                return false;
            }
            public void Remove(Item item)
            {
                items.Remove(item);
            }
        }
        public class Amount : LevelLoaded
        {
            protected void CharacterDetector()
            {
                PlayerDetector();
                BotDetector();
            }
            protected void PlayerDetector()
            {
                player.AddRange(FindObjectsOfType<PlayerController>());
            }
            protected void BotDetector()
            {
                bot = FindObjectsOfType<BotReciever>();
            }
        }
    }

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
        private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {

            GetComponent<AudioSource>().clip = Music[SceneManager.GetActiveScene().buildIndex];
            GetComponent<AudioSource>().Play();
            player.Clear();
            Array.Clear(bot, 0, bot.Length);
        }
    }

    

    public class Shortcuts : Variables
    {
        SaveEncrypt save = new SaveEncrypt();
        protected void FunctionKeys()
        {
            if(Input.GetKeyDown(KeyCode.F6))
            {
                save.player = player;
                save.Save("Saves","AutoSave","playerStats");
            }

            if (Input.GetKeyDown(KeyCode.F7))
            {
                save.player = player;
                save.Load("Saves", "AutoSave", "playerStats");
            }
        }
    }





    public class Variables : KnightsControls.Gamepad
    {
        #region Characters
        /// <summary>
        /// Contains all of the playable characters.
        /// </summary>
        //public PlayerController[] player = { };
        public List<PlayerController> player = new List<PlayerController>();

        /// <summary>
        /// Contains all of the CPU players.
        /// </summary>
        public BotReciever[] bot = { };

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
        public List<Item> items = new List<Item>();
        public List<GameObject> stockItems = new List<GameObject>();

        /// <summary>
        /// The slot prefab to add to the inventory box.
        /// </summary>
        public GameObject slotAdd;

        /// <summary>
        /// The new item slot added.
        /// </summary>
        protected GameObject newItem;

        /// <summary>
        /// The names of all the available items.
        /// </summary>
        public string[] itemNames = { "Key", "Potion" };

        /// <summary>
        /// The amount of an item is stored here.
        /// </summary>
        public int[] itemAmount = new int[2];
        #endregion
        #region To Be Scrapped
        /// <summary>
        /// How much space you have left in your inventory. (Becoming Redundant)
        /// </summary>
        protected int space = 10;
        #endregion
        #endregion
    }
}