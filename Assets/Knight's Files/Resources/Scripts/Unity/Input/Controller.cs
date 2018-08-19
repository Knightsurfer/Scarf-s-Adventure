using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {







    #region Text Test
    private Text moveXT;
    private Text moveYT;

    private Text LTrigger;
    private Text RTrigger;
    private Text cameraXT;
    private Text cameraYT;
    private Text DLeft;
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






    // Use this for initialization
    void Start()
    {
        

        XBoxText();
    }

    // Update is called once per frame
    void Update()
    {
        //XBoxText();

    }


    void TestText()
    {

        moveXT = GameObject.Find("Axis X").GetComponent<Text>();
        moveYT = GameObject.Find("Axis Y").GetComponent<Text>();

        LTrigger = GameObject.Find("Axis 3").GetComponent<Text>();
        RTrigger = GameObject.Find("Axis 3").GetComponent<Text>();
        cameraXT = GameObject.Find("Axis 4").GetComponent<Text>();
        cameraYT = GameObject.Find("Axis 5").GetComponent<Text>();
        DLeft = GameObject.Find("Axis 6").GetComponent<Text>();
        Axis7 = GameObject.Find("Axis 7").GetComponent<Text>();
        Axis8 = GameObject.Find("Axis 8").GetComponent<Text>();
        Axis9 = GameObject.Find("Axis 9").GetComponent<Text>();
        Axis10 = GameObject.Find("Axis 10").GetComponent<Text>();
        Axis11 = GameObject.Find("Axis 11").GetComponent<Text>();
        Axis12 = GameObject.Find("Axis 12").GetComponent<Text>();
        Axis13 = GameObject.Find("Axis 13").GetComponent<Text>();
        Axis14 = GameObject.Find("Axis 14").GetComponent<Text>();
        Axis15 = GameObject.Find("Axis 15").GetComponent<Text>();
        Axis16 = GameObject.Find("Axis 16").GetComponent<Text>();
        Axis17 = GameObject.Find("Axis 17").GetComponent<Text>();








        moveXT.text = "Axis X: " + Input.GetAxis("Horizontal").ToString();
        moveYT.text = "Axis Y: " + Input.GetAxis("Vertical").ToString();
        LTrigger.text = "Axis 3: " + Input.GetAxis("Axis3").ToString();
        RTrigger.text = "Axis 3: " + Input.GetAxis("Axis3").ToString();
        cameraXT.text = "Axis 4: " + Input.GetAxis("Axis4").ToString();
        cameraYT.text = "Axis 5: " + Input.GetAxis("Axis5").ToString();
        DLeft.text = "Axis 6: " + Input.GetAxis("Axis6").ToString();





    }
    void XBoxText()
    {
        moveXT = GameObject.Find("LeftStickX").GetComponent<Text>();
        moveYT = GameObject.Find("LeftStickY").GetComponent<Text>();

        cameraXT = GameObject.Find("RightStickX").GetComponent<Text>();
        cameraYT = GameObject.Find("RightStickY").GetComponent<Text>();






        LTrigger = GameObject.Find("TriggerL").GetComponent<Text>();
        RTrigger = GameObject.Find("TriggerR").GetComponent<Text>();
        
        DLeft = GameObject.Find("Axis 6").GetComponent<Text>();
        Axis7 = GameObject.Find("Axis 7").GetComponent<Text>();
        Axis8 = GameObject.Find("Axis 8").GetComponent<Text>();
        Axis9 = GameObject.Find("Axis 9").GetComponent<Text>();
        Axis10 = GameObject.Find("Axis 10").GetComponent<Text>();
        Axis11 = GameObject.Find("Axis 11").GetComponent<Text>();
        Axis12 = GameObject.Find("Axis 12").GetComponent<Text>();
        Axis13 = GameObject.Find("Axis 13").GetComponent<Text>();
        Axis14 = GameObject.Find("Axis 14").GetComponent<Text>();
        Axis15 = GameObject.Find("Axis 15").GetComponent<Text>();
        Axis16 = GameObject.Find("Axis 16").GetComponent<Text>();
        Axis17 = GameObject.Find("Axis 17").GetComponent<Text>();
        XboxConversion();

    }


   protected void XboxConversion()
    {
        #region Axis
        #region Control Sticks
        moveX = Input.GetAxis("Horizontal");
        moveY = -Input.GetAxis("Vertical");
        
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
        if(Input.GetAxis("Axis6") == 1)
        {
            

        }


        button_Jump = Input.GetKeyDown(KeyCode.Joystick1Button0);
        #endregion




        //ControllerInput();
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
        DLeft.text = "D-Left: " + Input.GetAxis("Axis6").ToString();
        #endregion
    }









}

