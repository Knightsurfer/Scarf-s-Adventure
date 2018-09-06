using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

    protected string[] currentControllers = new string[] { };
    protected string selectedController;
    public string controller;

  



    protected bool testMode;



    #region Text Test
    private Text moveXT;
    private Text moveYT;

    private Text LTrigger;
    private Text RTrigger;
    private Text cameraXT;
    private Text cameraYT;
    private Text jumpButton;
    private Text attackButton;

    private Text startButton;
    private Text selectButton;

    private Text lButton;
    private Text rButton;

    private Text DUp;
    private Text DDown;
    private Text DLeft;
    private Text DRight;


    protected Text Axis7;
    protected Text Axis8;
    protected Text Axis9;
    protected Text Axis10;
    protected Text Axis11;
    protected Text Axis12;
    protected Text Axis13;
    protected Text Axis14;
    protected Text Axis15;
    protected Text Axis16;
    protected Text Axis17;
    #endregion

    #region Axis
    protected float moveX;
    protected float moveY;

    protected float cameraX;
    protected float cameraY;

    protected float triggerL;
    protected float triggerR;
    #endregion

    protected bool button_Jump;
    protected bool button_Attack;
    protected bool button_Action;

    protected bool d_Up;
    protected bool d_Down;
    protected bool d_Left;
    protected bool d_Right;
    protected bool button_L;
    protected bool button_R;

    protected bool button_Start;
    protected bool button_Select;

    [HideInInspector]
    public string direction = "none";



    protected void ControllerDetect()
    {


       
        currentControllers = Input.GetJoystickNames();

        selectedController = currentControllers[currentControllers.Length - 1];


        if (currentControllers.Length == 0 && currentControllers[0] == "" || currentControllers.Length > 0 && currentControllers[currentControllers.Length - 1] == "")
        {
            selectedController = "Keyboard";
        }




        if (currentControllers[0] != "")
        {
            selectedController = currentControllers[0];
        }

        


        switch(selectedController)
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





        
    }



    private void Start()
    {
        if (testMode)
        {
            TestText();
        }
    }


    






    private void Update()
    {
        ControllerCheck();

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            testMode = true;
        }
       
        if (testMode)
        {
            TestText();
            ControllerInput();
        }
    }


    protected void ControllerCheck()
    {
        ControllerDetect();

        switch(controller)
        {
            case "PS4":
                Ps4Conversion();
                break;

            case "Xbox":
                XboxConversion();
                break;

            case "Keyboard":
                KeyboardConversion();
                break;
        }
    }










    void TestText()
    {
        moveXT = GameObject.Find("LeftStickX").GetComponent<Text>();
        moveYT = GameObject.Find("LeftStickY").GetComponent<Text>();

        cameraXT = GameObject.Find("RightStickX").GetComponent<Text>();
        cameraYT = GameObject.Find("RightStickY").GetComponent<Text>();

        LTrigger = GameObject.Find("TriggerL").GetComponent<Text>();
        RTrigger = GameObject.Find("TriggerR").GetComponent<Text>();

        jumpButton = GameObject.Find("Button Jump").GetComponent<Text>();
        attackButton = GameObject.Find("Button Attack").GetComponent<Text>();


        DUp = GameObject.Find("D-Up").GetComponent<Text>();
        DDown = GameObject.Find("D-Down").GetComponent<Text>();

        DLeft = GameObject.Find("D-Left").GetComponent<Text>();
        DRight = GameObject.Find("D-Right").GetComponent<Text>();

        lButton = GameObject.Find("Button L").GetComponent<Text>();
        rButton = GameObject.Find("Button R").GetComponent<Text>();

        startButton = GameObject.Find("Button Start").GetComponent<Text>();
        selectButton = GameObject.Find("Button Select").GetComponent<Text>();


    }


    protected void XboxConversion()
    {
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


        #region D-X
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

        #region D-Y
        if (!d_Up)
        {
            if (Input.GetAxis("Axis7") > 0)
            {
                direction = "up";
            }
        }



        if (!d_Down)
        {
            if (Input.GetAxis("Axis7") < 0)
            {
                direction = "down";
            }
        }
        if (Input.GetAxis("Axis7") == 0)
        {
            d_Up = false;
            d_Down = false;
            direction = "none";
        }
            #endregion

        button_Jump = Input.GetKeyDown(KeyCode.JoystickButton1);
        button_Attack = Input.GetKeyDown(KeyCode.JoystickButton0);
        button_Action = Input.GetKeyDown(KeyCode.JoystickButton3);


        button_L = Input.GetKeyDown(KeyCode.JoystickButton4);
        button_R = Input.GetKeyDown(KeyCode.JoystickButton5);

        button_Start = Input.GetKeyDown(KeyCode.JoystickButton7);
        button_Select = Input.GetKeyDown(KeyCode.JoystickButton6);


        #endregion
    }


    protected void Ps4Conversion()
    {
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


        #region D-X
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

        #region D-Y
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

        button_Jump = Input.GetKeyDown(KeyCode.JoystickButton2);
        button_Attack = Input.GetKeyDown(KeyCode.JoystickButton1);
        button_Action = Input.GetKeyDown(KeyCode.JoystickButton3);

        button_L = Input.GetKeyDown(KeyCode.JoystickButton4);
        button_R = Input.GetKeyDown(KeyCode.JoystickButton5);

        button_Start = Input.GetKeyDown(KeyCode.JoystickButton9);
        button_Select = Input.GetKeyDown(KeyCode.JoystickButton13);
        #endregion
    }




    void KeyboardConversion()
    {
        #region Move
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
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

        #region Camera
        cameraX = Input.GetAxis("Mouse X");
        cameraY = Input.GetAxis("Mouse Y");
        #endregion

        #region D-Y
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










    void ControllerInput()
    {
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

