
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMode : MonoBehaviour {
   
    
    #region Main Variables
    public string gameType;

    protected bool EscapeMenuOpen = false;
    public GameObject escape;
    #endregion

    #region RealTimeStratedgy Variables
    protected float panspeed = 20f;
    protected float panBorder = 10f;
    [HideInInspector]
    public LayerMask movementLayer;
    public LayerMask spritetLayer;
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


   



    




    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        neck = GameObject.Find("Neck");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (gameType == "RealTimeStratedgy")
        {
            camTransform = new Vector3(57f,0,44.5f);
            UnityEngine.Cursor.visible = true;
            currentZoom = 4f;
        }
        if (gameType != "RealTimeStratedgy")
        {
            currentZoom = 2f;
            UnityEngine.Cursor.visible = false;
        }
    }
    void Update()
    {
        OpenMenu();
        CursorCheck();


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

        }
    }


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
        currentZoom = Mathf.Clamp(currentZoom, 2.4f, 4f);
    }
    void ThirdPersonCamera()
    {
        neck.transform.localScale = new Vector3(1, 1, 1);
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
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, spritetLayer))
            {


                Debug.Log("We hit " + hit.collider.name + " " + hit.point);


            }

        }

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
