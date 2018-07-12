using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour {

    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;


    public void Back()
    {
        optionMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        PauseMenu.UsingOptions = false;
    }

    public void Resume()
    {
        optionMenuUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.IsPaused = false;
        PauseMenu.UsingOptions = false;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }


}
