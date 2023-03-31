using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapperController : MonoBehaviour
{
    private BoxCollider2D bc;
    private TrapperStunScript trapperStun;
    private Animator trapperAnimator;
    // Start is called before the first frame update
    void Start()
    {
        trapperAnimator = GetComponentInParent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        trapperStun = FindObjectOfType<TrapperStunScript>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Checks if the tag is assigned as Player
        if (other.gameObject.tag == "Player")
        {
            //Initiates the stun function
           trapperAnimator.SetTrigger("IsBiting");
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (trapperStun.isStunned)
        {
            bc.enabled = false;
        }
        else
        {
            bc.enabled = true;
        }
    }

}
