using UnityEngine;

public class Hybrid_Cam : MonoBehaviour
{
    #region Variables

    protected GameObject cam;
    protected PauseMenu pause;
    protected Vector3 camLocation;
    Vector3 targetLocation;

    GameObject target;



    protected float currentYaw = 180f;
    protected float currentZoom = 2f;

    protected float panspeed = 20f;
    protected float panBorder = 10f;
    public float camSpeed = 7.5f;
    

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
        currentZoom = 4f;
        target = GameObject.FindGameObjectWithTag("Player");
        camLocation = new Vector3(target.transform.position.x, 0, target.transform.position.z);
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
            camLocation.x += Input.GetAxis("LeftStickX")* Time.deltaTime * camSpeed;
            camLocation.z += Input.GetAxis("LeftStickY") * Time.deltaTime * camSpeed;
            currentZoom -= Input.GetAxis("RightStickX") * 4f;


        }
        currentZoom = Mathf.Clamp(currentZoom, 2.4f, 8f);
        camLocation.x = Mathf.Clamp(camLocation.x, -20, 120);
        camLocation.z = Mathf.Clamp(camLocation.z, -10, 120);










    }
    #endregion
}
