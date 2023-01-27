using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is for use on the player, and any other entity that needs to know when it is touching the ground.
/// REQUIRES: A GameObject with a transform component assigned to "groundCheck."
/// </summary>
public class GroundCeilingCheck : MonoBehaviour
{
    [Header("Grounding and Jumping")]
    [SerializeField] LayerMask whatIsGround; // What layers are considered ground?
    [SerializeField] GameObject groundCheck; // If this object touches the ground, the gameObject is on the ground
    [SerializeField] float groundDetectRadius; // Distance that groundCheck checks to see if it is on the ground

    // Returns true if the player is Grounded and false otherwise
    // This code is meant to be run by an external class, and is not called within this class
    public bool GroundedCheck()
    {
        // Sets a CircleCast at groundCheck's position, with groundDetectRadius, checking for layers included in whatIsGround
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, groundDetectRadius, whatIsGround);

        // If the cast detected at least 1 collider labelled ground, continue.
        if (colliders.Length != 0)
        {
            // Check to see if any colliders detected are this gameObject
            for (int i = 0; i < colliders.Length; i++)
            {
                // Declare the gameObject isGrounded if the CircleCast found at least one ground layer that is not this gameobject
                if (colliders[i].gameObject != gameObject)
                {
                    return true;
                }
            }
        }

        return false;

    }

    // Draws the ground and ceiling checks in the scene view
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundDetectRadius);
    }
}
