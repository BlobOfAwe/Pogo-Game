using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int baseHP = 3; // The player's starting HP
    public int hp; // Tracks the player's current HP, set to baseHP on start or on respawn
    public Vector2 checkpointPos; // The position the player is reset to after death
    private Rigidbody2D rb; // The player's rigidbody

    private void Start()
    {
        hp = baseHP; // Initialize HP
        rb = GetComponent<Rigidbody2D>(); // Initialize rigidbody
    }
    // Update is called once per frame
    void Update()
    {
        // When HP gets below 0, reset the player to the last checkpoint
        if (hp <= 0)
        {
            transform.position = checkpointPos;
            rb.velocity = Vector2.zero;
            rb.rotation = 0;
            hp = baseHP;
        }
    }
}
