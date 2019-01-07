using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


//The Main Startup Class. (Nothing is really required from this class.)
public class BotReciever : Bot_Controller
{
     public Vector3 pointZero;
     bool positionSet;

    
    private void Start()
    {
        StartController();
        if(!positionSet)
        {
            pointZero = points[0];
            positionSet = true;
        }
        if (isPartyMember)
        {
            points[0] = pointZero;
        }
        PositionSet();
        
    }
    private void Update()
    {
        BotUpdate();
    }
    
    private void Setpositions()
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
}


public class Bot_Controller : Bot_Components
{
    public float range = 10;
    protected float speed = 8.5f;
    
    public float distance;

    protected void BotUpdate()
    {
        canMove = !pause.paused;
        bot.enabled = canMove;
        botAI.enabled = canMove;

        
        if (canMove)
        {
            Movement();
            Animation();
        }
    }

    Vector3 moveDirection;
    protected void Movement()
    {
        distance = Vector3.Distance(new Vector3(target.position.x, transform.position.y, target.position.z), transform.position);
        if (distance < range && distance > 3 && WaypointPassed == 100)
        {
            transform.LookAt(target.position);
        }
        
        if(isPartyMember)
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
                        return;
                    }
                        WaypointPassed++;
                    }
                    else
                    {
                        range = 100;
                        target = FindObjectOfType<ThirdPerson>().transform;
                    }

                    break;

                case 100:
                    Follow();
                    break;


            }
        }

    }

    protected void Follow()
    {
        if (prey == null && distance < 3)
        {
            WaypointPassed = 1;
        }
    }
    protected void Animation()
    {
        if (canMove && distance > 2)
        {
            anim.SetFloat("Speed", Mathf.Abs(1) + Mathf.Abs(1));
        }
        if(distance < 2 || distance > range)
        {
            anim.SetFloat("Speed", 0);
        }
    }

}


public class Bot_Components : MonoBehaviour
{
    public bool isPartyMember = true;

    public Animator anim;
    protected PauseMenu pause;
    public CharacterController bot;
    public NavMeshAgent botAI;
    public Transform target;
    public Interactable focus;

    protected Vector3 startPosition;
    protected Vector3 pointA;
    protected Vector3 pointB;
    public Vector3[] points;
    protected GameObject createdCube;

    protected int createdNo;
    public int WaypointPassed = 0;
    protected bool canMove;
    protected ThirdPerson prey;

    protected void PositionSet()
    {
        startPosition = transform.position;
        if (!GameObject.Find("Point0"))
        {
            foreach (Vector3 p in points)
            {
                createdCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                createdCube.GetComponent<MeshRenderer>().enabled = false;
                createdCube.GetComponent<Collider>().enabled = false;
                createdCube.name = "Point" + createdNo;
                createdCube.transform.parent = GameObject.Find("Waypoints").transform;
                createdCube.transform.localPosition = p;
                createdNo++;
            }
        }

        if (!isPartyMember)
        {
            target = GameObject.Find("Point0").transform;
        }
        else
        {
            target = FindObjectOfType<ThirdPerson>().transform;
        }
    }












    protected void StartController()
    {
        anim = GetComponent<Animator>();
        pause = FindObjectOfType<PauseMenu>();

        if (!GetComponent<CharacterController>())
        {
            bot = gameObject.AddComponent<CharacterController>();
        }
        else
        {
            bot = GetComponent<CharacterController>();
        }

        bot.height = 2f;
        bot.radius = 0.5f;
        bot.center = new Vector3(0, 1.1f, 0);

        if (!GetComponent<NavMeshAgent>())
        {
            botAI = gameObject.AddComponent<NavMeshAgent>();
        }
        else
        {
            botAI = gameObject.GetComponent<NavMeshAgent>();
        }

        botAI.agentTypeID = 0;
        botAI.speed = 7f;
        botAI.angularSpeed = 12000;
        botAI.acceleration = 20;
        botAI.baseOffset = -0.2f;
        botAI.enabled = false;
        botAI.autoTraverseOffMeshLink = false;
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

        if (other.GetComponent<ThirdPerson>())
        {
            prey = other.GetComponent<ThirdPerson>();
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

        if (other.GetComponent<ThirdPerson>())
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

