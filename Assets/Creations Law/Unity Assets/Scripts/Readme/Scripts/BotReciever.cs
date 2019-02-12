using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


//The Main Startup Class. (Nothing is really required from this class.)
public class BotReciever : Bot_Controller
{
    private void Start()
    {
        AddComponents();
        if (!positionSet)
        {
            pointZero = points[0];
            positionSet = true;
        }
        if (isPartyMember)
        {
            points[0] = pointZero;
            playerIsPresent = true;
        }
        PositionSet();
    }
    private void Update()
    {
        BotUpdate();
    }
}

public class Bot_Controller : Bot_Destinations
{
    protected void BotUpdate()
    {
        canMove = !pause.paused;
        bot.enabled = canMove;
        botAI.enabled = canMove;


        if (isPartyMember)
        {
            playerIsPresent = true;
        }

        if (canMove)
        {
            Movement();
            Animation();
            PositionSet();
        }
    }
    protected void Movement()
    {
        distance = Vector3.Distance(new Vector3(target.position.x, transform.position.y, target.position.z), transform.position);
        if (distance < range && distance > 3 && WaypointPassed == 100)
        {

        }
        if (isPartyMember)
        {
            transform.LookAt(target.position);
        }
        moveDirection = transform.TransformDirection(Vector3.forward);
        moveDirection.y -= 1f;

        if (bot.enabled || botAI.enabled)
        {
            if (distance > 2 && WaypointPassed != 100)
            {
                botAI.enabled = true;
                bot.enabled = false;

                botAI.SetDestination(target.position);
                Patrol();
            }
        }
        if (distance < range - 3 && distance > 2 && WaypointPassed == 100)
        {
            botAI.enabled = false;
            bot.enabled = true;

            bot.Move(moveDirection * Time.deltaTime * speed);
        }
        else
        {
            if (bot.enabled)
            {
                bot.Move(moveDirection * Time.deltaTime * 0);
            }
        }
    }
    protected void Animation()
    {
        if (playerIsPresent)
        {
            if (canMove && distance > 2)
            {
                anim.SetFloat("Speed", Mathf.Abs(1) + Mathf.Abs(1));
            }
            if (distance < 2)
            {
                anim.SetFloat("Speed", 0);
            }
        }
    }
}
public class Bot_Destinations : Bot_Components
{
    private void Follow()
    {
        if (prey == null && distance < 3)
        {
            WaypointPassed = 1;
        }
    }
    protected void Patrol()
    {
        if (prey != null)
        {
            WaypointPassed = 100;
            return;
        }
        if (distance < 3)
        {
            switch (WaypointPassed)
            {
                default:
                    if (!isPartyMember)
                    {
                        target = GameObject.Find("Point" + WaypointPassed).transform;
                        range = 10;
                        if (WaypointPassed == points.Length - 1)
                        {
                            WaypointPassed = 0;
                            Debug.Log("Waypoint Reset.");
                            return;
                        }
                        WaypointPassed++;

                        Debug.Log("Waypoint " + WaypointPassed + " Passed");
                    }
                    else
                    {
                        range = 100;
                        target = FindObjectOfType<PlayerController>().transform;
                    }
                    break;

                case 100:
                    Follow();
                    break;
            }
        }
    }
    protected void PositionSet()
    {
        if (!GameObject.Find("Point0"))
        {
            foreach (Vector3 p in points)
            {
                waypoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
                waypoint.GetComponent<MeshRenderer>().enabled = false;
                waypoint.GetComponent<Collider>().enabled = false;
                waypoint.name = "Point" + createdNo;
                waypoint.transform.parent = GameObject.Find("Waypoints").transform;
                waypoint.transform.localPosition = p;
                createdNo++;
            }
        }
        if (isPartyMember)
        {
            target = FindObjectOfType<PlayerController>().transform;
        }
        else
        {
            target = GameObject.Find("Point" + WaypointPassed).transform;
        }
    }
    private void ResetPositions()
    {
        if (isPartyMember)
        {
            points[0] = Vector3.zero;
            WaypointPassed = 0;
        }
        else
        {
            points[0] = pointZero;
            WaypointPassed = 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Interactable>())
        {
            Interactable interaction = other.GetComponentInParent<Interactable>();
            switch (interaction.type)
            {
                case "Door":
                    focus = interaction;
                    interaction.hasInteracted = true;
                    interaction.botOverride = true;
                    break;
            }
        }
        if (other.GetComponent<PlayerController>())
        {
            prey = other.GetComponent<PlayerController>();
            target = prey.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Interactable>())
        {
            Interactable interact = other.GetComponentInParent<Interactable>();
            switch (interact.type)
            {
                case "Door":
                    interact.hasInteracted = false;
                    interact.botOverride = false;
                    if (!interact.locked)
                    {
                        if (interact.anim.GetBool("Approached"))
                        {
                            other.transform.parent.SendMessage("Door");
                        }
                    }
                    focus = null;
                    break;
            }
        }
        if (other.GetComponent<PlayerController>())
        {
            prey = null;
            WaypointPassed = 1;
            if (!isPartyMember)
            {
                target = GameObject.Find("Point0").transform;
            }
        }
    }
}
public class Bot_Components : Bot_Scripts
{
    #region Variables
    //Own Object
    public Animator anim;
    public Transform skeleton;
    public CharacterController bot;
    public NavMeshAgent botAI;

