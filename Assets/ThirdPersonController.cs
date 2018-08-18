using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour {

    #region Third Person
    #region Components
    protected Animator anim;

    protected CapsuleCollider capu;
    protected CharacterController player;
    protected ThirdPerson_Mode thirdPerson;
    protected Vector3 controllerOffset = new Vector3(0, 1.1f, 0);
    #endregion
    #region Variables
    protected Vector3 moveDirection;
    protected float moveSpeed = 10;
    protected float jumpForce = 0;
    protected float gravity = 2f;
    
    #endregion



    protected void Start()
    {
        Components();
    }
    protected void Update()
    {
        Controller();
    }

    protected void Components()
    {
        #region Add Components
        gameObject.AddComponent<CharacterController>();
        player = GetComponent<CharacterController>();
        #endregion
        #region Set Components
        player.center = controllerOffset;
        player.radius = 0.5f;
        player.height = 2;
        #endregion
    }
    protected void Controller()
    {
        //moveDirection = new Vector3(Input.GetAxis("LeftStickX") * moveSpeed, jumpForce, Input.GetAxis("LeftStickY") * moveSpeed);
        moveDirection = transform.forward * Input.GetAxis("LeftStickY")  + (transform.right * Input.GetAxis("LeftStickX"));
        moveDirection = moveDirection.normalized * moveSpeed;
        jumpForce = jumpForce + (Physics.gravity.y * Time.deltaTime * gravity);

        if (player.isGrounded)
        {
            jumpForce = 0;
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                moveDirection.y = jumpForce = 5;
            }
        }
        Movement();
    }

    protected void Movement()
    {
        player.Move(moveDirection * Time.deltaTime);
    }
    

    #endregion


















   







}
