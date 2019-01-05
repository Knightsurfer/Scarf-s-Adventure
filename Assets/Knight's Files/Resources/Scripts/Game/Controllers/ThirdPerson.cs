using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//The Main Startup Class. (Nothing is really required from this class.)
public class ThirdPerson : ThirdPerson_Mode
{
    private void Start()
    {
       
        StartingVariables();
        InteractStart();
        ControllerStart();
        Components();
        
    }
    private void Update()
    {
        if (!canMove)
        {
            if (GameObject.Find("Scarf").GetComponent<ThirdPerson_Mode>().currentYaw != 0)
            {
                GameObject.Find("Scarf").GetComponent<ThirdPerson_Mode>().currentYaw -= 1 * 5;
            }
            else
            {
                elapsed += Time.deltaTime;
                if (!canMove)
                {
                    if (elapsed >= timerspeed)
                    {

                        GameObject.Find("KH UI").GetComponent<Canvas>().enabled = true;
                        FindObjectOfType<Command_Controller>().enabled = true;
                        FindObjectOfType<PauseMenu>().enabled = true;

                        canMove = true;
                    }
                }
            }
        }



        InteractUpdate();
        ControllerUpdate();
    }
}

//Camera Control.
public class ThirdPerson_Mode : ThirdPerson_Stats
{
    #region Variables
    #region Main
    private float horizontal;
    #endregion
    #region First Person
    Transform lookObject;
    #endregion
    #region Third Person
    protected Vector3 offset;
    protected float cam_rotateSpeed_X = 180;
    protected float cam_rotateSpeed_Y = 80;
    public float currentYaw = 210f; //210
    protected float currentZoom = 2f;
    #endregion
    #endregion
    

    protected float timerspeed = 0f;
    protected float elapsed = -0.5f;


    private void LateUpdate()
    {
        horizontal = gamepad.cameraX * cam_rotateSpeed_X * Time.deltaTime;

        
        rotator =  currentYaw+150;
        if (canMove)
        {
            currentYaw += gamepad.cameraX * cam_rotateSpeed_X * Time.deltaTime;
        }
        switch (viewType)
        {
            case "FirstPerson":
                if (gamepad.button_Select)
                {
                    cam.parent = GameObject.Find("Player 1").transform;
                    viewType = "ThirdPerson";
                    cam.localPosition = new Vector3(44.453f, 2.56f, 33.1f);
                }
                FirstPerson();
                break;

            case "ThirdPerson":
                if (gamepad.button_Select)
                {
                    cam.parent = lookObject.transform;
                    viewType = "FirstPerson";
                }
                ThirdPerson();
                break;
        }

    }
    protected void Components()
    {
        #region Getting Variables
        //rotator = GameObject.Find("Rotator").transform;
        lookObject = GameObject.Find("Look Object").transform;
        skeleton = GetComponentInChildren<SkinnedMeshRenderer>().transform;
        #endregion

        offset = transform.position - cam.position;
        //rotator.position = transform.position;
    }
    private void FirstPerson()
    {
        float vertical = gamepad.cameraX * cam_rotateSpeed_Y * 2 * Time.deltaTime;
        float horizontal = -gamepad.cameraY * cam_rotateSpeed_Y * 2 * Time.deltaTime;

        cam.localRotation = Quaternion.Euler(cam.localRotation.x, lookObject.localRotation.y, cam.localRotation.z);
        lookObject.Rotate(horizontal, 0, 0);

        transform.Rotate(0, vertical, 0);
        cam.Rotate(0, horizontal, 0);
        cam.localPosition = Vector3.zero;
    }
    private void ThirdPerson()
    {
        currentZoom -= gamepad.cameraY * 3.5f * Time.deltaTime;

        currentZoom = Mathf.Clamp(currentZoom, 2f, 4f);
        cam.transform.position = player.transform.position - new Vector3(0, -1, -2) * currentZoom;
        cam.transform.LookAt(player.transform.position + Vector3.up * 2f);

        cam.transform.RotateAround(player.transform.position, Vector3.up, currentYaw);
    }
}

//Storage for individual stat values.
public class ThirdPerson_Stats : ThirdPerson_Start
{

   [HideInInspector] public int healthMax;
   [HideInInspector] public int magicMax;
   [HideInInspector] protected int exp;


   [HideInInspector] public int level;
   [HideInInspector] public int magic;
   [HideInInspector] public int health;
}

//Controls the starting position of the player.
public class ThirdPerson_Start : ThirdPerson_Interact
{

    GameObject playerModel;


    protected void StartingVariables()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        




        playerModel = GameObject.FindGameObjectWithTag("Player");

        switch (currentScene)
        {

            case "Gameplay Test":
                //playerModel.transform.localPosition = new Vector3(51, 0, 34);
                break;

                /*
            case 5:
                playerModel.transform.localPosition = new Vector3(85, 0, -12);
                break;
                */
        }


    }


}

//Handles Interaction.
public class ThirdPerson_Interact : ThirdPersonController
{
    public Interactable focus;

    protected GameObject buttonPrompt;
    protected Collider chest;

    private readonly bool itemInteracted;

    protected void InteractStart()
    {
        buttonPrompt = GameObject.Find("Prompt");

        for (int i = 0; i < FindObjectsOfType<Interactable>().Length; i++)
        {
            if (FindObjectsOfType<Interactable>()[i].type == "Chest")
            {
                FindObjectsOfType<Interactable>()[i].SendMessage("Chest", FindObjectsOfType<Interactable>()[i].obtained);
                FindObjectsOfType<Interactable>()[i].GetComponents<BoxCollider>()[1].enabled = !FindObjectsOfType<Interactable>()[i].obtained;
            }
        }




    }
    protected void InteractUpdate()
    {
        ObjectBehaviour();
        LevelBounds();
    }

