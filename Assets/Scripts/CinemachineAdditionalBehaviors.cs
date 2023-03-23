using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineAdditionalBehaviors : MonoBehaviour
{
    [SerializeField] float lookAheadDistance = 5;
    [SerializeField] float adjustSpeed = 1;
    [SerializeField] float peekTarget = 1;
    private float SCREEN_Y = 0.5f;

    [SerializeField] Rigidbody2D followObject;
    [SerializeField] CinemachineVirtualCamera cinemachine;
    [SerializeField] CinemachineFramingTransposer transposer;

    // Start is called before the first frame update
    void Start()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
        transposer = GetComponentInChildren<CinemachineFramingTransposer>();
        followObject = cinemachine.Follow.GetComponentInParent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        if (followObject.bodyType == RigidbodyType2D.Kinematic) 
        { 
            if (Input.GetKey(KeyCode.S))
            {
                transposer.m_ScreenY = SmoothAdjust(transposer.m_ScreenY, -peekTarget + SCREEN_Y);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                transposer.m_ScreenY = SmoothAdjust(transposer.m_ScreenY, peekTarget + SCREEN_Y);
            }
            else
            {
                transposer.m_ScreenY = SmoothAdjust(transposer.m_ScreenY, SCREEN_Y);
            }

            transposer.m_TrackedObjectOffset = new Vector2(0, SmoothAdjust(transposer.m_TrackedObjectOffset.y, lookAheadDistance)); 
        }
        else 
        { 
            transposer.m_TrackedObjectOffset = new Vector2(0, SmoothAdjust(transposer.m_TrackedObjectOffset.y, 0)); 
        }
    }

    private float SmoothAdjust(float input, float target)
    {
        if (input > target)
        {
            input -= adjustSpeed * Time.deltaTime;
            if (input < target) { input = target; }
        }
        else if (input < target)
        {
            input += adjustSpeed * Time.deltaTime;
            if (input > target) { input = target; }
        }

        return input;
    }

}
