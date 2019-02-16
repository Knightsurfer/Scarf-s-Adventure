using UnityEngine;
using UnityEngine.SceneManagement;

namespace KnightsControls
{
    public class Gamepad : Controller
    {
        protected void GamepadUpdate()
        {
            ControllerDetect();
            ControllerCheck();
        }
        private void StandaloneUpdate()
        {
            GamepadUpdate();
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
        public string controller;
        [HideInInspector] public bool isGamepad;
        protected string selectedController;
        [HideInInspector]public string[] currentControllers = new string[] { };

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
        [HideInInspector] public bool button_Kick;

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

                case "Snes":
                    isGamepad = true;
                    SnesConversion();
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
                    if (isTest)
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

            if (isTest)
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

        #endregion


        private void SnesConversion()
        {

            button_Attack = Input.GetKeyDown(KeyCode.JoystickButton4); //ButtonY
            button_Kick = Input.GetKeyDown(KeyCode.JoystickButton0); //ButtonA
            button_Jump = Input.GetKeyDown(KeyCode.JoystickButton1); //ButtonB
            button_Action = Input.GetKeyDown(KeyCode.JoystickButton3); //ButtonX

            button_L = Input.GetKeyDown(KeyCode.JoystickButton6); //ButtonL
            button_R = Input.GetKeyDown(KeyCode.JoystickButton7); //ButtonR

            button_Select = Input.GetKeyDown(KeyCode.JoystickButton10); //ButtonSelect
            button_Start = Input.GetKeyDown(KeyCode.JoystickButton11); //ButtonStart

            moveX = Input.GetAxis("Horizontal");
            moveY = Input.GetAxis("Vertical");

            d_Down = Input.GetKeyDown(KeyCode.JoystickButton0); //ButtonA

            if (Input.GetKey(KeyCode.JoystickButton6))
            {
                cameraX = -1;
            }

            if (Input.GetKey(KeyCode.JoystickButton7))
            {
                cameraX = 1;
            }

            if (!Input.GetKey(KeyCode.JoystickButton6) && !Input.GetKey(KeyCode.JoystickButton7))
            {
                cameraX = 0;
            }

        }


        protected void ControllerInput()
        {



        }

        public void LoadScene()
        {
            SceneManager.LoadScene(0);
        }

    }
}