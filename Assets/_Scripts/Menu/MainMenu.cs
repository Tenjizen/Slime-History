using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject credits;
    public GameObject settings;

    private void Start()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        credits.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settings.gameObject.activeInHierarchy || credits.gameObject.activeInHierarchy)
                MainMenus();
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenus()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        credits.SetActive(false);
    }
    public void Options()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
        credits.SetActive(false);
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
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
