using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//The Main Startup Class. (Nothing is really required from this class.)
public class ThirdPerson : Thirdperson_PartyHandler
{
    

    public void Start()
    {
       
        StartingVariables();
        InteractStart();
        PlayerStart();
        Components();
        
    }
    private void Update()
    {
        LevelBounds();
        if (!canMove)
        {
            if (currentYaw != 0)
            {
                currentYaw -= 1 * 5;
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
        PartySwitcher();


        InteractUpdate();
        PlayerUpdate();
        
    }


    protected void LevelBounds()
    {
        if (transform.localPosition.y < -15)
        {
            transform.position = GameObject.Find("Player 1").transform.position;
            health = health - 45;
        }
    }




}

public class Thirdperson_PartyHandler : Thirdperson_Mode
{
    BotReciever partyMember;
 
    protected void PartySwitcher()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            partyMember = FindObjectOfType<BotReciever>();
            if (partyMember.isPartyMember)
            {
                cam = null;
                Destroy(partyMember.GetComponent<BoxCollider>());
                Destroy(partyMember.GetComponent<CapsuleCollider>());

                gameObject.AddComponent<BotReciever>();
                GetComponent<BotReciever>().points = partyMember.points;
                GetComponent<BotReciever>().WaypointPassed = 0;
                GetComponent<BotReciever>().range = 20;

                partyMember.gameObject.AddComponent<ThirdPerson>();
                Destroy(partyMember.botAI);
                partyMember.bot.enabled = true;
                Destroy(partyMember);
                GetComponent<BotReciever>().target = FindObjectOfType<ThirdPerson>().transform;
                Destroy(this);
            }
        }
    }

}



//Camera Control.
public class Thirdperson_Mode : Thirdperson_Stats
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
    [HideInInspector] public float currentYaw = 210f; //210
    protected float currentZoom = 2f;
    #endregion
    #endregion
    

    protected float timerspeed = 0f;
    protected float elapsed = -0.5f;


    private void LateUpdate()
    {
        
            horizontal = game.cameraX * cam_rotateSpeed_X * Time.deltaTime;
            rotator = currentYaw + 150;
            if (canMove)
            {
                currentYaw += game.cameraX * cam_rotateSpeed_X * Time.deltaTime;
            }
            switch (viewType)
            {
                case "FirstPerson":
                    if (game.button_Select)
                    {
                        cam.parent = GameObject.Find("Player 1").transform;
                        viewType = "ThirdPerson";
                        cam.localPosition = new Vector3(44.453f, 2.56f, 33.1f);
                    }
                    FirstPerson();
                    break;

                case "ThirdPerson":
                    if (game.button_Select)
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
        float vertical = game.cameraX * cam_rotateSpeed_Y * 2 * Time.deltaTime;
        float horizontal = -game.cameraY * cam_rotateSpeed_Y * 2 * Time.deltaTime;

        cam.localRotation = Quaternion.Euler(cam.localRotation.x, lookObject.localRotation.y, cam.localRotation.z);
        lookObject.Rotate(horizontal, 0, 0);

        transform.Rotate(0, vertical, 0);
        cam.Rotate(0, horizontal, 0);
        cam.localPosition = Vector3.zero;
    }
    private void ThirdPerson()
    {
            currentZoom -= game.cameraY * 3.5f * Time.deltaTime;

            currentZoom = Mathf.Clamp(currentZoom, 2f, 4f);
        
            cam.transform.position = player.transform.position - new Vector3(0, -1, -2) * currentZoom;
            cam.transform.LookAt(player.transform.position + Vector3.up * 2f);

            cam.transform.RotateAround(player.transform.position, Vector3.up, currentYaw);
        
    }
}

//Storage for individual stat values.
public class Thirdperson_Stats : Thirdperson_Start
{

   [HideInInspector] public int healthMax;
   [HideInInspector] public int magicMax;
   [HideInInspector] protected int exp;


   [HideInInspector] public int level;
   [HideInInspector] public int magic;
   [HideInInspector] public int health;
}

//Controls the starting position of the player.
public class Thirdperson_Start : Thirdperson_Interact
{
 
    protected void StartingVariables()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "Gameplay Test":
               
                break;
        }
    }

 




}

