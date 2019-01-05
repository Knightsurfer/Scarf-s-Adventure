﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//Handles Inventory
public class PlayerInventory : StartSettings
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
            items.Add(item);
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

    void Start()
    {
        player = FindObjectsOfType<ThirdPerson>();

        switch ((SceneManager.GetActiveScene().buildIndex))
        {
            default:
                player[0].canMove = true;
                break;
            case 5:
                break;

            case 3:
                break;


        }

        switch ((SceneManager.GetActiveScene().buildIndex))
        {
            case 2:
                player[0].transform.localPosition = new Vector3(85, 0, -4.555f);
                player[0].transform.localRotation = Quaternion.Euler(0, -90, 0);
                player[0].currentYaw = 88;
                break;


            case 3:
                player[0].transform.localPosition = new Vector3(-0.7f, 0, 64);
                player[0].transform.localRotation = Quaternion.Euler(0, 180, 0);
                player[0].currentYaw = 210;
                break;

            case 4:
                player[0].transform.localPosition = new Vector3(0, 0, 64);
                player[0].transform.localRotation = Quaternion.Euler(0, 90, 0);
                
                player[0].currentYaw = -85;
                break;
        }
    }

}


//Controls Player Stats
public class PlayerStats : PlayerInventory
{
    public int[] health = new int[4];
    public int[] magic = new int[4];
    public int[] exp = new int[4];
    public int[] level = new int[4];

    protected int target;
    protected float healthBar;
    public int[] healthMax = new int[4];

    public string[] title = new string[] { };

    protected void Stats_Update()
    {
        player = FindObjectsOfType<ThirdPerson>();
        if (player.Length > 0)
        {
            foreach (ThirdPerson_Mode c in player)
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
        
        
        if (FindObjectOfType<ThirdPerson_Mode>())
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
    public float GetHealthPercent()
    {
        return healthBar / player[0].healthMax;
    }
}

public class GameManager : PlayerStats
{
    public string mode;
    public int modeSelector;
    bool thirdPerson;
    
    private void Update()
    {
        GamepadUpdate();
        Test_Functions();
        Stats_Update();
        Shortcuts();
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
