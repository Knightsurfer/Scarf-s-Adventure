using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson_Mode : MonoBehaviour
{
    protected Transform cam;
    protected Transform player;
    protected Transform rotator;

    public Vector3 offset;
    protected float rotateSpeed = 10;



    private void Start()
    {
       rotator =GameObject.Find("Rotator").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = Camera.main.transform;

        offset = player.position - cam.position;
        rotator.position = player.position;
        rotator.parent = player.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float horizontal = -Input.GetAxis("RightStickX") * rotateSpeed;
        player.Rotate(0, horizontal, 0);

        float vertical = -Input.GetAxis("RightStickY") * rotateSpeed;
        rotator.Rotate(vertical,0,0);


        float desiredYAngle = player.eulerAngles.y;
        float desiredXAngle = rotator.eulerAngles.x;
 

        Quaternion rotation = Quaternion.Euler(desiredXAngle,desiredYAngle,0);
        cam.position = player.position - (rotation * offset);

        if (cam.position.y < player.position.y)
        {
            cam.position = new Vector3(cam.position.x, player.position.y, cam.position.z);
        }


        cam.LookAt(player);
    }














}
