using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseBasic : Controller {

    #region Variables
    #region Pause Check
    [HideInInspector] public bool paused;
    protected Canvas pausePanel;
    #endregion
    #region Pause Menu
    public int selected;
    protected Image highlighter;

    protected Vector3 highlightPos;
    protected GameObject player;
    protected Animator animators;
    #endregion
    #region SelectedMenu
    protected string currentMenu = "Main Menu";

    protected Canvas mainCanvas;
    protected Canvas itemCanvas;
    protected Canvas equipCanvas;
    #endregion
    #endregion

   protected int selectedMin;
   protected int selectedMax;
   protected Text stageName;
   protected Text worldName;
    bool testmode = false;



    #region Setting Variables
    protected void BasicStart ()
    {
       
            worldName = GameObject.Find("World Name").GetComponent<Text>();
            stageName = GameObject.Find("Level Section").GetComponent<Text>();

            ControllerDetect();

            pausePanel = GetComponent<Canvas>();
            player = GameObject.Find("Scarf");

            highlighter = GameObject.Find("Highlight").GetComponent<Image>();
            highlighter.rectTransform.position = new Vector3(244, 775, 0);

            CanvasSearcher();
        }
    
    protected void CanvasSearcher()
    {
        mainCanvas = GameObject.Find("Main Menu").GetComponent<Canvas>();
        itemCanvas = GameObject.Find("Item Menu").GetComponent<Canvas>();
        equipCanvas = GameObject.Find("Equip Menu").GetComponent<Canvas>();
    }
    #endregion


    protected void BasicUpdate()
    {
        ControllerCheck();
        PauseCheck();
    }








    #region PauseFunction
    public void PauseCheck()
    {


        if (button_Start)
        {
            if(!paused)
            {
                switch (SceneManager.GetActiveScene().buildIndex)
                {
                    case 1:
                        stageName.text = "|| Dream State: Tutorial";
                        worldName.text = "Dream\nState";
                        break;
                }
               ;
            }
            pausePanel.enabled = !pausePanel.enabled;
            paused = pausePanel.enabled;
            if (controller == "Keyboard")
            {
                Cursor.visible = pausePanel.enabled;
            }
            GamePause();
        }
        

        
    }
    protected void GamePause()
    {
        if (FindObjectOfType<RTS_Mode>().enabled == false)
        {
            player.GetComponent<ThirdPerson_Mode>().enabled = !paused;
        }
        if (FindObjectOfType<RTS_Mode>().enabled == true)
        {
            player.GetComponent<Unit_Controller>().enabled = !paused;
        }
        player.GetComponent<Animator>().enabled = !paused;
    }





    #endregion














    #region Controls
    protected void UpDownHandler(int min, int max)
    {
        
        if (direction == "up")
        {
            if (d_Up == false)
            {
                d_Up = true;

                selected--;
            }
        }

        if (direction == "down")
        {
            if (d_Down == false)
            {
                d_Down = true;

                selected++;
            }
        }

        highlighter.rectTransform.position = highlightPos;
        MenuScroller(min, max);
    }

    protected void MenuScroller(int min,int max)
    {
        if (selected < min)
            {
                selected = max;
            }
        if (selected > max)
            {
            selected = min;
            }

        HighlightPos();
    }


    protected void HighlightPos()
    {
        switch (selected)
        {
            case 0:
                highlightPos = new Vector3(244, 775, 0);
                break;

            case 1:
                highlightPos = new Vector3(244, 703, 0);
                break;

            case 2:
                highlightPos = new Vector3(244, 629, 0);
                break;

            case 3:
                highlightPos = new Vector3(244, 555, 0);
                break;

            case 4:
                highlightPos = new Vector3(244, 485, 0);
                break;

            case 5:
                highlightPos = new Vector3(244, 412, 0);
                break;

            case 6:
                highlightPos = new Vector3(244, 344, 0);
                break;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion













}
