using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ThirdPerson_Mode : ThirdPersonController
{
    #region Variables
    protected Vector3 offset;
    protected float cam_rotateSpeed_X = 180;
    protected float cam_rotateSpeed_Y = 80;

    public float currentYaw = 210f;
    protected float currentZoom = 2f;
    protected GameObject neck;

    public float lookY;

    Transform lookObject;
    #endregion
    #region Components
    protected Transform cam;
    #endregion

    



    #region Start
    protected void Start()
    {
        ControllerDetect();

        lookObject = GameObject.Find("Look Object").transform;
        Components();

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            player.transform.localPosition = new Vector3(51, 0, 34);
        }
    }
    protected void Components()
    {
        #region Add Components
        gameObject.AddComponent<CharacterController>();
        #endregion
        #region Get Components
        anim = GetComponent<Animator>();
        player = GetComponent<CharacterController>();
        pause = FindObjectOfType<PauseMenu>();
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




        Actions();
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
        float vertical = cameraX * cam_rotateSpeed_Y  * 2 * Time.deltaTime;
        float horizontal = -cameraY * cam_rotateSpeed_Y * 2 * Time.deltaTime;



        cam.localRotation = Quaternion.Euler(cam.localRotation.x, lookObject.localRotation.y, cam.localRotation.z);
        lookObject.Rotate(horizontal, 0, 0);



        transform.Rotate(0, vertical, 0);
        cam.Rotate(0, horizontal, 0);


        cam.localPosition = Vector3.zero;
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
