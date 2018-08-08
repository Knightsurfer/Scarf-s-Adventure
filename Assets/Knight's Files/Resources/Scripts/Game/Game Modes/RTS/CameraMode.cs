
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraMode : MonoBehaviour {
   
    
    #region Main Variables
    public string gameType = "RealTimeStratedgy";

    protected bool EscapeMenuOpen = false;
    public GameObject escape;
    #endregion

    #region RealTimeStratedgy Variables
    protected float panspeed = 20f;
    protected float panBorder = 10f;
    public LayerMask movementLayer;
    public LayerMask spriteLayer;
    RaycastHit hit;
    Vector3 point;
    public Text remainingDistance;
    #endregion

    #region ThirdPerson Variables
    protected GameObject cam;
    protected Vector3 camTransform;
    protected Transform target;
    protected GameObject neck;

    Quaternion camvar;

    protected float currentYaw = 180f;
    protected float currentZoom  = 2f;
    #endregion

    #region Spectate Variables
    public float speedNormal = 10.0f;
    public float speedFast = 50.0f;

    public float mouseSensitivityX = 5.0f;
    public float mouseSensitivityY = 5.0f;

    float rotY = 0.0f;
    #endregion

    //ScarfController scarf;







    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        neck = GameObject.Find("Neck");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (gameType == "RealTimeStratedgy" || gameType == "Misc")
        {
            camTransform = new Vector3(57f,0,44.5f);
            UnityEngine.Cursor.visible = true;
            currentZoom = 4f;
        }


        if (gameType != "RealTimeStratedgy" || gameType != "Misc")
        {
            currentZoom = 2f;
            UnityEngine.Cursor.visible = false;
        }

        //scarf = GameObject.FindGameObjectWithTag("Player").GetComponent<ScarfController>();


    }
    void Update()
    {
        /*
        OpenMenu();
        CursorCheck();
        if (GetComponent<NavMeshAgent>())
        {
            remainingDistance.text = "Remaining Distance: " + (int)scarf.agent.remainingDistance;
        }


        switch (gameType)
        {
            case "FirstPerson":
                FirstPerson();
                break;

            case "ThirdPerson":
                ThirdPerson();
                break;

            case "RealTimeStratedgy":
                RealTimeStratedgy();
                
                break;

            case "Spectate":
                Spectate();
                break;

            case "Misc":
                UnityEngine.Cursor.visible = true;
                Misc();
                MiscKeyboard();
                break;

   
        }
         */
    }


    #region Spectator
    // rotation      
    void Spectate()
    {
        switch (EscapeMenuOpen)
        {
            case false:
                if (Input.GetMouseButton(1))
                {
                    float rotX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivityX;
                    rotY += Input.GetAxis("Mouse Y") * mouseSensitivityY;
                    rotY = Mathf.Clamp(rotY, -89.5f, 89.5f);
                    transform.localEulerAngles = new Vector3(-rotY, rotX, 0.0f);
                }

                if (Input.GetKey(KeyCode.U))
                {
                    gameObject.transform.localPosition = new Vector3(0.0f, 3500.0f, 0.0f);
                }

                float forward = Input.GetAxis("Vertical");
                float strafe = Input.GetAxis("Horizontal");

                // move forwards/backwards
                if (forward != 0.0f)
                {
                    float speed = Input.GetKey(KeyCode.LeftShift) ? 50 : 10;
                    Vector3 trans = new Vector3(0.0f, 0.0f, forward * speed * Time.deltaTime);
                    gameObject.transform.localPosition += gameObject.transform.localRotation * trans;
                }

                // strafe left/right
                if (strafe != 0.0f)
                {
                    float speed = Input.GetKey(KeyCode.LeftShift) ? 50 : 10;
                    Vector3 trans = new Vector3(strafe * speed * Time.deltaTime, 0.0f, 0.0f);
                    gameObject.transform.localPosition += gameObject.transform.localRotation * trans;
                }
                break;
        }
    }
    #endregion


    #region FirstPerson
    void FirstPerson()
    {
        switch (EscapeMenuOpen)
        {
            case false:
                FirstPersonKeyboard();
                FirstPersonCamera();
                break;
        }
    }
    void FirstPersonKeyboard()
    {
            camvar.x += Input.GetAxis("Mouse Y") * Time.deltaTime;
            camvar.y -= Input.GetAxis("Mouse X") * Time.deltaTime;
            camvar.z = cam.transform.rotation.z;
            camvar.w = cam.transform.rotation.w;
    }
    void FirstPersonCamera()
    {
        neck.transform.localScale = new Vector3(0, 0, 0);
        cam.transform.position = target.transform.position + new Vector3(0, 1.2f, -0.2f);
        cam.transform.rotation = camvar;
    }
    #endregion
    #region ThirdPerson
    void ThirdPerson()
    {
        switch (EscapeMenuOpen)
        {
            case false:
                ThirdPersonKeyboard();
                ThirdPersonCamera();
                break;
        }
    }
    void ThirdPersonKeyboard()
    {
        currentYaw += Input.GetAxis("Mouse X") * 100f * Time.deltaTime;
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * 4f;

        currentYaw += Input.GetAxis("RightStickY") * 100f * Time.deltaTime;
        currentZoom -= Input.GetAxis("RightStickX") * 4f;


    }
    void ThirdPersonCamera()
    {
        neck.transform.localScale = new Vector3(1, 1, 1);
        currentZoom = Mathf.Clamp(currentZoom, 2.4f, 4f);
        cam.transform.position = target.position - new Vector3(0, -2, -2) * currentZoom;
        cam.transform.LookAt(target.position + Vector3.up * 2f);

        cam.transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
    #endregion
    #region RealTimeStratedgy
    void RealTimeStratedgy()
    {
        switch (EscapeMenuOpen)
        {
            case false:
                RealTimeKeyboard();
                RealTimeCamera();
                CursorMessage();
                break;
        }
    }
    void RealTimeKeyboard()
    {
        if (Input.mousePosition.y >= Screen.height - panBorder)
        {
            camTransform.z += panspeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= panBorder)
        {
            camTransform.z -= panspeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - panBorder)
        {
            camTransform.x += panspeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= panBorder)
        {
            camTransform.x -= panspeed * Time.deltaTime;
        }

        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * 4f;
        currentZoom = Mathf.Clamp(currentZoom, 2.4f, 8f);
        camTransform.x = Mathf.Clamp(camTransform.x, 10, 90);
        camTransform.z = Mathf.Clamp(camTransform.z, 10, 90);
        neck.transform.localScale = new Vector3(1, 1, 1);
    }
    void RealTimeCamera()
        {
            cam.transform.position = camTransform - new Vector3(0, -2, -2) * currentZoom;
            cam.transform.LookAt(camTransform + Vector3.up * 2f);
            cam.transform.RotateAround(camTransform, Vector3.up, currentYaw);
        }
    void CursorMessage()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100, spriteLayer))
            {

               
                
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);
            }

          

        }
        if (GetComponent<NavMeshAgent>())
        {
            /*
            if (scarf.agent.remainingDistance == 0)
            {
                scarf.GetComponent<Animator>().SetBool("Walking", false);
            }
            */
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            

            if (Physics.Raycast(ray, out hit, movementLayer))
            {
                if (GetComponent<NavMeshAgent>())
                {
                    //scarf.GetComponent<Animator>().SetBool("Walking", true);
                    //scarf.MoveToPoint(hit.point);
                    point = hit.point;
                }
                Debug.Log("We hit " + hit.collider.name + " " + hit.point );


            }

        }
         
    }
    #endregion

    #region ThirdPerson
    void Misc()
    {
        switch (EscapeMenuOpen)
        {
            case false:
                MiscKeyboard();
                ThirdPersonCamera();
                CursorMessage();
                break;
        }
    }



    #endregion



    #region Misc
    void MiscKeyboard()
    {
            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * 4f;
            currentZoom = Mathf.Clamp(currentZoom, 1f, 8f);
    }
    void MiscCamera()
    {
        neck.transform.localScale = new Vector3(1, 1, 1);
        cam.transform.position = target.position - new Vector3(0, -2, -2) * currentZoom;
        cam.transform.LookAt(target.position + Vector3.up * 2f);

        cam.transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
    #endregion








    #region Pause Menu
    void OpenMenu()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            escape.gameObject.SetActive(!escape.activeSelf);
        }


    }
    void CursorCheck()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Application.isEditor)
            {

                switch (EscapeMenuOpen)
                {
                    case true:

                        EscapeMenuOpen = false;
                        UnityEngine.Cursor.visible = false;
                        break;

                    case false:
                        EscapeMenuOpen = true;
                        UnityEngine.Cursor.visible = true;
                        #if !UNITY_WEBGL
                        System.Windows.Forms.Cursor.Position = new System.Drawing.Point(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2);
                        #endif
                        break;
                }
            }
        }
    }

    public void QuitGame()
    {
        UnityEngine.Application.Quit();
    }
    public void RestartLevel()
    {
        if (!UnityEngine.Application.isEditor)
        {
            Scene loadedLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedLevel.buildIndex);
        }
    }
    #endregion






}
