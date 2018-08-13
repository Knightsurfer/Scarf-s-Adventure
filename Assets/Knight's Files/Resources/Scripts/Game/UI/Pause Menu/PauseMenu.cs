using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    #region Variables

    public bool paused;
    public Canvas pausePanel;
    #endregion


    private void Update()
    {
        PauseCheck();
    }



    public void PauseCheck()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
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
