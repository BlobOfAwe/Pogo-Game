using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script moves the enemy left or right within the specified range (minX and maxX). 
/// When it reaches the end of its range, it flips its facingRight direction and changes its scale to face the opposite direction.
/// On the update it checks if the enemy is stunned or not, if not then it continues to do what it does, if it is then changes its speed to zero
/// </summary>
public class SlimePatrol : MonoBehaviour
{
    
    public float speed = 2f; //Sets up the variable for the patrol speed
    public float maxX = 2f; //Sets up the variable for the max distance it patrols
    public float minX = -2f; //Set up the variable for the minimum distance 
    public bool facingRight = true; // Boolean checks if the enemy is facing right, is set to active at the start

    private Rigidbody2D rb; //Sets up the rigidbody 
    private EnemyStun enemyStun; // The script that controls whether the enemy is stunned or not

    //At the start it gets rigidbody componenet 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyStun = GetComponentInChildren<EnemyStun>();
    }

    void Update()
    {
        //Checks if the enemy is facing right and has reached max distance to the right

        if (facingRight && transform.position.x > maxX)
        {
            //Sets the facingright boolean to false

            facingRight = false;
            //Scales the enemy to face left
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // Checks if the enemy is not facing right and if they have reahed the minimum distance 
        else if (!facingRight && transform.position.x < minX)
        {
            //sets the facingright boolean to true
            facingRight = true;
            //Scales the enemy to face right
            transform.localScale = new Vector3(1, 1, 1);
        }
        //Responsible for moving the enemy
        //Checks if the enemy is is stunned or not then if it is it changes its speed to 0
        if (enemyStun.isStunned)
        {
            { 
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
                rb.freezeRotation = true;
            }
        }
        //If its not stunned then it carries on
        else
        {
            rb.velocity = new Vector2(facingRight ? speed : -speed, rb.velocity.y);
            rb.isKinematic = false;
            rb.freezeRotation = false;
        }

        //DEBUG: If R is pressed, un-stun the enemy
        if (Input.GetKey(KeyCode.R)) { enemyStun.isStunned = false; }
    }

}
