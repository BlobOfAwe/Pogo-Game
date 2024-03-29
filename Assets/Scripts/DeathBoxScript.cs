using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoxScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject player = other.gameObject;
            player.GetComponentInParent<HealthManager>().hp = 0;
        }
    }
}
