using UnityEngine;


public class RTS_Variables : MonoBehaviour
{
        
    #region Main Variables  
    #region Camera
    protected GameObject cam;
    protected Vector3 camLocation;

    protected float currentYaw = 180f;
    protected float currentZoom = 2f;
    #endregion
    #region Pause Menu
    protected bool EscapeMenuOpen = false;
    protected Canvas pausePanel;
    #endregion
    #region Controller
    protected GameObject selectedUnit;
    protected RaycastHit hit;
    protected Vector3 point;

    [SerializeField] protected LayerMask movementLayer;
    [SerializeField] protected LayerMask spriteLayer;
    #endregion
    #region RTS Controls

    protected float panspeed = 20f;
    protected float panBorder = 10f;
    protected float remainingDistance;
    
    protected GameObject selected;
    #endregion
    #endregion
    #region Not Inplemented
    protected GameObject skeleton;
    #endregion

}
