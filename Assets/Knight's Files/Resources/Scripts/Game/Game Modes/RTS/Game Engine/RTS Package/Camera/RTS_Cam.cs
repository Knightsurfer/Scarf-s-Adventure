using UnityEngine;
using UnityEngine.SceneManagement;

public class RTS_Cam : MonoBehaviour
{
    #region Variables


    private GameObject player;

    protected Camera cam;
    protected PauseMenu pause;
    public Vector3 camLocation;

    protected float maxX;
    protected float minX;

    protected float maxY;
    protected float minY;

    protected float currentYaw = 180f;
    protected float currentZoom = 2f;

    protected float panspeed = 20f;
    protected float panBorder = 10f;

    

    #endregion

    #region Main Routines
    #region Standalone
    private void Start()
    {
        Cam_Start();
    }
    private void Update()
    {
        Cam_Update();
    }
    #endregion

    protected void Cam_Start()
    {
        pause = GameObject.Find("Pause Menu").GetComponent<PauseMenu>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cam.transform.SetParent(GameObject.Find("Game Manager").transform);
        
        currentZoom = 4f;
        player = GameObject.Find("Scarf");


        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            camLocation.x = 142;
            camLocation.y = -75;
            camLocation.z = -391;

            player.transform.localPosition = new Vector3(48, -0, -45);
            
        }




    }
    protected void Cam_Update()
    {
        
            RealTimeStratedgy();
        
    }
    #endregion


    #region Camera Movement
    protected void RealTimeStratedgy()
    {
        RealTimeKeyboard();
        RealTimeCamera(); 
    }

    protected void RealTimeCamera()
    {
        cam.transform.position = camLocation - new Vector3(0, -2, -2) * currentZoom;
        cam.transform.LookAt(camLocation + Vector3.up * 2f);
        cam.transform.RotateAround(camLocation, Vector3.up, currentYaw);
    }
    protected void RealTimeKeyboard()
    {
        if (!pause.paused)
        {
        
                if (Input.mousePosition.y >= Screen.height - panBorder)
                {
                    camLocation.z += panspeed * Time.deltaTime;
                }
                if (Input.mousePosition.y <= panBorder)
                {
                    camLocation.z -= panspeed * Time.deltaTime;
                }
                if (Input.mousePosition.x >= Screen.width - panBorder)
                {
                    camLocation.x += panspeed * Time.deltaTime;
                }
                if (Input.mousePosition.x <= panBorder)
                {
                    camLocation.x -= panspeed * Time.deltaTime;
                }

                currentZoom -= Input.GetAxis("Mouse ScrollWheel") * 4f;
        }
        currentZoom = Mathf.Clamp(currentZoom, 2.4f, 8f);


        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            camLocation.x = Mathf.Clamp(camLocation.x, 100f, 180);
            camLocation.z = Mathf.Clamp(camLocation.z, -370, -270);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            camLocation.x = Mathf.Clamp(camLocation.x, 10, 90);
            camLocation.z = Mathf.Clamp(camLocation.z, 10, 90);
        }

    }
    #endregion
}
