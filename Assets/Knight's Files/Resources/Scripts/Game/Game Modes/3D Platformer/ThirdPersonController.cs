using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThirdPersonController : Controller {

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
    [HideInInspector]public Animator anim;
    protected CharacterController player;
    protected CapsuleCollider capu;
    protected Transform skeleton;
    protected Transform rotator;
    protected PauseMenu pause;
    protected Transform cam;



    #endregion

    public bool canMove;

    protected void ControllerStart ()
    {
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
            moveDirection = transform.forward * moveY + (transform.right * moveX);
            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = yspeed;
            #endregion
            moveDirection.y = moveDirection.y + (Physics.gravity.y * Time.deltaTime * gravity);
            player.Move(moveDirection * Time.deltaTime);

            if (viewType == "ThirdPerson")
            {
                #region Rotation
                if (moveX != 0 || moveY != 0)
                {
                    player.transform.rotation = Quaternion.Euler(0, rotator.rotation.eulerAngles.y + 30, 0);
                    Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 90, moveDirection.z));
                    theRotation = newRotation;

                    skeleton.rotation = Quaternion.Slerp(skeleton.rotation, newRotation, player_rotateSpeed * Time.deltaTime);
                }
                #endregion
            }
            #region Animator
            anim.SetBool("OnGround", player.isGrounded);
            anim.SetFloat("Speed", Mathf.Abs(moveX) + Mathf.Abs(moveY));
            #endregion
        
    }
    protected void Actions()
    {
        if (player.isGrounded)
        {
            moveDirection.y = 0;
            if (button_Jump)
            {
                moveDirection.y = jumpForce;
            }
        }
    }

}

