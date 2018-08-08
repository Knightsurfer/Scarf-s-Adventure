using UnityEngine;
using System.Collections;

public class RoverFollow : MonoBehaviour
{
    Transform target;
    public Animator anim;
    protected EscapeMenuActivator pause;

    protected float currentYaw = 180f;
    float currentZoom = 2f;

    Quaternion invertRotation;


    [HideInInspector]
    public string Controller = "Keyboard";

    void Start()
    {
        
        pause = GameObject.Find("Game Manager").GetComponent<EscapeMenuActivator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = target.GetComponent<Animator>();
    }




    void Update()
    {
        switch (Controller)
        {

            case "Keyboard":
                KeyboardControls();
                break;

            case "Gamepad":
                GamepadControls();
                break;

        }



        switch (pause.EscapeMenuOpen)
        {
            case false:
                CameraMovement();
                break;
        }
    }


    void KeyboardControls()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * 4f;
        currentZoom = Mathf.Clamp(currentZoom, 2.4f, 4f);

        currentYaw += Input.GetAxis("Mouse X") * 100f * Time.deltaTime;


        if(Input.GetKey(KeyCode.W))
        {
                        
            anim.SetFloat("MoveY", 1.0f);
            anim.SetBool("Moving", true);


        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetFloat("MoveY", 0f);
            anim.SetBool("Moving", false);

        }

   

    }

    void GamepadControls()
    {

        currentYaw -= Input.GetAxis("RightStickY") * 100f * Time.deltaTime;
        currentZoom -= Input.GetAxis("RightStickX") * 4f;
        currentZoom = Mathf.Clamp(currentZoom, 2.4f, 4f);
    }





    void CameraMovement()
    {
        var characterRotation = transform.rotation;


        characterRotation.x = 0;
        characterRotation.z = 0;
        
        transform.position = target.position - new Vector3(0, -2, -2) * currentZoom;

        characterRotation.y = transform.rotation.y;
        

        target.transform.rotation = characterRotation;




        transform.LookAt(target.position + Vector3.up * 2f);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }










}
