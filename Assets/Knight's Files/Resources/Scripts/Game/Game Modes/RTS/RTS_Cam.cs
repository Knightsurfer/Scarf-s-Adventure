
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RTS_Cam : RTS_Variables
{
   
    










   







    public void Cam_Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");

     
        scarf = GameObject.FindGameObjectWithTag("Player").GetComponent<ScarfController>();


        camTransform = new Vector3(57f,0,44.5f);
        UnityEngine.Cursor.visible = true;
        currentZoom = 4f;


       


    }
    public void Cam_Update()
    {
        OpenMenu();
        CursorCheck();
        RealTimeStratedgy();

        if (GetComponent<NavMeshAgent>())
        {
            remainingDistance.text = "Remaining Distance: " + (int)scarf.agent.remainingDistance;
        }
                
    }



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
            if (scarf.agent.remainingDistance == 0)
            {
                scarf.GetComponent<Animator>().SetBool("Walking", false);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            

            if (Physics.Raycast(ray, out hit, movementLayer))
            {
                if (GetComponent<NavMeshAgent>())
                {
                    scarf.GetComponent<Animator>().SetBool("Walking", true);
                    scarf.MoveToPoint(hit.point);
                    point = hit.point;
                }
                Debug.Log("We hit " + hit.collider.name + " " + hit.point );


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
