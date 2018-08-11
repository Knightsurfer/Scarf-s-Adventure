using UnityEngine;

public class Unit_Stats : Interactable {

    [HideInInspector] public UnitInfo unit;

    [HideInInspector] public int HP = 0;
    [HideInInspector] public int MP = 0;
    [HideInInspector] public int EXP = 10;

    [HideInInspector] public int level = 0;




    public Sprite portrait = null;

    [HideInInspector] public string unitName = "";


    public int baseHP = 10;
    public int baseMP = 10;
    public int baseEXP = 10;

    [HideInInspector] public int maxHP = 0;
    [HideInInspector] public int maxMP = 0;
    [HideInInspector] public int maxEXP = 0;

    public event System.Action<int, int> OnHealthChanged;



    protected void Assign_Stats()
    {
        unitName = name;



        maxHP = baseHP;
        maxMP = baseMP;
        maxEXP = baseEXP;

        HP = maxHP;
        MP = maxMP;
    }


    protected void Scriptable()
    {
        unitName = unit.unitName;
        portrait = unit.unitPortrait;

        maxHP = unit.default_HP;
        maxMP = unit.default_MP;
    }






    public void Stats()
    {
        maxHP = baseHP * level;
        maxMP = baseMP * level;





        if (EXP == maxEXP)
        {
            LevelUp();
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            TakeDamage(5, 0);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            TakeDamage(0, 5);

        }
        if (Input.GetKey(KeyCode.Alpha0))
        {
            EXP++;
        }



        HP = Mathf.Clamp(HP, 0, maxHP + baseHP);
        MP = Mathf.Clamp(MP, 0, maxMP + baseHP);









    }



    void LevelUp()
    {
        level++;
        EXP = 0;



        RestoreStats(maxHP + baseHP, maxMP + baseMP);

        maxEXP = baseEXP * level;




        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHP, HP);
        }

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









}
