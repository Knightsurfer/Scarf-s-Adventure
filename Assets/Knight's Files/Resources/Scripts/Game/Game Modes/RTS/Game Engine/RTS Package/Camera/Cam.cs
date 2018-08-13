using UnityEngine;

public class RTS_Cam : MonoBehaviour
{
    #region Variables

    protected GameObject cam;
    protected PauseMenu pause;
    protected Vector3 camLocation;
    

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
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        camLocation = new Vector3(47.5f, 0, 30f);
        currentZoom = 4f;
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
        camLocation.x = Mathf.Clamp(camLocation.x, 10, 90);
        camLocation.z = Mathf.Clamp(camLocation.z, 10, 90);
    }
    #endregion
}
