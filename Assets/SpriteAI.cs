using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class SpriteAI : MonoBehaviour
{
    public int remainingDistance;
    public bool triggered;
    public int phase;
    NavMeshAgent nav;

    
    void Start ()
    {
        remainingDistance = 1;
        nav = GetComponent<NavMeshAgent>();
	}
	

	void Update ()
    {
        
        if (triggered)
        {
            triggered = false;
            phase++;
        }

        if(remainingDistance == 2)
        {
            GetComponent<SphereCollider>().enabled = true;
        }




        switch(phase)
        {

            case 1:
                Move(93,transform.position.y,- 260);
                remainingDistance = (int)nav.remainingDistance;
                break;


            case 2:
                Move(155,transform.position.y,-264);
                remainingDistance = (int)nav.remainingDistance;
                break;

            
                


        }
	}

    private void Move(int x,float y,int z)
    {
        nav.SetDestination(new Vector3(x, y, z));
    }









}
