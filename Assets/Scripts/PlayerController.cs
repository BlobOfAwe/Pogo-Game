using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The main controller for the player character. This class handles all basic actions the player makes, which are specific to the player.
/// Actions that are:
///     a) conditional, such as upgrades
///     b) generally applicable, such as damage scripts
/// Will not be included in the PlayerController for organizational purposes, though they will be referenced and called.
/// For instance, the player's jumping ability is a constant throughout the game, and is not used by any other entity, therefore it is included in this script.
/// The ability to detect when a gameObject is touching the ground is used by many entities, and so the PlayerController references an external DetectGround script.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Jumping")]
    // The equation for spring force is F = springConstant*springCharge
    [SerializeField] float springConstant = 10f; // A physics term representing how hard a spring is to move. A higher value means less charge is required to produce a lot of jump force.
    [SerializeField] float springCharge = 0; // Represents the displacement of a spring from its equilibrium position. This indicates how strong the jump will be.
    [SerializeField] float maxCharge = 30; // Represents the maximum displacement (compression) of the spring. This indicates the maximum power of a jump.
    [SerializeField] float springChargeRate = 0.5f; // How much charge is added to the spring each frame
    [SerializeField] float accelRate = 0.5f; // Represents the amount of time it takes for the spring to return to equilibrium position, ie; how long it takes for the jump force to be applied
    [SerializeField] float releaseTime; // Represents how much time is left since the jump began, before all the stored force has been applied to the pogo

    [Header("Rotation")]
    [SerializeField] float torque = 5; // The force applied when the player rotates
    [SerializeField] float antiTorque; // Represents the force trying to move the player to an upright position
    [SerializeField] float antiTorqueDenominator; // antiTorque is calculated using the player's current rotation devided by this variable.

    private Rigidbody2D playerRB; // The player's rigidbody (Assigned at Start())
    private GroundCeilingCheck groundCeilingCheck; // The GroundCheck class, used to identify when the player is touching the ground.

    // Start is called before the first frame update
    void Start()
    {
        // Assign relevant components from the player's gameObject
        playerRB = gameObject.GetComponent<Rigidbody2D>();
        groundCeilingCheck = gameObject.GetComponent<GroundCeilingCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the Space key is pressed or is being held...
        if (Input.GetKey(KeyCode.Space))
        {
            // ...and the springCharge is not at max...
            if (springCharge < maxCharge)
            {
                // ...increase the charge by the charge rate.
                springCharge += springChargeRate;
            }

            // ...and the springCharge is at or above the max...
            else
            {
                // ...set the charge to the maximum
                springCharge = maxCharge;
            }
        }
        
        // If the space key was released this frame...
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // ...and the player is grounded...
            if (groundCeilingCheck.GroundedCheck())
            {
                // ...start the release timer
                releaseTime = accelRate;
                // Begin the jump
                StartCoroutine("ReleaseSpring");
            }

            // ...and the player is not grounded...
            else
            {
                // ...reset the jump charge
                springCharge = 0;
            }
        }

    }

    // FixedUpdate is called at a fixed rate, whenever unity makes physics calculations. This may be more or less often than Update.
    private void FixedUpdate()
    {
        // antiTorque increases along with the player's rotation. This counterbalances the increase in gravatational torque as the player's rotation increases.
        antiTorque = playerRB.rotation / antiTorqueDenominator;
        
        // If the A key is pressed down or held
        if (Input.GetKey(KeyCode.A))
        {
            // Apply torque to the player's rigidbody CounterClockwise
            playerRB.AddTorque(torque);
        }

        // If the D key is pressed down or held
        else if (Input.GetKey(KeyCode.D))
        {
            // Apply torque to the player's rigidbody Clockwise
            playerRB.AddTorque(-torque);
        }

        // If neither key is being pressed down or held
        else
        {
            // Apply the antiTorque force to the player's rigidbody. Note this will always push the player towards a vertical orientation.
            playerRB.AddTorque(-antiTorque);            
        }
    }

    // IEnumerator is called independently, parallel to other processes
    // ReleaseSpring() releases the stored charge over a period of time, causing the player to jump.
    IEnumerator ReleaseSpring()
    {
        // As long as the relaseTime timer is not at 0
        while (releaseTime > 0)
        {
            float releasedCharge = springCharge * (Time.deltaTime / releaseTime); // Represents the change in displacement of the spring since the last call
            float jumpForce = releasedCharge * springConstant; // Represents the released force from the change in the spring's displacement
            
            // Apply the released force to the player's rigidbody on its local Y axis
            playerRB.AddRelativeForce(new Vector2(0, jumpForce));

            // Remove the released charge from the stored charge
            springCharge -= releasedCharge;

            // Remove the time passed from the time remaining
            releaseTime -= Time.deltaTime;

            // Wait for the next frame before being called again
            yield return null;
        }
    }
}
