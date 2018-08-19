using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson_Mode : Controller
{
    protected Transform cam;
    protected Transform player;
    protected Transform rotator;

    protected Vector3 offset;
    protected float rotateSpeed = 10;



    private void Start()
    {
       rotator = GameObject.Find("Rotator").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main.transform;

        offset = player.position - cam.position;
        rotator.position = player.position;
    }
    void LateUpdate()
    {
        XboxConversion();
        float horizontal = cameraX * rotateSpeed;
        rotator.Rotate(0, horizontal, 0);

        float vertical = -cameraY * rotateSpeed;
        rotator.Rotate(vertical,0,0);






        if(rotator.rotation.eulerAngles.x > 45 && rotator.rotation.eulerAngles.x < 180)
        {
            rotator.rotation = Quaternion.Euler(45, 0, 0);
        }

        if (rotator.rotation.eulerAngles.x > 180 && rotator.rotation.eulerAngles.x < 315)
        {
            rotator.rotation = Quaternion.Euler(315, 0, 0);
        }




        float desiredYAngle = rotator.eulerAngles.y;
        float desiredXAngle = rotator.eulerAngles.x;
 

        Quaternion rotation = Quaternion.Euler(desiredXAngle,desiredYAngle,0);
        cam.position = player.position - (rotation * offset);

        if (cam.position.y < player.position.y)
        {
            cam.position = new Vector3(cam.position.x, player.position.y +.5f, cam.position.z);
        }


        cam.LookAt(player);
    }














}
