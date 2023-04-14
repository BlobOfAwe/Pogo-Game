using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] int nextSceneIndex;

    public void StartGame()
    {
        Debug.Log("Entered Teleporter");
        SceneManager.LoadScene(nextSceneIndex);;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
