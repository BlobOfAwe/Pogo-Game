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
        dmgPlayer = transform.parent.GetComponentInChildren<DamagePlayer>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (groundSlam.isSlamming)
            {
                Stun();
            }
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
            Debug.Log("Trapper is now stunned");
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

    public void UnStun()
    {
        // Checks if the enemy is stunned  
        if (isStunned)
        {
            // Un-stuns the enemy if it is stunned
            isStunned = false;

        }
    }

}
