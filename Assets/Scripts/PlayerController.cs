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
    public float releaseTime; // Represents how much time is left since the jump began, before all the stored force has been applied to the pogo

    [Header("Rotation")]
    [SerializeField] float torque = 5; // How quickly the player rotates
    [SerializeField] LayerMask impassableLayerMask; // Which layers can the player not rotate through
    [SerializeField] float rightClamp; // The maximum possible rotation in the right-direction
    [SerializeField] float leftClamp; // The maximum possible rotation in the left-direction
    [SerializeField] float raycastDegreeInterval = 1; // How often the raycast checks for an obstacle. A lower number may result in lag
    [SerializeField] float defaultClamp = 270; // How many degrees the player can rotate when in midair or when no barrier to rotation is detected
    [SerializeField] float zeroDegreeCheck = 25f; // When a collider is detected at 0 degrees, check this many degrees to either side to see which side is blocked
    [SerializeField] float raycastCircleRadius; // The radius of the circlecast detecting impassable layers
    [SerializeField] float offsetRaycastStart = 0.1f; // How far from transform.position does the circleCast begin? This prevents the cast from detecting ground below the player as a wall.
    [SerializeField] float raycastDistance; // How far from transform.position does the raycast check. THIS DOES NOT ACCOUNT FOR OFFSETRAYCASTSTART
    [SerializeField] float raycastAngleDegrees; // What degree is the raycast currently checking
    [SerializeField] float raycastAngleX; // The calculated X value of the rotation (SIN of the angle)
    [SerializeField] float raycastAngleY; // The calculated Y value of the rotation (COS of the angle)
    [SerializeField] float startRaycastAngleFrom; // Where should the player begin raycasting for obstacles from

    [Header("Other")]
    [SerializeField] const int WHILELOOPKILL = 360; // Terminate a while loop after it runs this many times
    private int whileLoopTracker = 0; // Tracks the number of times a while loop has run

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
        // If the player is grounded and they are not in the middle of a jump...
        if (groundCeilingCheck.grounded && releaseTime <= 0)
        {
            // Make them kinematic, and freeze all momentum. This allows the player to rotate freely without dealing with gravity
            playerRB.bodyType = RigidbodyType2D.Kinematic;
            playerRB.velocity = Vector2.zero;
            playerRB.angularVelocity = 0;

            // Note the player's rotation as they land, this will be the angle the raycasts begin at
            startRaycastAngleFrom = 0;
        }
        
        // Otherwise, the player is jumping, allow physics to be applied normally
        else
        {
            playerRB.bodyType = RigidbodyType2D.Dynamic;
        }

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
            if (groundCeilingCheck.grounded)
            {
                // ...start the release timer
                releaseTime = accelRate;
                // ...make the player Dynamic...
                playerRB.bodyType = RigidbodyType2D.Dynamic;
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

        if (playerRB.rotation > leftClamp)
        {
            playerRB.rotation = leftClamp;
        }

        else if (playerRB.rotation < rightClamp)
        {
            playerRB.rotation = rightClamp;
        }

        if (groundCeilingCheck.grounded)
        {
            AssignClamps(false);
            AssignClamps(true);
        }

        else
        {
            rightClamp = -defaultClamp;
            leftClamp = defaultClamp;
        }

        
    }

    // FixedUpdate is called at a fixed rate, whenever unity makes physics calculations. This may be more or less often than Update.
    private void FixedUpdate()
    {        
        // If the A key is pressed down or held
        if (Input.GetKey(KeyCode.A) && playerRB.rotation <= leftClamp)
        {
            // Apply torque to the player's rigidbody CounterClockwise
            // playerRB.AddTorque(torque);
            playerRB.MoveRotation(playerRB.rotation + torque);
        }

        // If the D key is pressed down or held
        else if (Input.GetKey(KeyCode.D) && playerRB.rotation >= rightClamp)
        {
            // Apply torque to the player's rigidbody Clockwise
            // playerRB.AddTorque(-torque);
            playerRB.MoveRotation(playerRB.rotation - torque);
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

    // Raycast right or left depending on parameter, detecting impassable colliders, and clamping the player's rotation in that direction to prevent rotating into objects
    private void AssignClamps(bool assignRightClamp)
    {
        // Initialize Variables
        bool detectedImpassable = false; // Has the raycast detected an impassable collider yet?
        whileLoopTracker = 0;
        raycastAngleDegrees = startRaycastAngleFrom;

        // While the raycast has not detected an impassable collider...
        while (!detectedImpassable)
        {
            // If the while loop has exceeded the WHILELOOPKILL limit, assign the right or left clamp to its default
            if (whileLoopTracker >= WHILELOOPKILL)
            {
                //Debug.Log("Did not detect impassable collider, maximized rotation");
                if (assignRightClamp)
                {
                    rightClamp = -defaultClamp;
                }
                else if (!assignRightClamp)
                {
                    leftClamp = defaultClamp;
                }

                // If it cannot figure out whether to assign the right or left clamp, log an error
                else
                {
                    Debug.LogError("Error: Right or Left clamp not specified");
                }

                // Break out of the while loop
                break;
            }

            // Once the loop has passed the check, add one to the loop tracker
            whileLoopTracker += 1;

            // Assign the vector values corresponding to the raycast angle
            raycastAngleX = Mathf.Sin(-raycastAngleDegrees * Mathf.Deg2Rad);
            raycastAngleY = Mathf.Cos(-raycastAngleDegrees * Mathf.Deg2Rad);

            // Send a circle-cast in the direction of the raycast angle, starting from the offsetRaycastStart and ending at the raycastDistance, detecting impassableLayerMask
            RaycastHit2D hit = Physics2D.CircleCast(
                new Vector2(
                    transform.position.x + (raycastAngleX * offsetRaycastStart),
                    transform.position.y + (raycastAngleY * offsetRaycastStart)
                ),
                raycastCircleRadius,
                 new Vector2(
                    raycastAngleX,
                    raycastAngleY
                 ),
                 raycastDistance,
                 impassableLayerMask
            );

            // If the raycast detects an impassable layer...
            if (hit)
            {

                detectedImpassable = true; // Shows the raycast was successful

                // If we are checking and assigning the raycast to the right, assign the raycast's angle to the right clamp
                if (assignRightClamp)
                {
                    rightClamp = raycastAngleDegrees;
                }

                // If we are not checking and assigning to the right, assign the raycast's angle to the left clamp
                else if (!assignRightClamp)
                {
                    leftClamp = raycastAngleDegrees;
                }

                // If the parameter is undefined, or has an otherwise invalid value, throw an error
                else
                {
                    Debug.LogError("Error: Right or Left clamp not specified");
                }

                // Log the angle where the collider was detected
                //Debug.Log("Detected Impassable object (" + hit.collider.name + ") at angle: " + raycastAngleDegrees);
            }

            // If the raycast does not detect an impassable layer
            else
            {
                // If the parameter says we are assigning to the right, subtract the raycastDegreeInterval from the raycasting angle before trying again
                if (assignRightClamp)
                {
                    raycastAngleDegrees -= raycastDegreeInterval;
                }

                // If the parameter says we are not assigning to the right, add the raycastDegreeInterval to the raycasting angle before trying again
                else if (!assignRightClamp)
                {
                    raycastAngleDegrees += raycastDegreeInterval;
                }

                // If the parameter is undefined, or has an otherwise invalid value, throw an error
                else
                {
                    Debug.LogError("Error: Right or Left clamp not specified");
                }
                
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(
            new Vector2(
                transform.position.x + (raycastAngleX * offsetRaycastStart),
                transform.position.y + (raycastAngleY * offsetRaycastStart)
            ),
            new Vector2(
                transform.position.x + (raycastAngleX * raycastDistance), 
                transform.position.y + (raycastAngleY * raycastDistance)
            )
        );

        Gizmos.DrawWireSphere(
            new Vector2(
                transform.position.x + (raycastAngleX * raycastDistance),
                transform.position.y + (raycastAngleY * raycastDistance)
            ), 
            raycastCircleRadius
        );
    }
}
