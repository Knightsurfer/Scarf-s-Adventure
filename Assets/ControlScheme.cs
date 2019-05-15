using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlScheme : MonoBehaviour
{

    #region Old Keycode Stuff

    #region Controllers //Variables for detecting how many controllers there are.
    public string controller;
    [HideInInspector] public bool isGamepad;
    protected string selectedController;
    public string[] currentControllers = new string[] { };
    #endregion
    #region Axis    //All analog movement is recorded as a float here.
    [HideInInspector] public string direction = "none";

    [HideInInspector] public float moveX;
    [HideInInspector] public float moveY;

    [HideInInspector] public float cameraX;
    [HideInInspector] public float cameraY;

    [HideInInspector] public float triggerL;
    [HideInInspector] public float triggerR;
    #endregion
    #region Buttons //All button feedback is recorded as a bool here.
    [HideInInspector] public KeyCode button_Jump;
    [HideInInspector] public KeyCode button_Attack;
    [HideInInspector] public KeyCode button_Action;
    [HideInInspector] public KeyCode button_Kick;

    [HideInInspector] public bool d_Up;
    [HideInInspector] public bool d_Down;
    [HideInInspector] public bool d_Left;
    [HideInInspector] public bool d_Right;
    [HideInInspector] public KeyCode button_L;
    [HideInInspector] public KeyCode button_R;

    [HideInInspector] public KeyCode button_Start;
    [HideInInspector] public KeyCode button_Select;
    #endregion



    #endregion





    #region New KeyCode Stuff
    public List<Text> Inputs = new List<Text>();

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ControllerDetect()
    {
        //Starts checking number of controllers and then decides what controller mode to turn on.
        //Will Come back to this for a simpler version later.

        currentControllers = Input.GetJoystickNames();
        if (currentControllers.Length < 1 || currentControllers[0] == "")
        {
            selectedController = "Keyboard";
        }
        if (currentControllers.Length > 0)
        {
            selectedController = currentControllers[currentControllers.Length - 1];
            if (currentControllers[0] != "")
            {
                selectedController = currentControllers[0];
            }
        }
        switch (selectedController)
        {
            case "Wireless Controller":
                controller = "PS4";
                break;

            case "Controller (Xbox 360 Wireless Receiver for Windows)":
                controller = "Xbox";
                break;

            case "Keyboard":
                controller = "Keyboard";
                break;

            case "SFC30 Joystick":
                controller = "Snes";
                break;
        }



        if (selectedController == "Wireless Controller")
        {
            controller = "PS4";
        }
        if (selectedController == "Controller (Xbox 360 Wireless Receiver for Windows)")
        {
            controller = "Xbox";
        }
        if (selectedController == "")
        {
            controller = "Keyboard";
        }

    }

    protected void ControllerCheck()
    {
        //Controller mode switcher.
        ControllerDetect();
        switch (controller)
        {
            case "PS4":
                isGamepad = true;
                //Ps4Conversion();
                break;

            case "Xbox":
                isGamepad = true;
                XboxConversion();
                break;

            case "Keyboard":
                isGamepad = false;
                //KeyboardConversion();
                break;

            case "Snes":
                isGamepad = true;
                //SnesConversion();
                break;
        }
    }






    void XboxConversion()
    {
        button_Jump = KeyCode.JoystickButton1;
        button_Attack = KeyCode.JoystickButton0;
        button_Kick = KeyCode.JoystickButton2;
        button_Action = KeyCode.JoystickButton3;
    }





    // Update is called once per frame
    void Update()
    {
        ControllerCheck();
        Inputs[0].text = "Button Attack = " + Input.GetKey(button_Attack);
        Inputs[1].text = "Button Jump = " + Input.GetKey(button_Jump);
        Inputs[2].text = "Button Kick = " + Input.GetKey(button_Kick);
        Inputs[3].text = "Button Action = " + Input.GetKey(button_Action);
    }
}
