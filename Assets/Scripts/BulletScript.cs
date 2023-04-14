using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    /// <summary>
    /// this script handles the colissions of the bullet
    /// if it collides with the ground/wall or player then it destroys itself
    /// </summary>
    private AudioClip aud;
    private DamagePlayer dmgPlr;
    private CircleCollider2D col;
    private SpriteRenderer spr;

    private void Start()
    {
        dmgPlr = GetComponent<DamagePlayer>();
        col = GetComponent<CircleCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        aud = GetComponent<AudioSource>().clip;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground"))
        {
            dmgPlr.enabled = false;
            col.enabled = false;
            spr.enabled = false;
            Destroy(gameObject, aud.length);
        }
    }
}

