using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float knockback = 5;

    //If the enemy collides with something it checks to see what it is, if it is the player then it initiates the stun function
    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Detected collision with " + col);
        //Checks if the tag is assigned as Player
        if (col.gameObject.tag == "Player")
        {
            GameObject player = col.gameObject;

            player.GetComponentInParent<HealthManager>().hp -= damage;
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.AddRelativeForce(new Vector2(knockback, 0), ForceMode2D.Impulse);
        }
    }
}
