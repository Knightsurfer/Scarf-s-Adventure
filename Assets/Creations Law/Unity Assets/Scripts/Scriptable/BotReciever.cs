using PlayerVariables;
using UnityEngine;
using UnityEngine.AI;


//The Main Startup Class. (Nothing is really required from this class.)
public class BotReciever : PartyController
{
    private void Start()
    {
        AssignPlayer();
        switch (botType)
        {
            case 0:
                AddComponents();
                break;
        }
    }

    private void Update()
    {
        switch (botType)
        {
            case 0:
                FacePlayer();
                break;
        }
    }
}

public class PartyController : Components
{
    protected void Animation()
    {
        if (distance < 1)
        {
            if (canMove && distance > 2)
            {
                anim.SetFloat("Speed", Mathf.Abs(1) + Mathf.Abs(1));
            }
            if (distance < 2)
            {
                anim.SetFloat("Speed", 0);
            }
        }
    }
}

public class Components : Variables
{
    protected void AddComponents()
    {
        #region NavMesh Agent
        if (GetComponent<NavMeshAgent>())
        {
            botAI = gameObject.GetComponent<NavMeshAgent>();
        }
        else
        {
            botAI = gameObject.AddComponent<NavMeshAgent>();
        }
        #endregion 
        #region Character Controller
        if (GetComponent<CharacterController>())
        {
            bot = GetComponent<CharacterController>();
        }
        else
        {
            bot = gameObject.AddComponent<CharacterController>();
        }
        #endregion
        SetComponents();
    }
    private void SetComponents()
    {
        #region Other
        //anim = GetComponent<Animator>();
        #endregion
        #region NavMesh Agent
        botAI.agentTypeID = 0;
        botAI.speed = 7f;
        botAI.angularSpeed = 12000;
        botAI.acceleration = 20;
        botAI.stoppingDistance = 2.5f;
        botAI.baseOffset = -0.19f;
        botAI.enabled = false;
        botAI.autoTraverseOffMeshLink = false;
        #endregion
        #region Character Controller
        bot.height = 2f;
        bot.radius = 0.5f;
        bot.center = new Vector3(0, 1.1f, 0);
        #endregion

        skeleton = GetComponentInChildren<SkinnedMeshRenderer>().transform;
    }
}

public class Variables : CharacterPackage
{
    #region Variables
    //Own Object

    #endregion

    #region Waypoints
    public float distance;
    public float range = 10;
    public Vector3[] points;
    protected int createdNo;
    public int WaypointPassed = 0;
    #endregion
    #region Controller Variables
    protected float speed = 8.5f;
    protected float rotator = 150;
    protected Vector3 moveDirection;
    protected Quaternion theRotation;
    protected float player_rotateSpeed = 10;

    protected Vector3 pointZero;
    protected bool positionSet;
    #endregion
}





