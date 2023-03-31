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
    private bool isHit = false;

    //The countdown timer for the stun
    private float stunTimer = 0f;

    private Animator enemyAnimator;

    void Start()
    {
        enemyAnimator = GetComponentInParent<Animator>();
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
        if (other.gameObject.tag == "Player" && !isHit)
        {
            isHit = true;
            SlimeTracker.Instance.CounterIncrement();
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
            enemyAnimator.SetBool("IsStunned", false);

        }
    }
    private void Update()
    {
        //If the enemy is stunned, decrease the stun timer
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;

            //If the stun timer reaches 0, reset the stun state
            if (stunTimer <= 0f)
            {
                isStunned = false;
                enemyAnimator.SetBool("IsStunned", false);
            }
        }
    }
}

