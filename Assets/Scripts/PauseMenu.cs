using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool ispaused;
    public GameObject player;
    // private void Start()
    // {
    //pauseMenu.SetActive(false);
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ispaused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }
    public void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        ispaused = true;
    }
    public void resume()
    {
        pauseMenu.SetActive(false);
        ispaused = false;
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        ispaused = false;
        pauseMenu.SetActive(false);
        player.GetComponentInParent<HealthManager>().hp = 0;

    }
}
