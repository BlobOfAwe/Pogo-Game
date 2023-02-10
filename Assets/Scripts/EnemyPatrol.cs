using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    /// <summary>
    /// This script moves the enemy left or right within the specified range (minX and maxX). 
    /// When it reaches the end of its range, it flips its facingRight direction and changes its scale to face the opposite direction.
    /// </summary>
    //Sets up the variable for the patrol speed
    public float speed = 2f;
    //Sets up the variable for the max distance it patrols
    public float maxX = 2f;
    //Set up the variable for the minimum distance 
    public float minX = -2f;

    //Boolean checks if the enemy is facing right, is set to active at the start
    private bool facingRight = true;
    //Sets up the rigidbody 
    private Rigidbody2D rb;
    //At the start it gets rigidbody componenet 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.velocity = new Vector2(facingRight ? speed : -speed, rb.velocity.y);
    }
}
