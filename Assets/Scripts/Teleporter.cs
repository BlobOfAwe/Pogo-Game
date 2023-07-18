using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [SerializeField] int nextSceneIndex;
    private AudioSource sfx;
    public Animator transitionAnim;

    private void Start()
    {
        sfx = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadLevel());
    }
   IEnumerator LoadLevel()
    {
        Debug.Log("Entered Teleporter");
        sfx.Play();
        transitionAnim.SetTrigger("FadeStart");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextSceneIndex);
        transitionAnim.SetTrigger("FadeEnd");
    }
}
