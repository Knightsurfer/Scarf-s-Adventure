using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_Controller : Unit_Stats
{
    protected RTS_Controller RTS;
    protected Transform target;
    protected Interactable focus;
    NavMeshAgent nav;



    protected void Start()
    {
        Assign_Stats();
        RTS = GameObject.Find("Game Manager").GetComponent<RTS_Controller>();
        nav = GetComponent<NavMeshAgent>();
    }
    protected void Update()
    {
        Animation();
        Follow();
    }


    protected void Animation()
    {
        if ((int)nav.remainingDistance > -1 && (int)nav.remainingDistance <= nav.stoppingDistance || target != null && nav.remainingDistance == (int)focus.radius)
        {
            GetComponent<Animator>().SetBool("Walking", false);
            
        }
        else
        {
            GetComponent<Animator>().SetBool("Walking", true);
        }

        if ((int)nav.remainingDistance == nav.stoppingDistance)
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            
        }
        else if((int)nav.remainingDistance != nav.stoppingDistance)
        {
            GetComponent<NavMeshAgent>().isStopped = false;
            
        }
    }
    public void Movement(Vector3 point)
    {
        nav.SetDestination(point);
        RemoveFocus();
    }



    #region Following
    public void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
        StartFollow(newFocus);
    }
    protected void RemoveFocus()
    {
        focus = null;
        StopFollow();
    }

    void Follow()
    {
        if (target != null)
        {
            nav.SetDestination(target.position);
            if (nav.remainingDistance >= nav.stoppingDistance)
            {
                GetComponent<Animator>().SetBool("Walking", true);
            }
            if (nav.remainingDistance <= nav.stoppingDistance - 1)
            {
                GetComponent<Animator>().SetBool("Walking", false);
            }
        }
    }
    void StartFollow(Interactable newTarget)
    {
        nav.stoppingDistance = 2 + focus.radius;
        target = newTarget.transform;
    }
    void StopFollow()
    {
        nav.stoppingDistance = 0;
        target = null;
    }
    #endregion
}


