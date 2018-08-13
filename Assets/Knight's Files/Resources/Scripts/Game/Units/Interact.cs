
using UnityEngine;

public class Unit_Interact : MonoBehaviour
{
    public float radius = 1;

    protected Vector3 boxRadius;
    protected Vector3 boxPosition;



    protected void RadiusUpdate()
    {
        boxRadius = new Vector3(radius,0);
        radius = transform.localScale.x;
    }

    protected void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + boxPosition , boxRadius);
    }

}
