using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDash : MonoBehaviour
{
    [SerializeField] float dashSpeed = 1; // The speed of the player's dash
    [SerializeField] bool dashLeft; // Is the player dashing left (true) or right (false)
    [SerializeField] bool usedDash; // Has the player used their dash yet?
    private Rigidbody2D playerRB; // The player's rigidbody (Assigned at Start())
    private GroundCeilingCheck groundCeilingCheck; // The GroundCheck class, used to identify when the player is touching the ground.
    private PlayerController playerController; // The PlayerController class, used for the player's basic movement

    // Start is called before the first frame update
    void Start()
    {
        // Assign the component variables
        playerRB = GetComponent<Rigidbody2D>();
        groundCeilingCheck = GetComponent<GroundCeilingCheck>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is not grounded, has not used their dash yet, and presses the left arrow key...
        if (!groundCeilingCheck.grounded && !usedDash && Input.GetKey(KeyCode.LeftArrow))
        {
            usedDash = true; // Mark the dash as used
            playerRB.velocity = new Vector2(-dashSpeed, playerRB.velocity.y); // Add the dash speed to the player's velocity left
        }

        // If the player is not grounded, has not used their dash yet, and presses the right arrow key...
        else if (!groundCeilingCheck.grounded && !usedDash && Input.GetKey(KeyCode.RightArrow))
        {
            usedDash = true; // Mark the dash as used
            playerRB.velocity = new Vector2(dashSpeed, playerRB.velocity.y); // Add the dash speed to the player's velocity right
        }

        // If the player is grounded, reset the dash
        if (groundCeilingCheck.grounded)
        {
            usedDash = false;
        }
    }
}
