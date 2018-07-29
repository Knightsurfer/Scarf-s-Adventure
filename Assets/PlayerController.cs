using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
        string movementX = "LeftStickX";

        string movementY = "LeftStickY";

        float speed = 5;

        public float moveX;
        public float moveY;

    void Update()
    {
        if (moveX != 0 || moveY != 0)
        {
            GetComponent<Animator>().SetBool("Walking", true);
        }
        else if (moveX == 0 || moveY == 0)
        {
            GetComponent<Animator>().SetBool("Walking", false);
        }
    }
    void FixedUpdate ()
        {
            moveX = Input.GetAxis(movementX);
            moveY = Input.GetAxis(movementY);

            Vector3 moveVector = new Vector3(moveX, 0, moveY) * (Time.deltaTime * speed);
            this.transform.Translate(moveVector, Space.World);

        }

       
 
/* Rigid Stuff
    
  float speed = 1;
    public float moveX;
    public float moveY;

    string movementX = "LeftStickX";
    string movementY = "LeftStickY";

    Rigidbody rb;


    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }


    private void Update()
    {
        moveX = Input.GetAxis(movementX);
        moveY = Input.GetAxis(movementY);
        if(moveX != 0 || moveY != 0)
        {
            GetComponent<Animator>().SetBool("Walking", true);
        }
        else if (moveX == 0 || moveY == 0)
        {
            GetComponent<Animator>().SetBool("Walking", false);
        }

    }


    void FixedUpdate()
    {
        Vector3 moveVector = new Vector3(moveX, 0, moveY) * speed;
        rb.AddForce(moveVector, ForceMode.VelocityChange);
    }
    */

}
