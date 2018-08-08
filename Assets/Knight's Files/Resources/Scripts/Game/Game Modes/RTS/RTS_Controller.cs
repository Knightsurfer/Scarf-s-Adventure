using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class RTS_Controller : RTS_Cam
{
   


	void Start ()
    {
        Cam_Start();
        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
	}
	

	void Update ()
    {
        Cam_Update();
        CursorMessage();
        remainingDistance = (int)scarf.GetComponent<NavMeshAgent>().remainingDistance;
    }



    void Outline()
    {
        
        

    }


    void CursorMessage()
    {

        

        if (scarf.GetComponent<NavMeshAgent>().remainingDistance == 0)
            {
                scarf.GetComponent<Animator>().SetBool("Walking", false);
            }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, spriteLayer))
            {



                Debug.Log("We hit " + hit.collider.name + " " + hit.point);
            }
        }

        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, movementLayer))
            {
                    remainingDistance = scarf.GetComponent<NavMeshAgent>().remainingDistance;
                    scarf.GetComponent<Animator>().SetBool("Walking", true);
                    point = hit.point;
                    
                    MoveToPoint(hit.point);
                    //FaceTarget();


                Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                
            }
        }
    }



    #region Movement pointer.


    public void PlayerMovement()
    {
        Vector3 destination = hit.point;
        MoveToPoint(destination);

    }

    public void MoveToPoint(Vector3 point)
    {
        scarf.GetComponent<NavMeshAgent>().SetDestination(point);
    }


    void FaceTarget()
    {
        Vector3 direction = (point - scarf.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        skeleton.transform.LookAt (direction);
    }
    #endregion

}
