using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson_Mode : ThirdPersonController
{
    #region Variables
    protected Vector3 offset;
    protected float cam_rotateSpeed_X = 180;
    protected float cam_rotateSpeed_Y = 80;
    

    Transform lookObject;
    #endregion
    #region Components
    protected Transform cam;
    #endregion



    #region Start
    protected void Start()
    {
        lookObject = GameObject.Find("Look Object").transform;
        Components();
    }
    protected void Components()
    {
        #region Add Components
        gameObject.AddComponent<CharacterController>();
        #endregion
        #region Get Components
        anim = GetComponent<Animator>();
        player = GetComponent<CharacterController>();
        pause = GameObject.Find("Pause Menu").GetComponent<PauseMenu>();
        #endregion
        #region Set Components
        player.height = 2;
        player.radius = 0.5f;
        player.center = new Vector3(0, 1.1f, 0);
        #endregion

        #region Getting Variables
        cam = Camera.main.transform;
        rotator = GameObject.Find("Rotator").transform;
        skeleton = GameObject.FindGameObjectWithTag("Skeleton").transform;
        #endregion
        #region Set Variables
        offset = transform.position - cam.position;
        rotator.position = transform.position;
        #endregion
    }
    #endregion









    protected void Update()
    {

        ControllerCheck();
        
        MovePlayer();
    }

    protected void LateUpdate()
    {
        float horizontal = cameraX * cam_rotateSpeed_X * Time.deltaTime;
        rotator.Rotate(0, horizontal, 0);

        float vertical = -cameraY * cam_rotateSpeed_Y * Time.deltaTime;
        rotator.Rotate(vertical, 0, 0);

        if (rotator.rotation.eulerAngles.x > 45 && rotator.rotation.eulerAngles.x < 180)
        {
            rotator.rotation = Quaternion.Euler(45, 0, 0);
        }

        if (rotator.rotation.eulerAngles.x > 180 && rotator.rotation.eulerAngles.x < 315)
        {
            rotator.rotation = Quaternion.Euler(315, 0, 0);
        }

        



        switch (viewType)
        {
            case "FirstPerson":
                cam.localPosition = (lookObject.localPosition);
               
                if (button_Select)
                {
                    cam.parent = GameObject.Find("Player 1").transform;
                    viewType = "ThirdPerson";
                    cam.localPosition = new Vector3(44.453f, 2.56f, 33.1f);
                   
                }
                FirstPerson();
                break;

            case "ThirdPerson":
                if (button_Select)
                {
                    cam.parent = lookObject.transform;
                    viewType = "FirstPerson";
                    
                   
                }
                ThirdPerson();
                break;




        }
    }



    void FirstPerson()
    {
        float horizontal = cameraX * cam_rotateSpeed_X * Time.deltaTime;

        float vertical = -cameraY * cam_rotateSpeed_Y * 2 * Time.deltaTime;

        lookObject.Rotate(vertical, 0, 0);
        player.transform.Rotate(0,horizontal,0);

    }

    void ThirdPerson()
    {
        float desiredYAngle = rotator.eulerAngles.y;
        float desiredXAngle = rotator.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        cam.position = transform.position - (rotation * offset);

        if (cam.position.y < transform.position.y -0.5f)
        {
            cam.position = new Vector3(cam.position.x, transform.position.y, cam.position.z);
        }


        cam.LookAt(transform);
    }













}
