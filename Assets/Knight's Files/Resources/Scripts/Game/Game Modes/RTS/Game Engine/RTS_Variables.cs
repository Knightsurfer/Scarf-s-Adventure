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
    protected GameObject selectedUnit = null;
    protected RaycastHit hit;
    protected Vector3 point;

    public LayerMask movementLayer;
    public LayerMask spriteLayer;
    public LayerMask itemLayer;
    #endregion
    #region RTS Controls

    protected float panspeed = 20f;
    protected float panBorder = 10f;
    protected float remainingDistance;
    
    protected RTS_Panel panel;
    #endregion
    #endregion
    #region Not Inplemented
    protected GameObject skeleton;
    #endregion

}
