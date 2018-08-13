using UnityEngine;
using UnityEngine.AI;

public class Unit_Controller : Unit_Stats
{
    [HideInInspector] public NavMeshAgent nav;
    protected CharacterController ThirdPersonController;
    protected float turnSpeed = 5f;






    protected Animator anim;

    protected Transform target;
    protected Unit_Interact focus;
    protected void Start()
    {
        Assign_Stats();
        ThirdPersonController = GetComponent<CharacterController>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();




    }
    protected void Update()
    {
        Stats();
        RTS_Movement();
        ThirdPerson_Movement();
    }









    #region Third Person Mode
    protected void ThirdPerson_Movement()
    {
        var horizontal = Input.GetAxis("LeftStickX");
        var vertical = Input.GetAxis("LeftStickY");

        var movement = new Vector3(horizontal, 0, vertical);

        if (ThirdPersonController != null)
        {
            if (canMove)
            {
                ThirdPersonController.SimpleMove(movement * Time.deltaTime * 200);
                anim.SetFloat("Speed", movement.magnitude);

                if (movement.magnitude > 0)
                {
                    Quaternion newDirection = Quaternion.LookRotation(movement);
                    transform.rotation = Quaternion.Slerp(transform.rotation,newDirection, Time.deltaTime * turnSpeed);
                }
            }
        }
    }
    #endregion











    #region RTS Mode
    protected void RTS_Movement()
    {
        if (!isDead)
        {
            if (nav != null)
            {
                Animation();
            }
            Follow();
        }
    }
    #region Movement
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
            else if ((int)nav.remainingDistance != nav.stoppingDistance)
            {
                GetComponent<NavMeshAgent>().isStopped = false;

            }
            
        
    }
    public void Movement(Vector3 point)
    {

        if (canMove)
        {
            if (nav != null)
            {
                nav.SetDestination(point);
                RemoveFocus();
                return;
            }
            nav = GetComponent<NavMeshAgent>();
            nav.SetDestination(point);
        }
        RemoveFocus();
    }
    #endregion
    #region Following
    public void SetFocus(Unit_Interact newFocus)
    {
        if (canMove)
        {
            focus = newFocus;
            StartFollow(newFocus);
        }
    }
    protected void RemoveFocus()
    {
        focus = null;
        StopFollow();
    }
    protected void Follow()
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
    protected void StartFollow(Unit_Interact newTarget)
    {
        nav.stoppingDistance = 2 + focus.radius;
        target = newTarget.transform;
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















}


