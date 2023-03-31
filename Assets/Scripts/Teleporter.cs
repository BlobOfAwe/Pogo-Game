using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [SerializeField] int nextSceneIndex;
    private AudioSource sfx;

    private void Start()
    {
        sfx = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered Teleporter");
        SceneManager.LoadScene(nextSceneIndex);
        sfx.Play();
    }
}
