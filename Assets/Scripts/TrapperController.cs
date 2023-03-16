using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapperController : MonoBehaviour
{
    private BoxCollider2D bc;
    private TrapperStunScript trapperStun;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        trapperStun = FindObjectOfType<TrapperStunScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trapperStun.isStunned)
        {
            bc.enabled = false;
        }
    }
}
