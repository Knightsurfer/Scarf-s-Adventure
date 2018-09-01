
using UnityEngine;
using UnityEngine.AI;

public class Unit_Interact : MonoBehaviour
{
    public Unit_Info stats;

    [HideInInspector]public float radius = 1;
    [HideInInspector] public NavMeshAgent nav;


    protected void Radius_Start()
    {
        if (!GetComponent<Unit_Controller>().stats.isProp)
        {
            gameObject.AddComponent<Rigidbody>();
            gameObject.GetComponent<Rigidbody>().isKinematic = true;

            gameObject.AddComponent<NavMeshAgent>();
            nav = GetComponent<NavMeshAgent>();
            nav.speed = 7;
            nav.baseOffset = 0;
            nav.height = 2;
            nav.radius = 1;
            nav.angularSpeed = 1200;
            nav.acceleration = 20;
            nav.areaMask = 5;
            nav.autoBraking = false;
        }
        if (stats.isProp)
        {
            gameObject.AddComponent<NavMeshObstacle>();
            gameObject.GetComponent<NavMeshObstacle>().carving = true;
            gameObject.AddComponent<BoxCollider>();
        }
        else
        {
            gameObject.AddComponent<CapsuleCollider>();
            gameObject.GetComponent<CapsuleCollider>().height = 2.5f;
            gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0, 1, 0);
        }
        
    }
}
