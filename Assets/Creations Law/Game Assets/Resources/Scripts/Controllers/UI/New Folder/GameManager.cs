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
        public GameObject userInterface;
        public static PlayerInventory instance;
        private void Awake()
        {
            Debug.Log("Woke");
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

        


        public delegate void OnItemChanged();
        public OnItemChanged onItemChangedCallback;

        readonly int space = 2;

        public List<Item> items = new List<Item>();


        public bool Add(Item item)
        {
            Debug.Log(item.name);
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

    public class PlayerHandler : LevelLoadHandler
    {
        protected void PlayerDetector()
        {
            player = FindObjectsOfType<PlayerController>();

            int index = 0;
            foreach(PlayerController pl in player)
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
        public PlayerController[] player = { };
        public BotReciever[] bot = { };
        #endregion

        #region Inventory
        public int keys;
        public int potions;
        #endregion
    }
}
