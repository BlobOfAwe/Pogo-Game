using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This program is specifically to be used for whitebox testing. It modifies the object sprite's colour tint in response to being broken, rather than breaking.
/// It also regenerates the platform after a period of time, which will not be present in the final game.
/// DO NOT USE THIS FOR ANY FINAL BUILDS
/// </summary>
public class WhiteboxDestructibleObject : MonoBehaviour
{
    BoxCollider2D destructibleCollider; // The collider of the destructible object
    SpriteRenderer spriteRenderer; // The sprite renderer for the destructible object
    [SerializeField] Color defaultColor = Color.yellow; // The colour tint for the sprite when the object is solid
    [SerializeField] Color brokenColor = Color.blue; // The colour tint for the sprite when the object is broken (not solid)
    [SerializeField] float restoreTimer = 10; // The time in seconds before the object becomes solid again

    // Start is called before the first frame update
    void Start()
    {
        // Assign component variables
        destructibleCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void TriggerDestroy(Collider2D col)
    {
        // If the collided object has the tag "Player"
        if (col.CompareTag("Player"))
        {
            // If the player's GroundSlam component is slamming
            if (col.GetComponent<GroundSlam>().isSlamming) // NOTE: This line will throw an error when any child colliders of the player pass through the trigger. This is normal and does not impact the game.
            {
                spriteRenderer.color = brokenColor; // Change the colour to the broken colour
                destructibleCollider.enabled = false; // Disable the collider
                StartCoroutine("RestoreObject"); // Start the countdown to make the object solid again
            }
        }
    }

    // After restoreTimer seconds, make the destroyed object solid again.
    IEnumerator RestoreObject()
    {
        yield return new WaitForSeconds(restoreTimer); // Wait for (restoreTimer) seconds
        spriteRenderer.color = defaultColor; // Reset the object's colour
        destructibleCollider.enabled = true; // Enable the collider
    }
}
