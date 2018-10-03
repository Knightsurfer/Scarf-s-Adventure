using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controls Player Stats
public class PlayerStats : PlayerInventory
{
    public int[] health = new int[] { };
    public int[] magic = new int[] { };
    public int[] exp = new int[] { };
    public int[] level = new int[] { };

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

    private readonly int target;
    protected float healthBar;
    protected int[] healthMax = new int[] { };
    public ThirdPerson_Mode[] player = new ThirdPerson_Mode[] { };

    protected void Stats_Start()
    {

    }

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
