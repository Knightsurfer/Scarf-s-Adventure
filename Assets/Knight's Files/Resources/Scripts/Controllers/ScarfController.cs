using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class ScarfController : MonoBehaviour
{

    NavMeshAgent agent;
    CameraMode cameraChecker;


	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        cameraChecker = GameObject.Find("Game Manager").GetComponent<CameraMode>();
	}
	
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }






	void Update ()
    {
		
	}
}
