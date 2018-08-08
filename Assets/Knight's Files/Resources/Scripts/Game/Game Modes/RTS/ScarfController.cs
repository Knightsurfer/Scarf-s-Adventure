using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ScarfController : RTS_Cam
{
   


	void Start ()
    {
        Cam_Start();
        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
	}
	
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }






	void Update ()
    {
        Cam_Update();
	}
}
