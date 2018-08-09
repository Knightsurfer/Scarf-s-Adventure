using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class RTS_Controller : RTS_Checks
{
    Transform target;
    public Interactable focus;
    public LayerMask itemLayer;




	void Start ()
    {
        Cam_Start();
	}
	

	void Update ()
    {
        Cam_Update();
        Check_Update();
        MainControls();

        Follow();

    }










    void MainControls()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, spriteLayer))
            {


                
                selectedUnit = GameObject.Find(hit.collider.name);
               









                #region commandPanel
                try   {
                        selected = GameObject.Find("Command Panel");
                    
                      }
                catch {
                        Debug.Log("No Command Panel Present");
                      }
                #endregion








                //Debug.Log("We hit " + hit.collider.name + " " + hit.point);
            }

        }

        if (selectedUnit != null)
        {

            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100, movementLayer))
                {
                    Movement();

                    

                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if(interactable != null)
                    {
                        SetFocus(interactable);

                    }

                    //Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                }






                if (Physics.Raycast(ray, out hit, 100, itemLayer))
                {
                    Movement();

                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }

                   
                }











            }
        }
    }


    void Movement()
    {
       
        remainingDistance = selectedUnit.GetComponent<NavMeshAgent>().remainingDistance;
        selectedUnit.GetComponent<Animator>().SetBool("Walking", true);
        point = hit.point;

        MoveToPoint(hit.point);

        RemoveFocus();
        



    }


    #region Focus
    void SetFocus (Interactable newFocus)
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
            //FaceTarget();
        }
    }






    void StartFollow (Interactable newTarget)
    {
        selectedUnit.GetComponent<NavMeshAgent>().stoppingDistance = newTarget.transform.localScale.x  *3;
        selectedUnit.GetComponent<NavMeshAgent>().updateRotation = false;
        target = newTarget.transform;

    }

    void StopFollow()
    {
        selectedUnit.GetComponent<NavMeshAgent>().stoppingDistance = 0;
        selectedUnit.GetComponent<NavMeshAgent>().updateRotation = true;
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
        selectedUnit.GetComponent<NavMeshAgent>().SetDestination(point);
    }


    #endregion

    /*void FaceTarget()
    {
        Vector3 direction = (point - selectedUnit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(0f, direction.y, 0f));

        selectedUnit.transform.rotation = Quaternion.Slerp(selectedUnit.transform.rotation,lookRotation, 8);
    }
    
    
    */
}
