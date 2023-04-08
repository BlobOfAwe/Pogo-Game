using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int baseHP = 3; // The player's starting HP
    [SerializeField] HealthbarManager healthbar; // The Script containing references to the healthbar animators
    public int hp; // Tracks the player's current HP, set to baseHP on start or on respawn
    public Vector2 checkpointPos; // The position the player is reset to after death
    private Rigidbody2D rb; // The player's rigidbody
    private AudioSource sfx;
    private SFXClipManager clip;
    private Animator playerAnimator;

    private void Start()
    {
        hp = baseHP; // Initialize HP
        rb = GetComponent<Rigidbody2D>(); // Initialize rigidbody
        sfx = GetComponent<AudioSource>();
        clip = GameObject.Find("SFXClipManager").GetComponent<SFXClipManager>();
        playerAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < healthbar.hearts.Length; i++)
        {
            if (i <= hp - 1)
            {
                healthbar.hearts[i].SetBool("Reappear", true);
            }
            else
            {
                healthbar.hearts[i].SetBool("Reappear", false);
                if(i == 0)
                {
                    healthbar.resetLight.SetTrigger("Reset");
                }
            }
        }
        // When HP gets below 0, reset the player to the last checkpoint
        if (hp <= 0)
        {
            sfx.clip = clip.death;
            sfx.Play();
            playerAnimator.SetTrigger("IsDead");
            transform.position = checkpointPos;
            rb.velocity = Vector2.zero;
            rb.rotation = 0;
            hp = baseHP;
            playerAnimator.SetTrigger("IsDead");
        }
    }
}
