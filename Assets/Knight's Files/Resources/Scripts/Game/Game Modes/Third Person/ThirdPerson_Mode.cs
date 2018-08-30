using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson_Mode : ThirdPersonController
{
    #region Variables
    protected Vector3 offset;
    protected float cam_rotateSpeed_X = 180;
    protected float cam_rotateSpeed_Y = 80;

    protected float currentYaw = 210f;
    protected float currentZoom = 2f;
    protected GameObject neck;

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




    float horizontal;




    protected void Update()
    {

        ControllerCheck();
        
        MovePlayer();
    }

    protected void LateUpdate()
    {
        horizontal = cameraX * cam_rotateSpeed_X * Time.deltaTime;
        rotator.Rotate(0, horizontal, 0);

        
        



        currentYaw += cameraX * cam_rotateSpeed_X * Time.deltaTime;
        switch (viewType)
        {
            case "FirstPerson":
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
        float vertical = -cameraY * cam_rotateSpeed_Y * 2 * Time.deltaTime;

        cam.localPosition = new Vector3();
        cam.localRotation = Quaternion.Euler(0, 0, 0);
        transform.Rotate(0, horizontal,0 );
        lookObject.Rotate(0,vertical,0);
    }

    void ThirdPerson()
    {
        currentZoom -= cameraY * cam_rotateSpeed_Y / 2 * Time.deltaTime;

        //neck.transform.localScale = new Vector3(1, 1, 1);
        currentZoom = Mathf.Clamp(currentZoom, 2f, 4f);
        cam.transform.position = player.transform.position - new Vector3(0, -1, -2) * currentZoom;
        cam.transform.LookAt(player.transform.position + Vector3.up * 2f);

        cam.transform.RotateAround(player.transform.position, Vector3.up, currentYaw);
    } 
}
