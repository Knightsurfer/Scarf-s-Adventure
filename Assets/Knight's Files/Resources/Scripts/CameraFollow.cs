using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    Transform target;

    protected EscapeMenuActivator pause;

    protected Vector3 offset = new Vector3(0,-2,-2);

    protected float zoomSpeed     = 4f;
    protected float minZoom       = 2.4f;
    protected float maxZoom       = 4f;

    protected float yawSpeed = 100f;
    protected float currentYaw = 180f;

    protected float pitch         = 2f;
    protected float currentZoom  = 2f;


    [HideInInspector]
    public string Controller = "Keyboard";

    void Start()
    {
        pause = GameObject.Find("Game Manager").GetComponent<EscapeMenuActivator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }




    void Update()
    {
		switch(Controller)
        {
            case "Keyboard":
                KeyboardControls();
                break;

            case "Gamepad":
                GamepadControls();
                break;

        }



        switch(pause.EscapeMenuOpen)
        {
            case false:
                CameraMovement();
                break;
        }
    }
 

    void KeyboardControls()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        currentYaw += Input.GetAxis("Mouse X") * yawSpeed * Time.deltaTime;
    }

    void GamepadControls()
    {

        currentYaw -= Input.GetAxis("RightStickY") * yawSpeed * Time.deltaTime;
        currentZoom -= Input.GetAxis("RightStickX") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
    }







    void CameraMovement()
	{
		transform.position = target.position - offset*currentZoom;
		transform.LookAt(target.position +Vector3.up * pitch);
		
		transform.RotateAround(target.position,Vector3.up, currentYaw);
	}

	
	
	
	
		
}
