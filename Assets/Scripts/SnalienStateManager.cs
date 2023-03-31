using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnalienStateManager : MonoBehaviour
{
    [SerializeField] GameObject activeBoxes;
    [SerializeField] GameObject stunnedBox;
    [SerializeField] float timeStunned;
    [SerializeField] float stunCooldown = 5f;
    private EnemyStun stunScript;

    // Start is called before the first frame update
    void Start()
    {
        stunScript = GetComponentInChildren<EnemyStun>();
        timeStunned = 0;
        activeBoxes.SetActive(true);
        stunnedBox.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stunScript.isStunned)
        {
            activeBoxes.SetActive(false);
            stunnedBox.SetActive(true);
            timeStunned += Time.deltaTime;
            if (timeStunned > stunCooldown)
            {
                stunScript.isStunned = false;
            }
        }
        else
        {
            activeBoxes.SetActive(true);
            stunnedBox.SetActive(false);
            timeStunned = 0;
        }
    }
}
