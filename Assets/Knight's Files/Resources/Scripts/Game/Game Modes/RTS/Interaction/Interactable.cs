
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 1;



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z));
    }

}
