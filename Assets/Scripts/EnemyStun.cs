using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStun : MonoBehaviour
{
    /// <summary>
    /// This script handles the stunning and stopping of the enemy movement. 
    /// 
    /// </summary>


    //The boolean is there to check if the character is stunned or not, its set to false at the begining. 
    public bool isStunned = false;
    public float stunDuration = 3f;

    //The countdown timer for the stun
    private float stunTimer = 0f;

    private Animator enemyAnimator;
    private AudioSource sfx;
    private SFXClipManager clip;
    private DamagePlayer dmgPlayer;

    void Start()
    {
        enemyAnimator = GetComponentInParent<Animator>();
        sfx = GetComponent<AudioSource>();
        clip = GameObject.Find("SFXClipManager").GetComponent<SFXClipManager>();
        dmgPlayer = transform.parent.GetComponentInChildren<DamagePlayer>();
    }

    //If the enemy collides with something it checks to see what it is, if it is the player then it initiates the stun function
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Checks if the tag is assigned as Player
        if (other.gameObject.tag == "Player")
        {
            //Initiates the stun function
            Stun();
        }


    }
    //Stun function checks to see if the enemy is not stunned, then stuns it
    public void Stun()
    {
        //Checks if the enemy is not stunned  
        if (!isStunned)
        {
            //Stuns the enemy if it is indeed stunned
            isStunned = true;
            dmgPlayer.enabled = false;
            sfx.clip = clip.enemyStun;
            sfx.Play();

            stunTimer = stunDuration;
        }
        if (!enemyAnimator.GetBool("IsStunned"))
        {
            enemyAnimator.SetBool("IsStunned", true);
            Invoke("UnStun", stunDuration);
        }
    }
    public void UnStun()
    {
        // Checks if the enemy is stunned  
        if (isStunned)
        {
            // Un-stuns the enemy if it is stunned
            isStunned = false;
            dmgPlayer.enabled = true;
            enemyAnimator.SetBool("IsStunned", false);

        }
    }
}

