using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class RTS_Unit : Unit_Controller
{

}

public class Unit_Controller : Unit_Stats
{
    protected Unit_Controller focus;
    protected Transform target;

    protected void Start()
    {
        Stats_Start();
        Radius_Start();

        nav = GetComponent<NavMeshAgent>();
    }
    protected void Update()
    {
        Stats_Update();
        if (canMove)
        {
            Animation(null);
            if (canMove)
            {
                Follow();
            }
        }
    }

    #region RTS Mode
    #region Movement
    public void Movement(Vector3 point)
    {
        RemoveFocus();
        if (canMove)
        {

            nav = GetComponent<NavMeshAgent>();
            nav.SetDestination(point);
        }
    }
    protected void Animation(string action)
    {
        Animator anim = GetComponent<Animator>();
        switch (action)
        {
            case null:
                if (nav != null)
                {

                    if ((int)nav.remainingDistance == nav.stoppingDistance || target != null && (int)nav.remainingDistance == nav.stoppingDistance + 1)
                    {
                        nav.isStopped = true;
                        anim.SetFloat("Speed", 0);
                    }
                    else
                    {
                        nav.isStopped = false;
                        anim.SetFloat("Speed", nav.remainingDistance);
                    }
                }
                break;


            case "work":
                anim.SetBool("Working", true);
                break;
        }
    }
    #endregion
    #region Unit Actions
    #region Following
    public void SetFocus(Unit_Controller newFocus)
    {
        if (newFocus.stats.isProp)
        {
            newFocus.GetComponent<NavMeshObstacle>().carving = false;
        }
        if (canMove)
        {
            focus = newFocus;
            StartFollow(newFocus);
        }
    }
    public void RemoveFocus()
    {
        if (focus != null)
        {
            if (focus.stats.isProp)
            {
                focus.GetComponent<NavMeshObstacle>().carving = true;
            }
            focus = null;
            StopFollow();
        }
    }
    protected void Follow()
    {
        if (target != null)
        {
            nav.SetDestination(target.position);
            if (focus.stats.isProp)
            {

                DestinationReached();
            }
        }
    }
    protected void StartFollow(Unit_Controller newTarget)
    {
        target = newTarget.transform;
        if (focus.stats.isProp)
            nav.stoppingDistance = 1;
        else
            nav.stoppingDistance = 2;
    }
    protected void StopFollow()
    {
        if (nav != null)
        {
            nav.stoppingDistance = 0;
            target = null;
        }
    }
    #endregion
    #endregion
    protected void DestinationReached()
    {
        if (focus.stats.isProp && (int)nav.remainingDistance == nav.stoppingDistance + 1)
        {
            Destroy(focus.gameObject);
            nav.isStopped = true;
            RemoveFocus();
            Animation("work");
        }
    }
    #endregion
}

public class Unit_Stats : Unit_Interact
{

    #region Variables
    #region Main

    protected RTS_Panel panel;
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
    [HideInInspector] public bool Dead = false;
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
        panel = GameObject.Find("Command Menu").GetComponent<RTS_Panel>();

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
            maxHP = stats.default_HP * level;
            maxMP = stats.default_MP * level;
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

public class Unit_Interact : MonoBehaviour
{
    public Unit_Info stats;

    [HideInInspector] public float radius = 1;
    [HideInInspector] public NavMeshAgent nav;


    protected void Radius_Start()
    {
        if (!GetComponent<Unit_Controller>().stats.isProp)
        {
            gameObject.AddComponent<Rigidbody>();
            gameObject.GetComponent<Rigidbody>().isKinematic = true;

            gameObject.AddComponent<NavMeshAgent>();
            nav = GetComponent<NavMeshAgent>();
            nav.speed = 7;
            nav.baseOffset = 0;
            nav.height = 2;
            nav.radius = 1;
            nav.angularSpeed = 1200;
            nav.acceleration = 20;
            nav.areaMask = 5;
            nav.autoBraking = false;
        }
        if (stats.isProp)
        {
            gameObject.AddComponent<NavMeshObstacle>();
            gameObject.GetComponent<NavMeshObstacle>().carving = true;
            gameObject.AddComponent<BoxCollider>();
        }
        else
        {
            gameObject.AddComponent<CapsuleCollider>();
            gameObject.GetComponent<CapsuleCollider>().height = 2.5f;
            gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0, 1, 0);
        }

    }
}
