using UnityEngine;
using UnityEngine.AI;

public class RTS_Mode : RTS_Checks
{
    #region Variables
    protected NavMeshAgent nav;
    protected RaycastHit hit;
    protected Vector3 point;

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
        #region Unit Select
        if (Input.GetMouseButtonDown(0))
            {
                ShootRay(unitLayer,false);
            }
        #endregion
        #region Remote Control
        if (selectedUnit != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                ShootRay(movementLayer,true);
                //ShootRay(itemLayer,true);
            }
        }
        #endregion
    }
    protected void ShootRay(LayerMask layer, bool moving)
    {
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100, layer))
        {
            if (layer == unitLayer)
            {
                if (!moving)
                {
                    selectedUnit = GameObject.Find(hit.collider.name);
                    if (selectedUnit.GetComponent<Unit_Controller>().nav == null)
                    {
                        EnableMovement();
                    }
                }
                else
                {
                    selectedUnit.GetComponent<Unit_Controller>().Movement(hit.point);
                    Unit_Interact interactable = hit.collider.GetComponent<Unit_Interact>();
                    if (interactable != null)
                    {
                        selectedUnit.GetComponent<Unit_Controller>().SetFocus(interactable);
                    }
                }
            }
            if (layer == movementLayer)
            {
                selectedUnit.GetComponent<Unit_Controller>().Movement(hit.point);
            }
        }
    }
    protected void EnableMovement()
    {
        if (selectedUnit.GetComponent<Unit_Controller>().canMove)
        {
            selectedUnit.AddComponent<NavMeshAgent>();
            nav = selectedUnit.GetComponent<NavMeshAgent>();
            nav.speed = 7;
            nav.angularSpeed = 1200;
            nav.acceleration = 20;
            nav.areaMask = 5;
            nav.autoBraking = false;
        }
    }



















}