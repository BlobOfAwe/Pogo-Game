using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float knockback = 5;
    private AudioSource sfx;
    private SFXClipManager clip;

    private void Start()
    {
        clip = GameObject.Find("SFXClipManager").GetComponent<SFXClipManager>();
        sfx = GetComponent<AudioSource>();
    }
    //If the enemy collides with something it checks to see what it is, if it is the player then it initiates the stun function
    private void OnCollisionEnter2D(Collision2D col)
    {
        // If the collider is the player
        if (col.gameObject.tag == "Player")
        {
            // Assign a temporary variable
            GameObject player = col.gameObject;
            sfx.clip = clip.damage;
            sfx.Play();

            Animator anim = player.GetComponentInParent<Animator>();
            anim.SetTrigger("damaged");
            

            player.GetComponentInParent<HealthManager>().hp -= damage; // subtract damage from the player's hp
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>(); // fetch the player's rigidbody
            rb.velocity = Vector2.zero; // set the player's velocity to 0
            rb.AddRelativeForce(new Vector2(knockback, 0), ForceMode2D.Impulse); // add force to the player's rigidbody to knock them backwards
        }
    }
}
