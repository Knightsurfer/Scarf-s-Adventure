using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour {


    protected bool testMode;
    public bool xboxController;
    public bool psController;


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


    private Text Axis7;
    private Text Axis8;
    private Text Axis9;
    private Text Axis10;
    private Text Axis11;
    private Text Axis12;
    private Text Axis13;
    private Text Axis14;
    private Text Axis15;
    private Text Axis16;
    private Text Axis17;
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
    protected bool d_Up;
    protected bool d_Down;
    protected bool d_Left;
    protected bool d_Right;
    protected bool button_L;
    protected bool button_R;

    protected bool button_Start;
    protected bool button_Select;

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
        if (xboxController)
        {
            XboxConversion();
        }
        if (psController)
        {
            Ps4Conversion();
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
        if(Input.GetAxis("Axis3") == 0)
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
        if (Input.GetAxis("Axis7") > 0)
        {
            d_Up = true;
        }
        if (Input.GetAxis("Axis7") < 0)
        {
            d_Down = true;
        }
        if (Input.GetAxis("Axis7") == 0)
        {
            d_Up = false;
            d_Down = false;
        }
        #endregion

        button_Jump = Input.GetKeyDown(KeyCode.JoystickButton0);
        button_Attack = Input.GetKeyDown(KeyCode.JoystickButton2);

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
            d_Up = true;
        }
        if (Input.GetAxis("Axis8") < 0)
        {
            d_Down = true;
        }
        if (Input.GetAxis("Axis8") == 0)
        {
            d_Up = false;
            d_Down = false;
        }
        #endregion

        button_Jump = Input.GetKeyDown(KeyCode.JoystickButton1);
        button_Attack = Input.GetKeyDown(KeyCode.JoystickButton0);

        button_L = Input.GetKeyDown(KeyCode.JoystickButton4);
        button_R = Input.GetKeyDown(KeyCode.JoystickButton5);

        button_Start = Input.GetKeyDown(KeyCode.JoystickButton9);
        button_Select = Input.GetKeyDown(KeyCode.JoystickButton8);
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

