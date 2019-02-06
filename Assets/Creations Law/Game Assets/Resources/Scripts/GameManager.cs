using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;

public class GameManager : GameHandler.PlayerInventory
{
    

    

    private void Update()
    {
        AutoSave();
        Stats_Update();
        GamepadUpdate();


        CountItems();
        Test_Functions();
        Stats_Update();
        Shortcuts();
        AutoSave();
    }



}
namespace GameHandler
{

    public class PlayerInventory : Main
    {



        protected void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }


        #region singleton
        public GameObject userInterface;
        public static PlayerInventory instance;
        private void Awake()
        {
            if (instance != null)
            {
                return;
            }
            instance = this;
            userInterface = GameObject.Find("Main Stuff");


        }
        #endregion

        #region Items

        #endregion

        void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            Start();
        }

        private void Start()
        {
            player = FindObjectsOfType<PlayerController>();
        }


        public delegate void OnItemChanged();
        public OnItemChanged onItemChangedCallback;

        readonly int space = 2;

        public List<Item> items = new List<Item>();


        public bool Add(Item item)
        {
            if (!item.isDefaultItem)
            {
                if (items.Count >= space)
                {
                    print("HOARDER");
                    return false;
                }

                switch (item.name)
                {
                    default:
                        items.Add(item);
                        break;
                    case "Key":
                        keys++;
                        break;

                    case "Potion":
                        potions++;
                        break;
                }
                if (onItemChangedCallback != null)
                {
                    onItemChangedCallback.Invoke();
                }
            }

            return true;
        }
        public void Remove(Item item)
        {
            items.Remove(item);

        }
    }


    public class Main : Gamepad
    {
        #region Saving
        protected float saveTimer = 60 * 1;
        protected float elapsed = -0.5f;

        protected void AutoSave()
        {
            if (SavingLoading.Instance.player != null)
            {
                elapsed += Time.deltaTime;
                if (elapsed >= saveTimer)
                {
                    Debug.Log("Game has been saved.");
                    SavingLoading.Instance.gameData.saveName = "Autosave";
                    SavingLoading.Instance.SaveGame();
                    elapsed = -0.5f;
                }
            }
        }
        #endregion

        #region Stats
        public PlayerController[] player = new PlayerController[] { };

        public string[] characterNames;
        public int[] health;
        public int[] healthMax;
        public int[] magic;
        public int[] exp;
        public int[] level;

        protected void Stats_Update()
        {
            player = FindObjectsOfType<PlayerController>();
            if (player.Length > 0)
            {
                int target = 0;
                foreach (PlayerController c in player)
                {
                    characterNames[target] = c.name;
                    health[target] = c.health;
                    healthMax[target] = c.healthMax;

                    target++;
                }
                target = 0;
                player = FindObjectsOfType<PlayerController>();

            }
        }
        #endregion



        #region Level Load
        public INIFile Items = new INIFile(appData + "\\Items.ini");
        public INIFile LevelData = new INIFile(appData + "\\LevelData.ini");
        public static string[] SavesDirectory = { appData + "\\Saves\\Save0", appData + "\\Saves\\Save1" };
        public INIFile[] SavedStats = { new INIFile(SavesDirectory[0] + "\\stats.ini"), new INIFile(SavesDirectory[1] + "\\stats.ini") };


       
        public static string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Scarfs Adventure\\Data";


        public string mode;
        public int modeSelector;
        bool thirdPerson;

        protected float healthBar;


        private void Update()
        {
            

        }
       protected void Shortcuts()
        {
            if (Input.GetKey(KeyCode.F3))
            {
                Recovery(0, "Heal", 1);

            }
            if (Input.GetKey(KeyCode.F4))
            {
                Recovery(0, "Damage", 1);
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                if (SavingLoading.Instance.player != null)
                {
                    Debug.Log("Game has been saved.");
                    SavingLoading.Instance.gameData.saveName = "Save 1";
                    SavingLoading.Instance.SaveGame();
                }
            }
            if (Input.GetKeyDown(KeyCode.F7))
            {
                if (SavingLoading.Instance.player != null)
                {
                    SavingLoading.Instance.LoadGame();
                }

            }
        }
       
       protected void Test_Functions()
        {
            UIDisplay();
            CountItems();
            switch (mode)
            {
                case "ThirdPerson":
                    if (!thirdPerson)
                    {
                        thirdPerson = true;

                        foreach (PlayerController tp in player)
                        {
                            tp.enabled = true;
                            tp.SendMessage("Start");
                        }

                        GameObject.Find("KH UI").GetComponent<Canvas>().enabled = true;
                    }
                    break;

                    

            }
        }

        public Text keyCount;

        public int keys = 0;
        public int potions;

        protected void UIDisplay()
        {
            keyCount = GameObject.Find("Key Count").GetComponentInChildren<Text>();
            //Debug.Log(keys.ToString());
        }

        protected void CountItems()
        {
            if (keyCount != null)
            {
                if (keys < 1)
                {
                    keyCount.color = new Color(0.500f, 0.469f, 0.469f);
                    keys = 0;
                }
                if (keys > 98)
                {
                    keys = 99;
                }
                if (keys > 0)
                {
                    keyCount.color = new Color(250, 250, 250);
                }
                if (keys < 10)
                {
                    keyCount.text = "0" + keys;
                }
                if (keys >= 10)
                {
                    keyCount.text = keys.ToString();
                }
            }
        }
    



    #endregion


    protected void Recovery(int target, string action, int points)
        {
            switch (action)
            {
                case "Heal":
                    player[target].health += points;
                    healthBar -= points;
                    break;

                case "Damage":
                    player[target].health -= points;
                    healthBar += points;
                    break;

                case "Die":
                    player[target].health -= player[target].health;
                    break;


            }
            if (player[target].health < 0)
            {
                player[target].health = 0;
            }
            if (player[target].health > player[target].healthMax)
            {
                player[target].health = player[target].healthMax;
            }
        }






    }









}