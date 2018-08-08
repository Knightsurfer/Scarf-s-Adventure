
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RTS_Cam : RTS_Variables
{
   
    
    public void Cam_Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        scarf = GameObject.FindGameObjectWithTag("Player");
        skeleton = GameObject.FindGameObjectWithTag("Skeleton");


        camTransform = new Vector3(57f,0,44.5f);
        currentZoom = 4f;


       


    }
    public void Cam_Update()
    {
        OpenMenu();
        CursorCheck();
        RealTimeStratedgy();

   
           
        
                
    }



    #region RealTimeStratedgy
    void RealTimeStratedgy()
    {
        
                RealTimeKeyboard();
                RealTimeCamera();
                

    }
    void RealTimeKeyboard()
    {
        switch (EscapeMenuOpen)
        {
            case false:

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
                break;
        }

        
                currentZoom -= Input.GetAxis("Mouse ScrollWheel") * 4f;
                currentZoom = Mathf.Clamp(currentZoom, 2.4f, 8f);
                camTransform.x = Mathf.Clamp(camTransform.x, 10, 90);
                camTransform.z = Mathf.Clamp(camTransform.z, 10, 90);
           
    }
    void RealTimeCamera()
        {
            cam.transform.position = camTransform - new Vector3(0, -2, -2) * currentZoom;
            cam.transform.LookAt(camTransform + Vector3.up * 2f);
            cam.transform.RotateAround(camTransform, Vector3.up, currentYaw);
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
            

            EscapeMenuOpen = escape.activeSelf;


                
            
        }
    }

    public void QuitGame()
    {


        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
		Application.Quit();
        #endif
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
