using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;


public class RTS_Variables : MonoBehaviour
{
    #region Pause Menu
    [HideInInspector] protected bool EscapeMenuOpen = false;


    #endregion

    #region Main variables

    public Vector3 destination;
    public GameObject scarf;
    public GameObject skeleton;
    protected GameObject cam;
    protected Vector3 camTransform;

    protected float currentYaw = 180f;
    protected float currentZoom = 2f;
    #endregion

    #region RTS Variables
    protected float panspeed = 20f;
    protected float panBorder = 10f;


     public RaycastHit hit;
     public Vector3 point;


    [SerializeField] protected LayerMask movementLayer;
    [SerializeField] protected LayerMask spriteLayer;
    public float remainingDistance;
    public NavMeshAgent agent;
    #endregion


    [SerializeField] protected GameObject escape;

    
    



}
