using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // If the player enters the attatched drigger, update the checkpointPos variable in the player's HealthManager
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponentInParent<HealthManager>().checkpointPos = transform.position;
        }
    }
}
