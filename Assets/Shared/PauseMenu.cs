using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool IsPaused;
    public GameObject pauseMenuUI;

    InputController playerInput;

    private void Start()
    {
        IsPaused = false;
        playerInput = GameManager.Instance.InputController;
    }

    // Update is called once per frame
    void Update ()
    {
        if (playerInput.Pause)
        {
            if (!IsPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }

        }

        if (IsPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (!IsPaused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
