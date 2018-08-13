using UnityEngine;

public class Unit_Stats : Unit_Interact {

    #region Variables
    #region Misc
    
    protected Command_Panel panel;
    protected Color selected = new Color(1, 1, 1);


    
    public Unit_Info unit;
    [HideInInspector] public string unitName;
    [HideInInspector] public Sprite portrait = null;

    #endregion
    #region Stats
    [HideInInspector] public int level;
    [HideInInspector] public int HP;
    [HideInInspector] public int MP;
    [HideInInspector] public int EXP;

    [HideInInspector] public int maxHP;
    [HideInInspector] public int maxMP;
    [HideInInspector] public int maxEXP;
    #endregion
    #region Abilities
    public bool canMove;
    protected bool canAttack;
    #endregion
    #endregion

    #region Status
    protected bool isDead;
    #endregion






    public event System.Action<int, int> OnHealthChanged;

    protected void Assign_Stats()
    {
        Scriptable();
       
        maxHP = unit.default_HP;
        maxMP = unit.default_MP;
        maxEXP = unit.default_EXP;

        HP = maxHP;
        MP = maxMP;
        EXP = maxEXP;

        
    }
    protected void Stats()
    {
        maxHP = unit.default_HP * level;
        maxMP = unit.default_MP * level;

        if (EXP == maxEXP)
        {
            LevelUp();
        }
        if (Input.GetKey(KeyCode.F1))
        {
            TakeDamage(5, 0);
        }
        if (Input.GetKey(KeyCode.F2))
        {
            TakeDamage(0, 5);

        }
        if (Input.GetKey(KeyCode.F3))
        {
            if (!unit.isProp)
            {
                EXP++;
            }
        }
        HP = Mathf.Clamp(HP, 0, maxHP + unit.default_HP);
        MP = Mathf.Clamp(MP, 0, maxMP + unit.default_HP);

        Death();
    }

    void Death()
    {
        if (panel != null)
        {
            if (!unit.isProp)
            {
                if (HP <= 0)
                {
                    isDead = true;
                    panel.portrait.sprite = null;
                    selected.a = 0;
                    panel.portrait.color = selected;
                    Destroy(gameObject);
                }
            }
        }




    }
    void LevelUp()
    {
        level++;
        EXP = 0;
        RestoreStats(maxHP + unit.default_HP, maxMP + unit.default_MP);
        maxEXP = unit.default_EXP * level;
    }


    void TakeDamage(int hp,int mp)
    {
        HP -= hp;
        MP -= mp;
    }
    void RestoreStats(int hp, int mp)
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
    
    
    

    protected void Scriptable()
    {
        panel = GameObject.Find("Command Menu").GetComponent<Command_Panel>();

        level = unit.startingLevel -1;
        unitName = unit.name;
        portrait = unit.unitPortrait;

        canMove = unit.canMove;
        canAttack = unit.canAttack;
    }
    









}
