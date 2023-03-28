using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapperStunScript : MonoBehaviour
{

    public bool isStunned = false;
    GroundSlam groundSlam;

    //If the enemy collides with something it checks to see what it is, if it is the player then it initiates the stun function
    private void Start()
    {
        groundSlam = FindObjectOfType<GroundSlam>();
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (groundSlam.isSlamming)
            {
                Stun();
            }
        }
    }
    //Stun function checks to see if the enemy is not stunned, then stuns it
    public void Stun()
    {
        //Checks if the enemy is not stunned  
        if (!isStunned)
        {
            //Stuns the enemy if it is indeed stunned
            isStunned = true;
            Debug.Log("Trapper is now stunned");
        }
    }
}