//Handles Interaction.
public class Thirdperson_Interact : Thirdperson_PlayerController
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
                //FindObjectsOfType<Interactable>()[i].SendMessage("Chest", FindObjectsOfType<Interactable>()[i].obtained);
                //FindObjectsOfType<Interactable>()[i].GetComponents<BoxCollider>()[1].enabled = !FindObjectsOfType<Interactable>()[i].obtained;
            }
        }
    }
    protected void InteractUpdate()
    {
        Interact();
    }
    protected void ButtonPrompt()
    {
        //buttonPrompt.transform.LookAt(cam.transform.position);
    }
    protected void OnTriggerEnter(Collider interaction)
    {
        if (interaction != null)
        {
            Interactable entity = interaction.GetComponentInParent<Interactable>();
            switch (entity.type)
            {
                default:
                    SetFocus(entity);
                    break;

                case "Item":
                    if (interaction != null)
                    {
                        SetFocus(entity);
                        interaction.transform.parent.SendMessage("Item");
                    }
                    break;
            }

        }
        switch (interaction.name)
        {
            case "Ledge":
                if (!collisionDetected && !player.isGrounded)
                {
                    anim.SetBool("Hanging", true);
                    collisionDetected = true;
                    moveType = "Hanging";

                }
                break;
            case "Sprite Light":
                interaction.enabled = false;
                interaction.GetComponent<SpriteAI>().triggered = true;
                break;

        }
        if (interaction.tag == "NPC")
        {
            buttonPrompt.transform.position = new Vector3(interaction.transform.position.x, interaction.transform.position.y + 1.8f, interaction.transform.position.z + 0.5f);
        }
    }
    protected void OnTriggerExit(Collider interaction)
    {
        if (interaction != null)
        {
            Interactable entity = interaction.GetComponentInParent<Interactable>();
            switch (entity.type)
            {
                default:
                    RemoveFocus();
                    break;

                case "Item":
                    RemoveFocus();
                    entity.SendMessage("Item");
                    break;
            }
            if (interaction.tag == "NPC")
            {
                buttonPrompt.transform.position = new Vector3();
            }
        }

    }

    #region Interaction Detection
    private void SetFocus(Interactable newFocus)
    {
        focus = newFocus;

        switch(focus.type)
        {
            default:
                focus.hasInteracted = true;
                break;

            case "Door":
                break;


            case "Chest":
                break;

            
        }


       
    }
    private void RemoveFocus()
    {
        focus.hasInteracted = false;
        if(focus.type  == "Door")
        {
            //focus.SendMessage("DoorClose");
        }
        focus = null;
    }

    private void Interact()
    {
        if (focus != null)
        {
            if (game.button_Action)
            {
                switch (focus.type)
                {
                    case "Chest":
                        focus.hasInteracted = !focus.hasInteracted;
                        break;

                    case "Door":
                        focus.hasInteracted = true;
                        break;

                    case "Save Point":
                        focus.hasInteracted = true;
                        break;
                }
            }
        }
    }
    #endregion
}

//Handles the controller component that moves the player.

public class Thirdperson_PlayerController : Thirdperson_Camera
{
    [HideInInspector] public Animator anim;
    [HideInInspector]public CharacterController player;
    public string moveType = "Normal";
    protected bool collisionDetected = true;
    Vector3 playerPosition;
    Vector3 playerDestination;




    protected void PlayerStart()
    {
        game = FindObjectOfType<GameManager>();

        cam = Camera.main.transform;
        anim = GetComponent<Animator>();
        if (GetComponent<CharacterController>())
        {
            player = GetComponent<CharacterController>();
        }
        else
        {
            player = gameObject.AddComponent<CharacterController>();
        }
        pause = FindObjectOfType<PauseMenu>();

        if (player)
        {
            player.height = 2;
            player.radius = 0.5f;
            player.center = new Vector3(0, 1.1f, 0);
        }
    }
    protected void PlayerUpdate()
    {
        MovePlayer();
    }
    protected void MovePlayer()
    {
        if (player.isGrounded)
        {
            anim.applyRootMotion = true;
            collisionDetected = false;
            
            moveType = "Normal";
        }

        switch (moveType)
        {
            case "Normal":
                player.enabled = true;
                anim.SetBool("Climbing", false);
                NormalMovement();
                break;

            case "Hanging":
                
                player.enabled = false;
                if(game.button_Jump)
                {
                    moveType = "Normal";
                    anim.SetBool("Hanging", false);
                }
                if (game.moveY > 0.5f)
                {

                    anim.SetBool("Climbing", true);
                    if (playerPosition == Vector3.zero)
                    {
                        playerPosition = transform.position;
                    }
                    playerDestination = new Vector3(playerPosition.x, playerPosition.y + 10, playerPosition.z);
                    anim.SetBool("Hanging", false);

                    

                }

                if (playerPosition != Vector3.zero)
                {
                    transform.position = Vector3.MoveTowards(playerPosition, playerDestination, 1);
                    if (transform.position == playerDestination)
                    {
                        
                        moveType = "Normal";

                    }
                }
                break;
               
        }
        

    }

    protected void NormalMovement()
    {
        Actions();
        #region Move direction
        float yspeed = moveDirection.y;
        if (canMove)
        {
            moveDirection = transform.forward * game.moveY + (transform.right * game.moveX);

            moveDirection = moveDirection.normalized * moveSpeed;
        }
        moveDirection.y = yspeed;
        #endregion
        moveDirection.y = moveDirection.y + (Physics.gravity.y * Time.deltaTime * gravity);
        if (player)
        {
            player.Move(moveDirection * Time.deltaTime);
        }

        if (viewType == "ThirdPerson")
        {
            #region Rotation
            if (canMove)
            {
                if (game.moveX != 0 || game.moveY != 0)
                {
                    player.transform.rotation = Quaternion.Euler(0, rotator + 30, 0);
                    Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 90, moveDirection.z));
                    theRotation = newRotation;

                    skeleton.rotation = Quaternion.Slerp(skeleton.rotation, newRotation, player_rotateSpeed * Time.deltaTime);
                }

            }
            #endregion
        }
        #region Animator
        if (canMove)
        {
            anim.SetBool("OnGround", player.isGrounded);
            anim.SetFloat("Speed", Mathf.Abs(game.moveX) + Mathf.Abs(game.moveY));
        }
        #endregion



    }








    protected void Actions()
    {
        #region Jump
        if (player)
        {
            if (player.isGrounded)
            {
                moveDirection.y = 0;
                if (game.button_Jump)
                {
                    moveDirection.y = jumpForce;
                    
                }
                anim.SetBool("OnGround", false);
            }
            else
            {
                anim.SetBool("OnGround", true);
                
            }
        }
        #endregion
    }
}


public class Thirdperson_Camera : MonoBehaviour
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
   
    
    protected CapsuleCollider capu;
    protected Transform skeleton;
    protected float rotator = 150;
    protected PauseMenu pause;
    protected Transform cam;
    protected GameManager game;
    #endregion

    public bool canMove;
    
    
    
   

}

