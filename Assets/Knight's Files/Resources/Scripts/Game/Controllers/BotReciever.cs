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
        StartController();
        PositionSet();
    }

    private void Update()
    {
        BotUpdate();
    }


}


public class Bot_Controller : Bot_Components
{
    protected float range = 30;
    protected float speed = 7;
    
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


    protected void Movement()
    {
        distance = Vector3.Distance(target.position, transform.position);
        transform.LookAt(target);
        Vector3 moveDirection = transform.TransformDirection(Vector3.forward);

        if (bot.enabled || botAI.enabled)
        {

            if (distance > 2 && WaypointPassed != 100)
            {
                botAI.enabled = true;
                bot.enabled = false;

                botAI.SetDestination(target.position);

                //bot.Move(moveDirection * Time.deltaTime * speed);
                Patrol();
            }



            if (distance < range && distance > 2 && WaypointPassed == 100)
            {
                botAI.enabled = false;
                bot.enabled = true;

                bot.Move(moveDirection * Time.deltaTime * speed);
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
                    target = GameObject.Find("Point" + WaypointPassed).transform;
                    if (WaypointPassed == points.Length-1)
                    {
                        WaypointPassed = 0;
                        return;
                    }
                    WaypointPassed++;
                    break;
/*
                case 1:
                    target = GameObject.Find("Point0").transform;
                    WaypointPassed = 0;
                    break;

                case 0:
                    
                    target = GameObject.Find("Point1").transform;
                    WaypointPassed = 1;
                    break;
                    */

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
        if(distance < 2 || distance > 30)
        {
            anim.SetFloat("Speed", 0);
        }
    }

}


public class Bot_Components : MonoBehaviour
{
    protected Animator anim;
    protected PauseMenu pause;
    protected CharacterController bot;
    protected NavMeshAgent botAI;
    public Transform target;
    public Interactable focus;

    protected Vector3 startPosition;
    protected Vector3 pointA;
    protected Vector3 pointB;
    public Vector3[] points;
    protected GameObject createdCube;

    protected int createdNo;
    public int WaypointPassed = 1;



    protected bool canMove;


    protected void PositionSet()
    {
        startPosition = transform.position;

        foreach(Vector3 p in points)
        {
           createdCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            createdCube.GetComponent<MeshRenderer>().enabled = false;
            createdCube.name = "Point" + createdNo;
            createdCube.transform.parent = GameObject.Find("Waypoints").transform;
            createdCube.transform.localPosition = p;
            createdNo++;
        }
        


        target = GameObject.Find("Point0").transform;

        
    }





    CapsuleCollider capu;
    BoxCollider box;

    void detection()
    {
        if (GetComponent<CapsuleCollider>() .GetComponent<Interactable>())
        {

        }
               
    }




    protected void StartController()
    {
        anim = GetComponent<Animator>();
        pause = FindObjectOfType<PauseMenu>();

        
        bot = gameObject.AddComponent<CharacterController>();
        bot.height = 2;
        bot.radius = 0.5f;
        bot.center = new Vector3(0, 1.1f, 0);

        
        botAI = gameObject.AddComponent<NavMeshAgent>();
        botAI.agentTypeID = 0;
        botAI.speed = 7;
        botAI.angularSpeed = 12000;
        botAI.acceleration = 20;
        botAI.enabled = false;
        
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
    protected ThirdPerson prey;






    protected void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Interactable>())
        {
            Interactable interact = other.GetComponentInParent<Interactable>();
            switch (interact.type)
            {
                case "Door":
                    
                    focus.hasInteracted = false;
                    focus.botOverride = false;
                    
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
            target = GameObject.Find("Point0").transform;
            
        }
   
    }


}

