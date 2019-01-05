using UnityEngine;
using UnityEngine.AI;

public class Unit_Controller : Unit_Stats
{
    
    protected Unit_Controller focus;
    protected Transform target;















    protected void Start()
    {
        Stats_Start();
        Radius_Start();

       
       
        nav = GetComponent<NavMeshAgent>();
        
    }
    protected void Update()
    {
        Stats_Update();
        if (canMove)
        {
            Animation(null);
            if (canMove)
            {
                Follow();
            }
        }
    }

    #region RTS Mode

    #region Movement
    public void Movement(Vector3 point)
    {
        RemoveFocus();
        if (canMove)
        {
            
            nav = GetComponent<NavMeshAgent>();
            nav.SetDestination(point);
        }
    }


    protected void Animation(string action)
    {
        Animator anim = GetComponent<Animator>();
        switch (action)
        {
            case null:
                if (nav != null)
                {
                    
                    if ((int)nav.remainingDistance == nav.stoppingDistance || target != null && (int)nav.remainingDistance == nav.stoppingDistance + 1)
                    {
                        nav.isStopped = true;
                        anim.SetFloat("Speed", 0);
                    }
                    else
                    {


                        
                        nav.isStopped = false;
                        anim.SetFloat("Speed", nav.remainingDistance);
                    }
                }
                break;


            case "work":
                anim.SetBool("Working", true);
                break;



        }


        

        
    }

    #endregion




    #region Unit Actions
    #region Following
    public void SetFocus(Unit_Controller newFocus)
    {
        if (newFocus.stats.isProp)
        {
            newFocus.GetComponent<NavMeshObstacle>().carving = false;
        }
        if (canMove)
        {
            focus = newFocus;
            StartFollow(newFocus);
        }
    }
    public void RemoveFocus()
    {
        if (focus != null)
        {
            if (focus.stats.isProp)
            {
                focus.GetComponent<NavMeshObstacle>().carving = true;
            }
            focus = null;
            StopFollow();
        }
    }
    protected void Follow()
    {
        if (target != null)
        {
            nav.SetDestination(target.position);
            if (focus.stats.isProp)
            {
                
                DestinationReached();
            }
        }

        
    }
    protected void StartFollow(Unit_Controller newTarget)
    {
        target = newTarget.transform;
        if (focus.stats.isProp)
            nav.stoppingDistance = 1;
        else
            nav.stoppingDistance = 2;
        
                
        
    }
    protected void StopFollow()
    {
        if (nav != null)
        {
            nav.stoppingDistance = 0;
            target = null;
        }
    }
    #endregion
    #endregion

    protected void DestinationReached()
    {
        if (focus.stats.isProp && (int)nav.remainingDistance == nav.stoppingDistance +1)
        {
            Destroy(focus.gameObject);
            nav.isStopped = true;
            RemoveFocus();
            Animation("work");
        }
    }




    #endregion















}


