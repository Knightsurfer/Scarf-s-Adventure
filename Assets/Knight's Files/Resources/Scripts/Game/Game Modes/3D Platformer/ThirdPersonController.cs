using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : Controller {

    #region Variables
    protected Vector3 moveDirection;
    protected Vector3 controllerOffset = new Vector3(0, 1.1f, 0);
    protected Quaternion theRotation;
    protected float moveSpeed = 10;
    protected float jumpForce = 14;
    protected float gravity = 3;
    protected float player_rotateSpeed = 10;
    protected string viewType = "ThirdPerson";
    #endregion
    #region Components
    [HideInInspector]public Animator anim;
    protected CapsuleCollider capu;
    protected CharacterController player;
    protected Transform rotator;
    protected Transform skeleton;
    protected PauseMenu pause;

    Collider chest;





    #endregion
    
    protected void MovePlayer()
    {
        #region Move direction
            float yspeed = moveDirection.y;
            moveDirection = transform.forward * moveY + (transform.right * moveX);
            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = yspeed;
            #endregion
        
        #region Gravity Handler
        
        moveDirection.y = moveDirection.y + (Physics.gravity.y * Time.deltaTime * gravity);
        #endregion

        Movement();
        LevelBounds();
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

    protected void Movement()
    {
        player.Move(moveDirection * Time.deltaTime);
    }


    protected void LevelBounds()
    {
        if (transform.localPosition.y < -15)
        {
            transform.localPosition = new Vector3(47, 1, 38);
        }
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







        if (chest != null)
        {

            if (button_Action)
            {
                    chest.GetComponent<Animator>().SetBool("Open", true);

                if(anim.GetBool("Working") == true)
                {
                    anim.SetBool("Working", false);

                }
                else if (anim.GetBool("Working") == false)
                {
                    anim.SetBool("Working", true);
                }





            }

            if (chest.GetComponent<Animator>().GetBool("Open") == true)
            {
                if (button_Attack)
                {
                    chest.GetComponent<Animator>().SetBool("Open", false);
                }
            }

        }








    }











    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("You picked " + other.name);

        if (other.name == "chest")
        {
            chest = other;
        }

        

    }

    private void OnTriggerExit(Collider other)
    {

        Debug.Log("You left " + other.name);
        chest = null;

        if (other.name == "chest")
        {
            other.GetComponent<Animator>().SetBool("Open", false);
        }

    }






}

