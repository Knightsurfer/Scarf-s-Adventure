using UnityEngine;
using UnityEngine.AI;

public class RTS_Mode : RTS_Checks
{
    #region Variables


    protected NavMeshAgent nav;
    protected RaycastHit hit;

    protected LayerMask movementLayer = 4096;
    protected LayerMask unitLayer = 24576;
    #endregion

    protected void Start()
    {
        Cam_Start();
    }
    protected void Update()
    {
        MainControls();
        Check_Update();
    }

    protected void MainControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootRay("Left", cam.ScreenPointToRay(Input.mousePosition));
        }



        if (selectedUnit != null)
        {
            if (selectedUnit.GetComponent<Unit_Controller>().Dead)
            {
                selectedUnit = null;
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (selectedUnit != null)
                {
                    ShootRay("Right", cam.ScreenPointToRay(Input.mousePosition));
                }
            }
            if (Input.GetKey(KeyCode.F1))
            {
                unit.TakeDamage(5, 0);
            }
            if (Input.GetKey(KeyCode.F2))
            {
                unit.TakeDamage(0, 5);
            }
            if (Input.GetKey(KeyCode.F3))
            {
                if (!unit.stats.isProp)
                {
                    unit.EXP++;
                }
            }
        }
    }
    protected void ShootRay(string mouseClick, Ray ray)
    {
        if (Physics.Raycast(ray, out hit, 100, unitLayer))
        {
            switch (mouseClick)
            {
                case "Left":
                    selectedUnit = hit.collider.gameObject;
                    break;

                case "Right":
                        Unit_Controller followTarget = GameObject.Find(hit.collider.name).GetComponent<Unit_Controller>();
                        selectedUnit.GetComponent<Unit_Controller>().SetFocus(followTarget);
                    break;
            }
        }
        else if (Physics.Raycast(ray, out hit, 100, movementLayer))
        {
            switch (mouseClick)
            {
                case "Left":

                    break;

                case "Right":
                    selectedUnit.GetComponent<Unit_Controller>().Movement(hit.point);
                    break;
            }
        }
    }
}