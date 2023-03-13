using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    /// <summary>
    /// this script handles the colissions of the bullet
    /// if it collides with the ground/wall or player then it destroys itself
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);

            //damage and knock back the player
        }
    }
}

