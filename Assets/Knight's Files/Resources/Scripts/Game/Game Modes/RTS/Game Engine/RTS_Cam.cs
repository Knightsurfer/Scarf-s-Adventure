using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class RTS_Cam : RTS_Variables
{
    #region Start
    protected void Cam_Start()
    {
        Variables();
    }
    #region Starter Variables
    void Variables()
    {
        
        selectedUnit = null;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        camLocation = new Vector3(47.5f, 0, 30f);
        currentZoom = 4f;
        skeleton = GameObject.FindGameObjectWithTag("Skeleton");
    }

    #endregion


    #endregion
    #region Update
    protected void Cam_Update()
    {
        PauseCheck();
        RealTimeStratedgy();
    }
    protected void RealTimeStratedgy()
    {
        RealTimeKeyboard();
        RealTimeCamera();
    }
    #endregion



    #region Main Engine
    void RealTimeKeyboard()
    {
        switch (EscapeMenuOpen)
        {
            case false:

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
                break;
        }
        currentZoom = Mathf.Clamp(currentZoom, 2.4f, 8f);
        camLocation.x = Mathf.Clamp(camLocation.x, 10, 90);
        camLocation.z = Mathf.Clamp(camLocation.z, 10, 90);
    }
    void RealTimeCamera()
        {
            cam.transform.position = camLocation - new Vector3(0, -2, -2) * currentZoom;
            cam.transform.LookAt(camLocation + Vector3.up * 2f);
            cam.transform.RotateAround(camLocation, Vector3.up, currentYaw);
        }
    #endregion


    #region Pause Menu
    void PauseCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            try
            {
                pausePanel = GameObject.Find("Pause Menu").GetComponent<Canvas>();
                switch (pausePanel.enabled)
                {
                    case true:
                        pausePanel.enabled = false;
                        break;

                    case false:
                        pausePanel.enabled = true;
                        break;
                }
                EscapeMenuOpen = pausePanel.enabled;
            }
            catch    
            {
                Debug.Log("No Pause Menu");
            }
        }
    }
    //##########################//
    public void RestartLevel()
    {
            Scene loadedLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedLevel.buildIndex);
    }
    public void QuitGame()
    {

		Application.Quit();
    }
    #endregion
}
