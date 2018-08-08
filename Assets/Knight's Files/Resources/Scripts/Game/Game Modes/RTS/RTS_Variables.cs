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

     protected GameObject cam;
     protected ScarfController scarf;
     protected Vector3 camTransform;

    protected float currentYaw = 180f;
    protected float currentZoom = 2f;
    #endregion

    #region RTS Variables
    protected float panspeed = 20f;
    protected float panBorder = 10f;


     protected RaycastHit hit;
     protected Vector3 point;


    [SerializeField] protected LayerMask movementLayer;
    [SerializeField] protected LayerMask spriteLayer;
    [SerializeField] protected Text remainingDistance;
    [SerializeField] protected NavMeshAgent agent;
    #endregion


    [SerializeField] protected GameObject escape;

    
    



}
