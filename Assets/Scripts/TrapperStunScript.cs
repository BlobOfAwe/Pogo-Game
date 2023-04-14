using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapperStunScript : MonoBehaviour
{

    public bool isStunned = false;
    GroundSlam groundSlam;
    // private float biteTimer = 0f;
    // public float BiteDuration = 3f;
    private AudioSource sfx;
    private SFXClipManager clip;
    private BoxCollider2D bc;
    private Animator enemyAnimator;
    private DamagePlayer dmgPlayer;

    //If the enemy collides with something it checks to see what it is, if it is the player then it initiates the stun function
    private void Start()
    {
        groundSlam = FindObjectOfType<GroundSlam>();
        enemyAnimator = GetComponentInParent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        dmgPlayer = GetComponent<DamagePlayer>();
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
        }
        if (!enemyAnimator.GetBool("IsStunned"))
        {
            enemyAnimator.SetBool("IsStunned", true);
        }
        // if (!enemyAnimator.GetBool("IsStunned"))
        //  {
        //    enemyAnimator.SetBool("IsStunned", true);

        // }

    }
}
