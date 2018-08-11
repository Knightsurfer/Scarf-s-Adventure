using UnityEngine;

public class RTS_Controller : RTS_Checks
{

    void Start()
    {
        Cam_Start();       
    }


    void Update()
    {
        Cam_Update();
        Check_Update();
        MainControls();

        if (selectedUnit != null)
        {
           selectedUnit.GetComponent<Unit_Stats>().OnHealthChanged += OnHealthChanged;
            
        }

    }


    void MainControls()
    {
 
        #region Unit Select
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, spriteLayer))
            {
                selectedUnit = GameObject.Find(hit.collider.name);

                
            }
        }
        #endregion
        #region Remote Control
        if (selectedUnit != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                selectedUnit.GetComponent<Player_Controller>().MainControls();

            }
        }
        #endregion
    }
}