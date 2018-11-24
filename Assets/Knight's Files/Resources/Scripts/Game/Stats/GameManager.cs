using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//Handles Inventory
public class PlayerInventory : MonoBehaviour
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


//Controls Player Stats
public class PlayerStats : PlayerInventory
{
    public int[] health = new int[] { };
    public int[] magic = new int[] { };
    public int[] exp = new int[] { };
    public int[] level = new int[] { };

    readonly int target;
    protected float healthBar;
    protected int[] healthMax = new int[] { };
    public ThirdPerson_Mode[] player = new ThirdPerson_Mode[] { };



    protected void Stats_Update()
    {
        player = FindObjectsOfType<ThirdPerson_Mode>();
        if (FindObjectOfType<ThirdPerson_Mode>())
        {
            foreach (ThirdPerson_Mode c in player)
            {
                player[target].health = c.health;
                player[target].healthMax = c.healthMax;

            }
            player = FindObjectsOfType<ThirdPerson_Mode>();
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


    public void Start()
    {

    }

    void Update()
    {
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
