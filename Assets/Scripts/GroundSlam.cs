using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlam : MonoBehaviour
{
    private Rigidbody2D playerRB; // The player's rigidbody (Assigned at Start())
    private GroundCeilingCheck groundCheck; // The GroundCheck class, used to identify when the player is touching the ground.
    [SerializeField] float slamVelocity = 10; // The speed of the player when slamming
    public bool isSlamming; // Is the player in the middle of a slam

    // Start is called before the first frame update
    void Start()
    {
        // Assign component variables
        playerRB = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<GroundCeilingCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is pressing the left shift button, they are not grounded, and they are not already slamming
        if (Input.GetKey(KeyCode.S) && !groundCheck.grounded && !isSlamming)
        {
            isSlamming = true; // Mark the player as slamming
            playerRB.velocity = new Vector2(0, -slamVelocity); // Set the player's velocity to be directly downward at slamVelocity
        }

        // If the player is grounded and is marked as being in the middle of a slam, reset the slam.
        else if (groundCheck.grounded && isSlamming)
        {
            isSlamming = false;
        }
    }
}
