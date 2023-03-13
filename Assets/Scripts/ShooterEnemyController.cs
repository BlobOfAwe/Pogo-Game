using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyController : MonoBehaviour
{
    /// <summary>
    /// This script is the main controller for the shooter type enemy
    /// It gives the object different parameters such as the projectile speed, delaly between shots, and the positioning of the enemy itself.
    /// it lets the player assign the bullet pbject and calls to the enemyStun script to stun the enemy if the player collides with it
    /// </summary>
    //The projectile 
    public GameObject bulletPrefab;
    //Projectile speed
    public float bulletSpeed = 5f;
    //Sseconds between each shot
    public float timeBetweenShots = 1f;
    //Checks if the enemy is facing right
    public bool isFacingRight = true;
    //calls the stun function from the EnemyStun script
    private EnemyStun enemyStun;
    //The time when the next shot can be fired
    private float nextFireTime;

    private void Start()
    {
        //Gets the functions from the EnemyStun script
        enemyStun = GetComponentInChildren<EnemyStun>();
    }
    void Update()
    {
        //If the enemy is not stunned then it shoots the bullet 
        if (!enemyStun.isStunned && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + timeBetweenShots;
            ShootProjectile();
        }

    }
    //Function that handles the shooting
    private void ShootProjectile()
    {
        //Instantiates the bullet 
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        //Grabs the rigidbody from the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //Gives the bullet velocity to move at the given direction
        rb.velocity = (isFacingRight ? transform.right : -transform.right) * bulletSpeed;
    }


}