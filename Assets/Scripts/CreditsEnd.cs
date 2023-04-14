using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CreditsEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GoMainMenu", 15f);    
    }

    void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
