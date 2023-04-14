using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlimeTracker : MonoBehaviour
{
    /// <summary>
    /// This script tracks the player's score in a game
    /// It updates the score on the UI
    /// And persists the score across scene changes using a singelton and the DontDestroyOnLoad() method.
    /// </summary>
    public static SlimeTracker Instance; //This variable references the instance of the SlimeTracker script.
    public TextMeshProUGUI slimeCountText;
    public static int slimeScore;
    //It calls the UpdateScoreText() method to update the score text on the UI,
    //And it also calls the DontDestroyOnLoad() method to ensure that the SlimeTracker object is not destroyed when a new scene is loaded.
    void Start()
    {
        Debug.Log("Start method called");
        UpdateScoreText();
        DontDestroyOnLoad(gameObject);
    }
    //This is a public static method that resets the score to 0 and updates the score text on the UI.
    public static void ResetScore()
    {
        slimeScore = 0;
        UpdateScoreText();
    }
    //It updates the text object on the UI with the current score value.
    void Update()
    {
        slimeCountText.text = "Slimes Hunted: " + slimeScore.ToString();
    }
    //This function increments the score by 1 and updates the score text on the UI.
    public void CounterIncrement()
    {
        slimeScore ++;
        UpdateScoreText();
    }
    //This is a private static method updates the score text on the UI. It first finds the instance of the SlimeTracker script using the FindObjectOfType() method, and then it checks if the slimeCountText variable is not null. If it is not null, then it updates the text with the current score
    private static void UpdateScoreText()
    {
        SlimeTracker instance = FindObjectOfType<SlimeTracker>();
        if (instance != null && instance.slimeCountText != null)
        {
            instance.slimeCountText.text = "Slimes Hunted: " + slimeScore.ToString();
        }
    }
}
