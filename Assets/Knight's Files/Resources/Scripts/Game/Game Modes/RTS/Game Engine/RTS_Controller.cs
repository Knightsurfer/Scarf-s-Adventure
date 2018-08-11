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
           selectedUnit.GetComponent<Player_Controller>().OnHealthChanged += OnHealthChanged;
        }

    }

    void ShootRay(LayerMask layer)
    {
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        

        if (Physics.Raycast(ray, out hit, 100, layer))
        {
            

            if (layer == spriteLayer)
            {
                selectedUnit = GameObject.Find(hit.collider.name);
            }



           if (layer == itemLayer)
            {
                
                selectedUnit.GetComponent<Player_Controller>().Movement(hit.point);

                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    selectedUnit.GetComponent<Player_Controller>().SetFocus(interactable);
                }
            }

           if (layer == movementLayer)
            {
                
                selectedUnit.GetComponent<Player_Controller>().Movement(hit.point);


            }

        }



    }


    void MainControls()
        {

            #region Unit Select
            if (Input.GetMouseButtonDown(0))
            {
                ShootRay(spriteLayer);
            }
        
        #endregion
        #region Remote Control
        if (selectedUnit != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                ShootRay(movementLayer);
                ShootRay(itemLayer);

            }
        }
        #endregion
    }
}