using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : Controller {

    #region Variables
    #region Pause Check
    [HideInInspector] public bool paused;
    protected Canvas pausePanel;
    #endregion
    #region Pause Menu
    public int selected;
    protected Image highlighter;

   protected Vector3 highlightPos;
   public GameObject player;
   protected Animator animators;
    #endregion
    #endregion

    private void Start()
    {
        pausePanel = GetComponent<Canvas>();
        player = GameObject.Find("Characters");
        highlighter = GameObject.Find("Highlight").GetComponent<Image>();
        highlighter.rectTransform.position = new Vector3(244, 775, 0);

    }







    private void Update()
    {
        
        ControllerCheck();
        PauseCheck();
        SelectedMenu1();
    }



    public void PauseCheck()
    {
        psController = player.GetComponentInChildren<ThirdPersonController>().psController;
        xboxController = player.GetComponentInChildren<ThirdPersonController>().xboxController;

        if (Input.GetKeyDown(KeyCode.Escape)||button_Start)
        {
          
          pausePanel.enabled = !pausePanel.enabled;
          paused = pausePanel.enabled;
          GamePause();
            




        }
    }

    void SelectedMenu1()
    {
        if (paused)
        {
            highlighter.rectTransform.position = highlightPos;

            UpDownHandler();

           
            switch (selected)
            {

                case -1:
                    selected = 6;
                    break;

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

                case 7:
                    selected = 0;
                    break;

                


            }
        }


    }





    void UpDownHandler()
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
    }

    protected void GamePause()
    {
       player.GetComponentInChildren<ThirdPersonController>().enabled = !paused;
       player.GetComponentInChildren<ThirdPersonController>().anim.enabled = !paused;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }








}
