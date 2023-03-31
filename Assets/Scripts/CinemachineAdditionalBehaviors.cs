using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineAdditionalBehaviors : MonoBehaviour
{
    [SerializeField] float lookAheadDistance = 5; // How far forward on the player's local y axis should the camera be looking
    [SerializeField] float adjustSpeed = 1; // How quickly the camera adjusts to a new position
    [SerializeField] float peekTarget = 1; // How far does the screen shift when peeking
    [SerializeField] float waitToLook = 5; // How long in seconds does the player need to be in the air before the camera offset looks below the player
    private float SCREEN_Y = 0.5f; // The default position of the Screen's Y position
    private bool waiting; // True if the cinemachine is not offset to be below the player
    private float timeDynamic; // How long in seconds the player has spent dynamic
    private float timeKinematic; // How long in seconds the player has spent kinematic

    [SerializeField] Rigidbody2D followObject; // The object followed by the cinemachine
    [SerializeField] CinemachineVirtualCamera cinemachine; // The cinemachine
    [SerializeField] CinemachineFramingTransposer transposer; // The cinemachine Body subcomponent

    // Start is called before the first frame update
    void Start()
    {
        // Initialize variables
        cinemachine = GetComponent<CinemachineVirtualCamera>();
        transposer = GetComponentInChildren<CinemachineFramingTransposer>();
        followObject = cinemachine.Follow.GetComponentInParent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        // If the followObject (the player) is kinematic
        if (followObject.bodyType == RigidbodyType2D.Kinematic) 
        {
            timeDynamic = 0;

            // Allow the player to peek the screen up and down
            if (Input.GetKey(KeyCode.S))
            {
                transposer.m_ScreenY = SmoothAdjust(transposer.m_ScreenY, -peekTarget + SCREEN_Y);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                transposer.m_ScreenY = SmoothAdjust(transposer.m_ScreenY, peekTarget + SCREEN_Y);
            }

            // Set the screen to its default position
            else
            {
                transposer.m_ScreenY = SmoothAdjust(transposer.m_ScreenY, SCREEN_Y);
            }

            // Set the tracked object offset to be above the player in local space
            transposer.m_TrackedObjectOffset = new Vector2(0, SmoothAdjust(transposer.m_TrackedObjectOffset.y, lookAheadDistance));
        }

        // If the followed object (the player) is NOT kinematic...
        else
        {
            timeKinematic = 0;
            if (timeDynamic < waitToLook) { timeDynamic += Time.deltaTime; Debug.Log(timeDynamic); }
            else { transposer.m_TrackedObjectOffset = new Vector2(0, SmoothAdjust(transposer.m_TrackedObjectOffset.y, -lookAheadDistance)); Debug.Log("adjusting camera"); }
        }
    }

    // Return a float that is between input and target based on adjustSpeed. If repeatedly called, eventually it will reach the target number
    private float SmoothAdjust(float input, float target)
    {
        // If the input is greater than the target
        if (input > target)
        {
            // Subtract from the input based on how much time has passed since last frame
            input -= adjustSpeed * Time.deltaTime;

            // If the subtraction would put the input below the target, instead set the input to the target
            if (input < target) { input = target; }
        }

        // If the input is less than the target
        else if (input < target)
        {
            // Add to the input based on how much time has passed since last frame
            input += adjustSpeed * Time.deltaTime;

            // If the addition would put the input above the target, instead set the input to the target
            if (input > target) { input = target; }
        }

        // Return the adjusted input value
        return input;
    }

}
