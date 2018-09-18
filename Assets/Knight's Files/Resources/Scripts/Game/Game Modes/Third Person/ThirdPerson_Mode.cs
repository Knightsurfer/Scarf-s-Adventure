using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ThirdPerson_Mode : ThirdPerson_Interact
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

    private void Start()
    {
        InteractStart();
        ControllerStart();
        Components();
    }
    private void Update()
    {
        if(canMove == false)
        {
            if (GameObject.Find("Scarf").GetComponent<ThirdPerson_Mode>().currentYaw != 0)
            {
                GameObject.Find("Scarf").GetComponent<ThirdPerson_Mode>().currentYaw -= 1 *5;
            }
            else
            {
                canMove = true;
            }
        }



        InteractUpdate();
        
            ControllerCheck();
        if (canMove)
        {
            ControllerUpdate();
        }
    }
    private void LateUpdate()
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


   private void Components()
    {
        #region Getting Variables
        rotator = GameObject.Find("Rotator").transform;
        lookObject = GameObject.Find("Look Object").transform;
        skeleton = GameObject.FindGameObjectWithTag("Skeleton").transform;
        #endregion

        offset = transform.position - cam.position;
        rotator.position = transform.position;
    }
   private void FirstPerson()
    {
        float vertical = cameraX * cam_rotateSpeed_Y  * 2 * Time.deltaTime;
        float horizontal = -cameraY * cam_rotateSpeed_Y * 2 * Time.deltaTime;

        cam.localRotation = Quaternion.Euler(cam.localRotation.x, lookObject.localRotation.y, cam.localRotation.z);
        lookObject.Rotate(horizontal, 0, 0);

        transform.Rotate(0, vertical, 0);
        cam.Rotate(0, horizontal, 0);
        cam.localPosition = Vector3.zero;
    }
   private void ThirdPerson()
    {
        currentZoom -= cameraY * 3.5f * Time.deltaTime;
        ButtonPrompt();


        currentZoom = Mathf.Clamp(currentZoom, 2f, 4f);
        cam.transform.position = player.transform.position - new Vector3(0, -1, -2) * currentZoom;
        cam.transform.LookAt(player.transform.position + Vector3.up * 2f);

        cam.transform.RotateAround(player.transform.position, Vector3.up, currentYaw);
    } 
}
