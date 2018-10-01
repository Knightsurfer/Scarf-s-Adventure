using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamepad : Controller
{

	
	private void Update ()
    {
        ControllerDetect();
        ControllerCheck();

        if (isTest)
        {
            ControllerInput();
        }
        
    }
}

public class Controller : MonoBehaviour
{
    public bool isTest;

    #region Controllers //Variables for detecting how many controllers there are.
    [HideInInspector] public string controller;
    [HideInInspector] public bool isGamepad;
    protected string selectedController;
    protected string[] currentControllers = new string[] { };

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
    [HideInInspector] public bool button_Jump;
    [HideInInspector] public bool button_Attack;
    [HideInInspector] public bool button_Action;

    [HideInInspector] public bool d_Up;
    [HideInInspector] public bool d_Down;
    [HideInInspector] public bool d_Left;
    [HideInInspector] public bool d_Right;
    [HideInInspector] public bool button_L;
    [HideInInspector] public bool button_R;

    [HideInInspector] public bool button_Start;
    [HideInInspector] public bool button_Select;
    #endregion

    //Device Check
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
                Ps4Conversion();
                break;

            case "Xbox":
                isGamepad = true;
                XboxConversion();
                break;

            case "Keyboard":
                isGamepad = false;
                KeyboardConversion();
                break;
        }
    }

    //Device Input
    private void XboxConversion()
    {
        //Xbox Controls
        #region Axis
        #region Control Sticks
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        cameraX = Input.GetAxis("Axis4");
        cameraY = -Input.GetAxis("Axis5");
        #endregion
        #region Triggers
        if (Input.GetAxis("Axis3") > 0)
        {
            triggerL = Input.GetAxis("Axis3");
        }
        if (Input.GetAxis("Axis3") < 0)
        {
            triggerR = -Input.GetAxis("Axis3");
        }
        if (Input.GetAxis("Axis3") == 0)
        {
            triggerL = 0;
            triggerR = 0;
        }
        #endregion
        #endregion
        #region Buttons
        #region D-X //Horizontal D-pad input.
        if (Input.GetAxis("Axis6") < 0)
        {
            d_Left = true;
        }
        if (Input.GetAxis("Axis6") > 0)
        {
            d_Right = true;
        }
        if (Input.GetAxis("Axis6") == 0)
        {
            d_Left = false;
            d_Right = false;
        }
        #endregion
        #region D-Y //Vertical D-pad input.
        if (!d_Up)
        {
            if (Input.GetAxis("Axis7") > 0)
            {
                direction = "up";
                if(isTest)
                {
                    d_Up = true;
                }
            }
        }
        if (!d_Down)
        {
            if (Input.GetAxis("Axis7") < 0)
            {
                direction = "down";
                if (isTest)
                {
                    d_Down = true;
                }
            }
        }
        if (Input.GetAxis("Axis7") == 0)
        {
            d_Up = false;
            d_Down = false;
            direction = "none";
        }
        #endregion

        #region Main Buttons
        button_Jump = Input.GetKeyDown(KeyCode.JoystickButton1);
        button_Attack = Input.GetKeyDown(KeyCode.JoystickButton0);
        button_Action = Input.GetKeyDown(KeyCode.JoystickButton3);

        button_L = Input.GetKeyDown(KeyCode.JoystickButton4);
        button_R = Input.GetKeyDown(KeyCode.JoystickButton5);

        button_Start = Input.GetKeyDown(KeyCode.JoystickButton7);
        button_Select = Input.GetKeyDown(KeyCode.JoystickButton6);

        if(isTest)
        {
            button_Jump = Input.GetKey(KeyCode.JoystickButton1);
            button_Attack = Input.GetKey(KeyCode.JoystickButton0);
            button_Action = Input.GetKey(KeyCode.JoystickButton3);

            button_L = Input.GetKey(KeyCode.JoystickButton4);
            button_R = Input.GetKey(KeyCode.JoystickButton5);

            button_Start = Input.GetKey(KeyCode.JoystickButton7);
            button_Select = Input.GetKey(KeyCode.JoystickButton6);
        }








        #endregion
        #endregion
    }
    private void Ps4Conversion()
    {
        //Ps4 Controls
        #region Axis
        #region Control Sticks
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        cameraX = Input.GetAxis("Axis3");
        cameraY = -Input.GetAxis("Axis6");
        #endregion
        #region Triggers 
        triggerL = Input.GetAxis("Axis4");
        triggerR = Input.GetAxis("Axis5");
        #endregion
        #endregion
        #region Buttons
        #region D-X //Horizontal D-pad input.
        if (Input.GetAxis("Axis7") < 0)
        {
            d_Left = true;
        }
        if (Input.GetAxis("Axis7") > 0)
        {
            d_Right = true;
        }
        if (Input.GetAxis("Axis7") == 0)
        {
            d_Left = false;
            d_Right = false;
        }
        #endregion
        #region D-Y //Vertical D-pad input.
        if (Input.GetAxis("Axis8") > 0)
        {
            direction = "up";
        }
        if (Input.GetAxis("Axis8") < 0)
        {
            direction = "down";
        }
        if (Input.GetAxis("Axis8") == 0)
        {
            d_Up = false;
            d_Down = false;
            direction = "none";
        }
        #endregion

        #region Main Buttons
        button_Jump = Input.GetKeyDown(KeyCode.JoystickButton2);
        button_Attack = Input.GetKeyDown(KeyCode.JoystickButton1);
        button_Action = Input.GetKeyDown(KeyCode.JoystickButton3);

        button_L = Input.GetKeyDown(KeyCode.JoystickButton4);
        button_R = Input.GetKeyDown(KeyCode.JoystickButton5);

        button_Start = Input.GetKeyDown(KeyCode.JoystickButton9);
        button_Select = Input.GetKeyDown(KeyCode.JoystickButton13);
        #endregion
        #endregion
    }
    private void KeyboardConversion()
    {
        //Keyboard Controls
        #region Move
        #region MoveX
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveY = 1;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            moveY = 0;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1;
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            moveY = 0;
        }
        #endregion
        #region MoveY
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveX = 0;
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = 1;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            moveX = 0;
        }
        #endregion
        #endregion
        #region Camera
        cameraX = Input.GetAxis("Mouse X");
        cameraY = Input.GetAxis("Mouse Y");
        #endregion
        #region D-Y //Menu scroll (up/down)
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            direction = "up";
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            direction = "down";
        }
        if (Input.GetAxis("Mouse ScrollWheel") == 0)
        {
            d_Up = false;
            d_Down = false;
            direction = "none";
        }
        #endregion
        #region Actions
        button_Jump = Input.GetMouseButtonDown(1);
        button_Attack = Input.GetMouseButtonDown(0);
        button_Action = Input.GetKeyDown(KeyCode.E);

        button_Start = Input.GetKeyDown(KeyCode.Escape);
        button_Select = Input.GetKeyDown(KeyCode.Backspace);
        #endregion
    }

    //Device Test
    #region  Test Variables //For the control test scene that enables visual controller feedback.
    readonly private bool controllerTest;

    [HideInInspector] public Text moveXT;
    [HideInInspector] public Text moveYT;

    [HideInInspector] public Text LTrigger;
    [HideInInspector] public Text RTrigger;
    [HideInInspector] public Text cameraXT;
    [HideInInspector] public Text cameraYT;
    [HideInInspector] public Text jumpButton;
    [HideInInspector] public Text attackButton;

    [HideInInspector] public Text startButton;
    [HideInInspector] public Text selectButton;

    [HideInInspector] public Text lButton;
    [HideInInspector] public Text rButton;

    [HideInInspector] public Text DUp;
    [HideInInspector] public Text DDown;
    [HideInInspector] public Text DLeft;
    [HideInInspector] public Text DRight;


    [HideInInspector] public Text Axis7;
    [HideInInspector] public Text Axis8;
    [HideInInspector] public Text Axis9;
    [HideInInspector] public Text Axis10;
    [HideInInspector] public Text Axis11;
    [HideInInspector] public Text Axis12;
    [HideInInspector] public Text Axis13;
    [HideInInspector] public Text Axis14;
    [HideInInspector] public Text Axis15;
    [HideInInspector] public Text Axis16;
    [HideInInspector] public Text Axis17;
    #endregion
  
    protected void TestText()
    {
        //Assigns the text fields for test mode.

        Text moveXT = GameObject.Find("LeftStickX").GetComponent<Text>();
        Text moveYT = GameObject.Find("LeftStickY").GetComponent<Text>();

        Text cameraXT = GameObject.Find("RightStickX").GetComponent<Text>();
        Text cameraYT = GameObject.Find("RightStickY").GetComponent<Text>();

        Text LTrigger = GameObject.Find("TriggerL").GetComponent<Text>();
        Text RTrigger = GameObject.Find("TriggerR").GetComponent<Text>();

        Text jumpButton = GameObject.Find("Button Jump").GetComponent<Text>();
        Text attackButton = GameObject.Find("Button Attack").GetComponent<Text>();


        Text DUp = GameObject.Find("D-Up").GetComponent<Text>();
        Text DDown = GameObject.Find("D-Down").GetComponent<Text>();

        Text DLeft = GameObject.Find("D-Left").GetComponent<Text>();
        Text DRight = GameObject.Find("D-Right").GetComponent<Text>();

        Text lButton = GameObject.Find("Button L").GetComponent<Text>();
        Text rButton = GameObject.Find("Button R").GetComponent<Text>();

        Text startButton = GameObject.Find("Button Start").GetComponent<Text>();
        Text selectButton = GameObject.Find("Button Select").GetComponent<Text>();


    }
    protected void ControllerInput()
    {
        //Visual feedback.
        #region Axis
        moveXT.text = "Left Stick X: " + moveX;
        moveYT.text = "Left Stick Y: " + moveY;
        
        LTrigger.text = "Trigger L:  " + triggerL;
        RTrigger.text = "Trigger R:  " + triggerR;
        cameraXT.text = "Right Stick X : " + cameraX;
        cameraYT.text = "Right Stick Y : " + cameraY;
        
        #endregion
        #region Buttons
        jumpButton.text = "Button Jump: " + button_Jump;
        attackButton.text = "Button Attack: " + button_Attack;

        DUp.text = "D-Up: " + d_Up;
        DDown.text = "D-Down: " + d_Down;

        DLeft.text = "D-Left: " + d_Left;
        DRight.text = "D-Right: " + d_Right;

        lButton.text = "Button L: " + button_L;
        rButton.text = "Button R: " + button_R;

        startButton.text = "Button Start: " + button_Start;
        selectButton.text = "Button Select: " + button_Select;
        #endregion
        
    }
}