using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : Controller {

    #region Third Person
    #region Components
    protected Animator anim;

    protected CapsuleCollider capu;
    protected CharacterController player;
    protected Transform rotator;
    protected ThirdPerson_Mode thirdPerson;
    protected Vector3 controllerOffset = new Vector3(0, 1.1f, 0);
    #endregion
    #region Variables
    protected Vector3 moveDirection;
    protected float moveSpeed = 10;
    protected float jumpForce = 7;
    protected float gravity = 50;
    protected float rotateSpeed = 10;
    protected Transform skeleton;
    #endregion



    protected void Start()
    {
        Components();
    }
    protected void Update()
    {
        ControllerCheck();
        Controller();
    }

    protected void Components()
    {
        #region Add Components
        gameObject.AddComponent<CharacterController>();
        player = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        #endregion
        #region Set Components
        skeleton = GameObject.FindGameObjectWithTag("Skeleton").transform;
        rotator = GameObject.Find("Rotator").transform;
        player.center = controllerOffset;
        player.radius = 0.5f;
        player.height = 2;
        #endregion
    }
    protected void Controller()
    {
        float yStore = moveDirection.y;
        moveDirection = transform.forward * moveY  + (transform.right * moveX);
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;
        

        if (player.isGrounded)
        {
            moveDirection.y = 0;
            if (button_Jump)
            {
                moveDirection.y = jumpForce;
            }            
        }
        moveDirection.y = moveDirection.y + (Physics.gravity.y * Time.deltaTime * gravity* Time.deltaTime);


        Movement();

        if (moveX != 0 || moveY != 0)
        {
            player.transform.rotation = Quaternion.Euler(0, rotator.rotation.eulerAngles.y + 30, 0);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 90, moveDirection.z));

            skeleton.rotation = Quaternion.Slerp(skeleton.rotation,newRotation,rotateSpeed * Time.deltaTime);
        }



        anim.SetBool("OnGround", player.isGrounded);
        anim.SetFloat("Speed",Mathf.Abs(moveX) + Mathf.Abs(moveY));

       

        
        
    }

    protected void Movement()
    {
        player.Move(moveDirection * Time.deltaTime);
        
    }
    

    #endregion
}
