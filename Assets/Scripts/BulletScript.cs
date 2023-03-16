using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    /// <summary>
    /// this script handles the colissions of the bullet
    /// if it collides with the ground/wall or player then it destroys itself
    /// </summary>
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == ("Ground"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == ("Player"))
        {
            Destroy(gameObject);

            //damage and knock back the player
        }
    }
}

