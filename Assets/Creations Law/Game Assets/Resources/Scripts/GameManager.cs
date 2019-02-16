using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.IO;

public class GameManager : GameHandler.PlayerInventory
{
    private void Start()
    {
        inventoryBox = GameObject.Find("Inventory Box").transform;
        PlayerDetector();
    }

    private void Update()
    {
        GamepadUpdate();
    }


}

namespace GameHandler
{


    public class PlayerInventory : PlayerHandler
    {
        #region singleton
        protected GameObject userInterface;
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
        [HideInInspector]public string[] itemNames = { "Key", "Potion" };
        public int[] itemAmount = new int[2];
        #endregion




        public delegate void OnItemChanged();
        public OnItemChanged onItemChangedCallback;

        readonly int space = 10;

        [HideInInspector]public List<Item> items = new List<Item>();

        public GameObject slotAdd;
        GameObject newItem;
        


        public bool Add(Item item)
        {
            if (!item.isDefaultItem)
            {
                if (items.Count >= space)
                {
                    print("HOARDER");
                    return false;
                }

                int i = 0;
                foreach (Item itemContext in items)
                {
                    if (itemNames[i] != null)
                    {
                        if (item.name == itemNames[i])
                        {
                            itemAmount[i]++;
                            return true;
                        }
                        else
                        {
                            i++;
                            break;
                        }

                    }

                }
                itemAmount[i]++;
                items.Add(item);
                newItem = Instantiate(slotAdd, inventoryBox);

                int objects = 0;
                foreach (MouseMenu menuItem in GameObject.Find("Pause Menu").GetComponent<MenuChooser>().menuItems)
                {
                    if (menuItem != null)
                    {
                        objects++;
                    }
                }
                GameObject.Find("Pause Menu").GetComponent<MenuChooser>().menuItems[objects] = newItem.GetComponent<MouseMenu>();
                newItem.GetComponentInChildren<Text>().text = item.name + ": " + itemAmount[i];
                newItem.name = itemNames[i];
                
                








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

    public class PlayerHandler : LevelLoadHandler
    {
        protected void PlayerDetector()
        {
            player = FindObjectsOfType<PlayerController>();

            int index = 0;
            foreach (PlayerController pl in player)
            {
                player[index] = pl;
            }
        }
    }

    public class LevelLoadHandler : Variables
    {
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        }

        private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {

        }
    }

    public class Variables : KnightsControls.Gamepad
    {
        #region Characters
        [HideInInspector]public PlayerController[] player = { };
        [HideInInspector]public BotReciever[] bot = { };
        protected Transform inventoryBox;
        #endregion

        #region Inventory
        #endregion
    }
}