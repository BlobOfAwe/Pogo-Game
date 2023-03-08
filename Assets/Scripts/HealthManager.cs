using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] int baseHP;
    public int hp = 3;
    public Vector2 checkpointPos;

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            transform.position = checkpointPos;
            hp = baseHP;
        }
    }
}
