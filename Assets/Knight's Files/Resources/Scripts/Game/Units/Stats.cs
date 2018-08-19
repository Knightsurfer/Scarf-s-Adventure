using UnityEngine;
using UnityEngine.UI;

public class Unit_Stats : Unit_Interact {

    #region Variables
    #region Main
    public Unit_Info stats;
    protected Command_Panel panel;
    protected Color selected = new Color(1, 1, 1);
    #endregion
    #region Attack Stuff
    protected float attackRefreshRate;
    protected float attackTimer;
    #endregion
    #endregion
    #region Remaining Stats
    [HideInInspector] public string unitName;
    [HideInInspector] public Sprite portrait = null;
    #region Stats

    public int HP;
    public int MP;
    

    public int level;

    [HideInInspector] public int EXP;
    [HideInInspector] public int maxHP;
    [HideInInspector] public int maxMP;
    [HideInInspector] public int maxEXP;

    #endregion
    #region Abilities
    protected bool canMove;
    #endregion
    #region Status
    [HideInInspector]public bool Dead = false;
    #endregion





    #endregion
    //public event System.Action<int, int> OnHealthChanged;







    protected void Stats_Start()
    {
        BaseStats();

        maxHP = stats.default_HP;
        maxMP = stats.default_MP;
        maxEXP = stats.default_EXP;

        HP = maxHP;
        MP = maxMP;
        EXP = maxEXP;
    }
    protected void BaseStats()
    {
        panel = GameObject.Find("Command Menu").GetComponent<Command_Panel>();

        level = stats.startingLevel - 1;
        unitName = stats.name;
        portrait = stats.unitPortrait;
        canMove = !stats.isProp;
    }








    protected void Stats_Update()
    {
            Stats(); 
    }

    #region Starting Stats
    
    
    #endregion
    #region Status Changes
    protected void Stats()
    {
        Death();
        maxHP = stats.default_HP * level;
        maxMP = stats.default_MP * level;
        if (EXP == maxEXP)
        {
            LevelUp();
        }
        HP = Mathf.Clamp(HP, 0, maxHP + stats.default_HP);
        MP = Mathf.Clamp(MP, 0, maxMP + stats.default_HP);

    }

    public void TakeDamage(int hp, int mp)
    {
        if (HP > 0)
        {
            HP -= hp;
        }
        if (MP > 0)
        {
            MP -= mp;
        }
            
    }
    protected void RestoreStats(int hp, int mp)
    {
        for (int i = HP; i <= hp; i++)
        {
            HP = i;
        }
        for (int i = MP; i <= mp; i++)
        {
            MP = i;
        }
    }
    protected void LevelUp()
    {
        if (!stats.isProp)
        {
            level++;
            EXP = 0;
            RestoreStats(stats.default_HP * level, stats.default_MP * level);
            maxEXP = stats.default_EXP * level;
        }
    }
    protected void Death()
    {
        if (panel != null)
        {
            if (!stats.isProp)
            {
                if (HP <= 0)
                {
                    Dead = true;
                    canMove = false;
                    panel.portrait.sprite = null;
                    selected.a = 0;
                    panel.portrait.color = selected;
                    Destroy(gameObject);
                    
                }
            }
        }
    }

    public bool CanAttack()
    {
        return attackTimer >= attackRefreshRate;
    }
    #endregion

}