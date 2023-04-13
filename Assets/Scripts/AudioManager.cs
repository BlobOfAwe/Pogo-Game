using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource sfx;
    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        sfx.clip = clip;
        sfx.Play();
    }
}
