using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int baseHP = 3;
    public int hp;
    public Vector2 checkpointPos;
    private Rigidbody2D rb;

    private void Start()
    {
        hp = baseHP;
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            transform.position = checkpointPos;
            rb.velocity = Vector2.zero;
            rb.rotation = 0;
            hp = baseHP;
        }
    }
}
