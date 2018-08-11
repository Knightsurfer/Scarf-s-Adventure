using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_Controller : Interactable
{
    protected RTS_Controller RTS;
    protected Transform target;
    protected Interactable focus;

    protected LayerMask itemLayer;
    protected LayerMask movementLayer;









    Camera cam;
    public float remainingDistance;

    protected RaycastHit hit;
    protected Vector3 point;

    private void Start()
    {
        RTS = GameObject.Find("Game Manager").GetComponent<RTS_Controller>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        itemLayer = RTS.itemLayer;
        movementLayer = RTS.movementLayer;








    }


    private void Update()
    {
        Selected();
        Follow();
    }



    protected void Selected()
    {
        
            remainingDistance = (int)GetComponent<NavMeshAgent>().remainingDistance;
            if (GetComponent<NavMeshAgent>().remainingDistance >= 0 && GetComponent<NavMeshAgent>().remainingDistance <= GetComponent<NavMeshAgent>().stoppingDistance)
            {
                GetComponent<Animator>().SetBool("Walking", false);
            }
        
    }







    public void MainControls()
    {

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100, RTS.movementLayer))
        {
            Movement();



            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                SetFocus(interactable);

            }

        }

        if (Physics.Raycast(ray, out hit, 100, RTS.itemLayer))
        {
            Movement();

            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                SetFocus(interactable);
            }


        }

    }









            
        
    


    void Movement()
    {

        remainingDistance = GetComponent<NavMeshAgent>().remainingDistance;
        GetComponent<Animator>().SetBool("Walking", true);
        point = hit.point;

        MoveToPoint(hit.point);

        RemoveFocus();




    }


    #region Focus
    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
        StartFollow(newFocus);
    }

    void RemoveFocus()
    {
        focus = null;
        StopFollow();
    }
    #endregion


    #region Following


    void Follow()
    {
        if (target != null)
        {
            MoveToPoint(target.position);
            // FaceTarget();
        }
    }






    void StartFollow(Interactable newTarget)
    {
        GetComponent<NavMeshAgent>().stoppingDistance = 2 + focus.radius;
        // selectedUnit.GetComponent<NavMeshAgent>().updateRotation = false;
        target = newTarget.transform;

    }

    void StopFollow()
    {
        GetComponent<NavMeshAgent>().stoppingDistance = 0;
        //selectedUnit.GetComponent<NavMeshAgent>().updateRotation = true;
        target = null;
    }
    #endregion







    #region Movement pointer.


    public void PlayerMovement()
    {
        Vector3 destination = hit.point;
        MoveToPoint(destination);

    }

    public void MoveToPoint(Vector3 point)
    {
        GetComponent<NavMeshAgent>().SetDestination(point);
    }


    #endregion

}


