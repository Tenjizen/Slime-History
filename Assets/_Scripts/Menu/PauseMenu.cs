using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject settings;
    //[SerializeField] GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settings.gameObject.activeInHierarchy || credits.gameObject.activeInHierarchy)
                MenuPause();
            else
            {
                gameIsPaused = !gameIsPaused;
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }
    public void Resume()
    {
        gameIsPaused = !gameIsPaused;
        PauseGame();
    }
    public void MenuPause()
    {
        pauseMenu.SetActive(true);
        credits.SetActive(false);
        settings.SetActive(false);
    }
    public void Options()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(true);
        credits.SetActive(false);
    }

    public void Credits()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(false);
        credits.SetActive(true);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}