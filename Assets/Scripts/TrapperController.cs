using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapperController : MonoBehaviour
{
    [SerializeField] float detectRad;
    private BoxCollider2D bc;
    private TrapperStunScript trapperStun;
    private DamagePlayer dmgPlayer;
    private Animator trapperAnimator;

    // Start is called before the first frame update
    void Start()
    {
        trapperAnimator = GetComponentInParent<Animator>();
        bc = GetComponent<BoxCollider2D>();
        trapperStun = GetComponent<TrapperStunScript>();
        dmgPlayer = GetComponent<DamagePlayer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log(col.tag);
            if (col.GetComponent<GroundSlam>().isSlamming || trapperStun.isStunned)
            {
                trapperStun.Stun();
            }

            else
            {
                trapperAnimator.SetTrigger("IsBiting");
                Debug.Log(trapperAnimator.GetBool("IsBiting"));
                dmgPlayer.Hurt(col.gameObject);
            }

        }
    }
}
