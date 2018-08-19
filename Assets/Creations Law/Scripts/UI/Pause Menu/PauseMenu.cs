using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Controller {

    #region Variables

    [HideInInspector] public bool paused;
    [HideInInspector] public Canvas pausePanel;
    #endregion


    private void Update()
    {
        ControllerCheck();
        PauseCheck();
    }



    public void PauseCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape)||button_Start)
        {
          pausePanel = GetComponent<Canvas>();
          pausePanel.enabled = !pausePanel.enabled;
          paused = pausePanel.enabled;
          
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








}
