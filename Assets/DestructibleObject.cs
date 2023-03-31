using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestructibleObject : MonoBehaviour
{
    BoxCollider2D destructibleCollider; // The collider of the destructible object
    [SerializeField] Rigidbody2D[] segments; // A list containing all segments of the broken platform
    private AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        // Assign component variables
        destructibleCollider = GetComponent<BoxCollider2D>();
        sfx = GetComponent<AudioSource>();
    }


    public void TriggerDestroy(Collider2D col)
    {
        // If the collided object has the tag "Player"
        if (col.CompareTag("Player"))
        {
            // If the player's GroundSlam component is slamming
            if (col.GetComponent<GroundSlam>().isSlamming) // NOTE: This line will throw an error when any child colliders of the player pass through the trigger. This is normal and does not impact the game.
            {
                foreach (Rigidbody2D rb in segments)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    rb.AddForce(new Vector2(Random.Range(-20, 20), Random.Range(0, 10)));
                }
                destructibleCollider.enabled = false;
                sfx.Play();
            }
        }
    }
}