    //Other Objects
    public Transform target;
    protected GameObject waypoint;
    #endregion
    protected void AddComponents()
    {
        #region NavMesh Agent
        if (GetComponent<NavMeshAgent>())
        {
            botAI = gameObject.GetComponent<NavMeshAgent>();
        }
        else
        {
            botAI = gameObject.AddComponent<NavMeshAgent>();
        }
        #endregion 
        #region Character Controller
        if (GetComponent<CharacterController>())
        {
            bot = GetComponent<CharacterController>();
        }
        else
        {
            bot = gameObject.AddComponent<CharacterController>();
        }
        #endregion
        SetComponents();
    }
    private void SetComponents()
    {
        #region Other
        anim = GetComponent<Animator>();
        pause = FindObjectOfType<MenuChooser>();
        #endregion
        #region NavMesh Agent
        botAI.agentTypeID = 0;
        botAI.speed = 7f;
        botAI.angularSpeed = 12000;
        botAI.acceleration = 20;
        botAI.baseOffset = -0.2f;
        botAI.enabled = false;
        botAI.autoTraverseOffMeshLink = false;
        #endregion
        #region Character Controller
        bot.height = 2f;
        bot.radius = 0.5f;
        bot.center = new Vector3(0, 1.1f, 0);
        #endregion

        skeleton = GetComponentInChildren<SkinnedMeshRenderer>().transform;
    }
}
public class Bot_Scripts : Bot_Variables
{
    protected MenuChooser pause;
    protected PlayerController prey;
    protected Interactable focus;
}
public class Bot_Variables : MonoBehaviour
{
    #region Misc
    protected bool canMove;
    public bool playerIsPresent;
    public bool isPartyMember = true;

    [HideInInspector] public int healthMax;
    [HideInInspector] public int magicMax;
    [HideInInspector] protected int exp;


    public int level;
    public int magic;
    public int health;


    #endregion
    #region Waypoints
    public float distance;
    public float range = 10;
    public Vector3[] points;
    protected int createdNo;
    public int WaypointPassed = 0;
    #endregion
    #region Controller Variables
    protected float speed = 8.5f;
    protected float rotator = 150;
    protected Vector3 moveDirection;
    protected Quaternion theRotation;
    protected float player_rotateSpeed = 10;

    protected Vector3 pointZero;
    protected bool positionSet;
    #endregion
}





