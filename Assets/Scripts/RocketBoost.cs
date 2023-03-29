using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBoost : MonoBehaviour
{
    [SerializeField] float rocketForce = 1; // The force applied to the player during the jump
    [SerializeField] float jumpTimer = 1; // The duration of the force to be applied to the player in seconds
    [SerializeField] float timer; // Represents the timer used by the coroutine in seconds
    [SerializeField] bool usedBoost; // Has the player used their boost yet?
    private Rigidbody2D playerRB; // The player's rigidbody (Assigned at Start())
    private GroundCeilingCheck groundCeilingCheck; // The GroundCheck class, used to identify when the player is touching the ground.

    // Start is called before the first frame update
    void Start()
    {
        // Assign component variables
        playerRB = GetComponent<Rigidbody2D>();
        groundCeilingCheck = GetComponent<GroundCeilingCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is not grounded, they have not used their boost, and the Space key is pressed...
        if (!groundCeilingCheck.grounded && !usedBoost && Input.GetKey(KeyCode.W))
        {
            timer = jumpTimer; // Set the timer
            usedBoost = true; // Mark the boost as used
            StartCoroutine("ApplyBoost"); // Apply the boost
        }

        // If the player is grounded, reset the boost
        if (groundCeilingCheck.grounded)
        {
            usedBoost = false;
        }
    }

    // Apply the boost force over a period of Timer seconds (Timer should be set from the jumpTimer variable)
    IEnumerator ApplyBoost()
    {
        // As long as the timer is not at 0
        while (timer > 0)
        {
            // Apply the released force to the player's rigidbody on its local Y axis
            playerRB.AddForce(new Vector2(0, rocketForce));

            // Remove the time passed from the time remaining
            timer -= Time.deltaTime;

            // Wait for the next physics update before being called again
            yield return new WaitForFixedUpdate();
        }
    }
}
