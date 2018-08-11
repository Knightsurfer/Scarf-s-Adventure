
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [HideInInspector] public float radius = 1;



    protected void RadiusUpdate()
    {
        radius = transform.localScale.x;
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z));
        
    }

}