    protected void ButtonPrompt()
    {
        //buttonPrompt.transform.LookAt(cam.transform.position);
    }
    protected void LevelBounds()
    {
        if (transform.localPosition.y < -15)
        {
            transform.localPosition = new Vector3(47, 1, 38);
        }
    }
    protected void ObjectBehaviour()
    {
        if (chest != null)
        {
            if (gamepad.button_Action)
            {
                chest.SendMessage("Chest", true);
                anim.SetBool("Working", !anim.GetBool("Working"));
            }
            if (chest.GetComponent<Animator>().GetBool("Open") == true)
            {
                if (gamepad.button_Attack)
                {
                    chest.SendMessage("Chest", false);
                }
            }
        }
    }

   
    protected void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Interactable>())
        {
            Interactable interaction = other.GetComponentInParent<Interactable>();
            switch (interaction.type)
            {
                case "Item":
                    if (interaction != null)
                    {
                        SetFocus(interaction);
                        other.transform.parent.SendMessage("Item");
                    }
                    break;

                case "Chest":
                    interaction.hasInteracted = true;

                    //chest = other;
                    break;

                case "Door":
                    interaction.hasInteracted = true;
                    break;
            }
        }
        if (other.name == "Sprite Light")
        {
            other.enabled = false;
            other.GetComponent<SpriteAI>().triggered = true;
        }
        if (other.tag == "NPC")
        {
            buttonPrompt.transform.position = new Vector3(other.transform.position.x, other.transform.position.y + 1.8f, other.transform.position.z + 0.5f);
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Interactable>())
        {
            Interactable interact = other.GetComponentInParent<Interactable>();
            switch (interact.type)
            {
                case "Item":
                    RemoveFocus();
                    break;

                case "Chest":
                    //chest = null;
                    //other.SendMessage("Chest", false);
                    interact.hasInteracted = false;
                    break;

                case "Door":
                    interact.hasInteracted = false;
                    if (!interact.locked)
                    {
                        if (interact.anim.GetBool("Approached"))
                        {
                            other.transform.parent.SendMessage("Door");
                        }
                    }
                    break;
            }
        }
        if (other.tag == "NPC")
        {
            buttonPrompt.transform.position = new Vector3();
        }
    }





    private void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
    }
    private void RemoveFocus()
    {
        focus = null;
    }




    protected void Punch()
    {


    }
}

//Handles the controller component that moves the player.
public class ThirdPersonController : MonoBehaviour
{

    #region Variables
    #region Rotation Math
    protected Quaternion theRotation;
    protected float player_rotateSpeed = 10;
    protected string viewType = "ThirdPerson";
    #endregion
    #region Movement
    protected Vector3 moveDirection;
    protected float moveSpeed = 10;
    protected float jumpForce = 14;
    #endregion
    #region Physics
    protected float gravity = 3;
    #endregion
    #endregion
    #region Components
    [HideInInspector] public Animator anim;
    protected CharacterController player;
    protected CapsuleCollider capu;
    protected Transform skeleton;
    protected float rotator = 150;

    protected PauseMenu pause;
    protected Transform cam;

    protected Gamepad gamepad;

    #endregion




   [HideInInspector] public bool canMove;

    protected void ControllerStart()
    {
        gamepad = FindObjectOfType<Gamepad>();

        cam = Camera.main.transform;
        anim = GetComponent<Animator>();
        player = gameObject.AddComponent<CharacterController>();
        pause = FindObjectOfType<PauseMenu>();

        player.height = 2;
        player.radius = 0.5f;
        player.center = new Vector3(0, 1.1f, 0);

    }
    protected void ControllerUpdate()
    {
        
            MovePlayer();
        
    }

    protected void MovePlayer()
    {

        Actions();
        #region Move direction
        float yspeed = moveDirection.y;
        if (canMove)
        {
            moveDirection = transform.forward * gamepad.moveY + (transform.right * gamepad.moveX);

            moveDirection = moveDirection.normalized * moveSpeed;
        }
        moveDirection.y = yspeed;
        #endregion
        moveDirection.y = moveDirection.y + (Physics.gravity.y * Time.deltaTime * gravity);

        player.Move(moveDirection * Time.deltaTime);

        if (viewType == "ThirdPerson")
        {
            #region Rotation
            if (canMove)
            {
                if (gamepad.moveX != 0 || gamepad.moveY != 0)
                {
                    //player.transform.rotation = Quaternion.Euler(0, rotator.rotation.eulerAngles.y + 30, 0);
                    player.transform.rotation = Quaternion.Euler(0, rotator + 30, 0);
                    Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 90, moveDirection.z));
                    theRotation = newRotation;

                    skeleton.rotation = Quaternion.Slerp(skeleton.rotation, newRotation, player_rotateSpeed * Time.deltaTime);
                }
                
            }
            #endregion
        }
        #region Animator
        anim.SetBool("OnGround", player.isGrounded);
        if (canMove)
        {
            anim.SetFloat("Speed", Mathf.Abs(gamepad.moveX) + Mathf.Abs(gamepad.moveY));
        }
        #endregion

    }
    protected void Actions()
    {
        if (player.isGrounded)
        {
            moveDirection.y = 0;
            if (gamepad.button_Jump)
            {
                moveDirection.y = jumpForce;
            }
        }
    }

}

