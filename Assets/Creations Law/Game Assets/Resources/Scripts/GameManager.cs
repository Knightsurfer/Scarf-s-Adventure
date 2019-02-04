    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;


//Handles Inventory
public class GameManager : LevelLoaded
{
    
    public INIFile Items = new INIFile(appData + "\\Items.ini");
    public INIFile LevelData = new INIFile(appData + "\\LevelData.ini");
    public static string[] SavesDirectory = { appData + "\\Saves\\Save0", appData + "\\Saves\\Save1" };
    public INIFile[] SavedStats = { new INIFile(SavesDirectory[0] + "\\stats.ini"), new INIFile(SavesDirectory[1] + "\\stats.ini") };



    public string mode;
    public int modeSelector;
    bool thirdPerson;

    protected float saveTimer = 60*1;
    protected float elapsed = -0.5f;


    private void Update()
    {
        GamepadUpdate();
        CountItems();
        Test_Functions();
        Stats_Update();
        Shortcuts();
        AutoSave();
        
    }
    void Shortcuts()
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
    void Test_Functions()
    {
        switch (mode)
        {
            case "ThirdPerson":
                if (!thirdPerson)
                {
                    thirdPerson = true;

                    foreach (ThirdPerson tp in FindObjectsOfType<ThirdPerson>())
                    {
                        tp.enabled = true;
                        tp.SendMessage("Start");
                    }

                    GameObject.Find("KH UI").GetComponent<Canvas>().enabled = true;
                }
                break;



        }
    }
}

public class LevelLoaded : PlayerStats
{
    protected void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Start();
    }

    void Start()
    {
        UIDisplay();
        Startup();
    }
}




public class ItemManager : StartSettings
{
    Text keyCount;

    public int keys = 0;
    public int potions;

    protected void UIDisplay()
    {
       //keyCount = GameObject.Find("Key Count").GetComponentInChildren<Text>();
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
}

public class PlayerStats : PlayerInventory
{
    public int[] health = new int[4];
    public int[] magic = new int[4];
    public int[] exp = new int[4];
    public int[] level = new int[4];

    protected int target;
    protected float healthBar;
    [HideInInspector] public int[] healthMax = new int[4];

    [HideInInspector] public string[] title = new string[] { };

    protected void Stats_Update()
    {
        player = FindObjectsOfType<ThirdPerson>();
        if (player.Length > 0)
        {
            foreach (Thirdperson_Mode c in player)
            {
                
                title[target] = c.name;
                health[target] = c.health;
                healthMax[target] = c.healthMax;
                
                target++;
            }
            target = 0;
            player = FindObjectsOfType<ThirdPerson>();
            if (player.Length > 0)
            {
                GameObject.Find("Health Meter").GetComponent<Image>().fillAmount = .050f + GetHealthPercent();
            }
        }
        StatHandler();
        
    }
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
    protected void StatHandler()
    {
        if (player.Length > 0)
        {
            healthBar = player[target].health;

            
            if (FindObjectOfType<Thirdperson_Mode>())
            {
                if (player[target].health > player[target].healthMax)
                {
                    health = healthMax;
                }

                if (healthBar < .5)
                {
                    healthBar = .5f;
                }

                if (healthBar > player[target].healthMax)
                {
                    healthBar = player[target].healthMax;
                }
            }
        }
    }
    public float GetHealthPercent()
    {
        return healthBar / player[0].healthMax;
    }
}
public class PlayerInventory : ItemManager
{
    #region singleton
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
public class StartSettings : Gamepad
{
    public ThirdPerson[] player = new ThirdPerson[] { };
    public BotReciever[] bots = new BotReciever[] { };
    public GameObject userInterface;
    protected SavingLoading saveSystem;
    public static string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Scarfs Adventure\\Data";



    protected void Startup()
    {
        
        saveSystem = GetComponent<SavingLoading>();
        player = FindObjectsOfType<ThirdPerson>();


        if (!Directory.Exists(appData))
        {
            Debug.Log("Created Directory:  " + appData);
            Directory.CreateDirectory(appData);
        }


        switch (SceneManager.GetActiveScene().name)
        {
            default:
                if (player.Length > 0)
                {
                    player[0].canMove = true;
                }
                break;

            case "DreamState":
                break;

            case "Demo Screen":
                if (userInterface)
                {
                    userInterface.SetActive(false);
                }
                break;

            case "Demo Screen 2":
                if (userInterface)
                {
                    userInterface.SetActive(false);
                }
                break;

            case "":
                if (userInterface)
                {
                    userInterface.SetActive(false);
                }
                break;
        }
        switch (SceneManager.GetActiveScene().name)
        {
            case "Gameplay Test":
                player[0].transform.localRotation = Quaternion.Euler(0, -90, 0);
                player[0].currentYaw = 88;
                break;


            case "DreamState":
                player[0].transform.localRotation = Quaternion.Euler(0, 180, 0);
                player[0].currentYaw = 210;
                break;

            case "Crystal Mines":
                player[0].transform.localRotation = Quaternion.Euler(0, 90, 0);
                player[0].currentYaw = -85;
                break;
        }
    }
    
}


