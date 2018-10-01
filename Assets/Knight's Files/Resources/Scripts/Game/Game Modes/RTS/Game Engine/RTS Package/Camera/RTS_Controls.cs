using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;


public class RTS_Controls : RTS_Mode
{

}

public class RTS_Mode : RTS_Checks
{
    #region Variables


    protected NavMeshAgent nav;
    protected RaycastHit hit;

    protected LayerMask movementLayer = 4096;
    protected LayerMask unitLayer = 24576;
    #endregion

    protected void Start()
    {
        Cam_Start();
    }
    protected void Update()
    {
        MainControls();
        Check_Update();
    }

    protected void MainControls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootRay("Left", cam.ScreenPointToRay(Input.mousePosition));
        }



        if (selectedUnit != null)
        {
            if (selectedUnit.GetComponent<Unit_Controller>().Dead)
            {
                selectedUnit = null;
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (selectedUnit != null)
                {
                    ShootRay("Right", cam.ScreenPointToRay(Input.mousePosition));
                }
            }
            if (Input.GetKey(KeyCode.F1))
            {
                unit.TakeDamage(5, 0);
            }
            if (Input.GetKey(KeyCode.F2))
            {
                unit.TakeDamage(0, 5);
            }
            if (Input.GetKey(KeyCode.F3))
            {
                if (!unit.stats.isProp)
                {
                    unit.EXP++;
                }
            }
        }
    }
    protected void ShootRay(string mouseClick, Ray ray)
    {
        if (Physics.Raycast(ray, out hit, 100, unitLayer))
        {
            switch (mouseClick)
            {
                case "Left":
                    selectedUnit = hit.collider.gameObject;
                    break;

                case "Right":
                    Unit_Controller followTarget = GameObject.Find(hit.collider.name).GetComponent<Unit_Controller>();
                    selectedUnit.GetComponent<Unit_Controller>().SetFocus(followTarget);
                    break;
            }
        }
        else if (Physics.Raycast(ray, out hit, 100, movementLayer))
        {
            switch (mouseClick)
            {
                case "Left":

                    break;

                case "Right":
                    selectedUnit.GetComponent<Unit_Controller>().Movement(hit.point);
                    break;
            }
        }
    }
}

public class RTS_Checks : RTS_Cam
{
    #region Variables
    protected RTS_Panel panel;
    protected Color selected = new Color(1, 1, 1);
    protected Unit_Controller unit;
    protected GameObject selectedUnit;
    #endregion

    private void Update()
    {
        Check_Update();
    }
    protected void Check_Update()
    {
        Cam_Update();
        Command_Panel();
    }
    protected void Command_Panel()
    {


        panel = FindObjectOfType<RTS_Panel>();
        panel.GetComponent<Canvas>().enabled = true;
        if (selectedUnit != null)
        {
            unit = selectedUnit.GetComponent<Unit_Controller>();
            PanelRemote();
        }
        if (selectedUnit == null)
        {
            panel.Panel_Defaults();
        }
    }
    protected void PanelRemote()
    {
        #region Defaults
        if (unit.portrait != null)
        {
            selected.a = 1;
            panel.portrait.color = selected;
            panel.portrait.sprite = unit.portrait;
        }
        #endregion
        #region Setting The Panel
        panel.unitName.text = unit.unitName;

        panel.HP_Text.text = "HP " + unit.HP + "/" + unit.maxHP;
        panel.MP_Text.text = "MP " + unit.MP + "/" + unit.maxMP;

        panel.HP_Bar.fillAmount = (float)unit.HP / unit.maxHP;
        panel.MP_Bar.fillAmount = (float)unit.MP / unit.maxMP;

        panel.EXP_Bar.fillAmount = (float)unit.EXP / unit.maxEXP;
        #endregion
    }
}

public class RTS_Cam : MonoBehaviour
{
    #region Variables


    private GameObject player;

    protected Camera cam;
    protected PauseMenu pause;
    public Vector3 camLocation;

    protected float maxX;
    protected float minX;

    protected float maxY;
    protected float minY;

    protected float currentYaw = 180f;
    protected float currentZoom = 2f;

    protected float panspeed = 20f;
    protected float panBorder = 10f;



    #endregion
    #region Main Routines
    #region Standalone
    private void Start()
    {
        Cam_Start();
    }
    private void Update()
    {
        Cam_Update();
    }
    #endregion

    protected void Cam_Start()
    {
        pause = GameObject.Find("Pause Menu").GetComponent<PauseMenu>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cam.transform.SetParent(GameObject.Find("Game Manager").transform);

        currentZoom = 4f;
        player = GameObject.Find("Scarf");

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            camLocation.x = 142;
            camLocation.y = -75;
            camLocation.z = -391;

            player.transform.localPosition = new Vector3(48, -0, -45);

        }
    }
    protected void Cam_Update()
    {
        RealTimeStratedgy();
    }
    #endregion
    #region Camera Movement
    protected void RealTimeStratedgy()
    {
        RealTimeKeyboard();
        RealTimeCamera();
    }

    protected void RealTimeCamera()
    {
        cam.transform.position = camLocation - new Vector3(0, -2, -2) * currentZoom;
        cam.transform.LookAt(camLocation + Vector3.up * 2f);
        cam.transform.RotateAround(camLocation, Vector3.up, currentYaw);
    }
    protected void RealTimeKeyboard()
    {
        if (!pause.paused)
        {

            if (Input.mousePosition.y >= Screen.height - panBorder)
            {
                camLocation.z += panspeed * Time.deltaTime;
            }
            if (Input.mousePosition.y <= panBorder)
            {
                camLocation.z -= panspeed * Time.deltaTime;
            }
            if (Input.mousePosition.x >= Screen.width - panBorder)
            {
                camLocation.x += panspeed * Time.deltaTime;
            }
            if (Input.mousePosition.x <= panBorder)
            {
                camLocation.x -= panspeed * Time.deltaTime;
            }

            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * 4f;
        }
        currentZoom = Mathf.Clamp(currentZoom, 2.4f, 8f);


        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            camLocation.x = Mathf.Clamp(camLocation.x, 100f, 180);
            camLocation.z = Mathf.Clamp(camLocation.z, -370, -270);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            camLocation.x = Mathf.Clamp(camLocation.x, 10, 90);
            camLocation.z = Mathf.Clamp(camLocation.z, 10, 90);
        }

    }
    #endregion
}

