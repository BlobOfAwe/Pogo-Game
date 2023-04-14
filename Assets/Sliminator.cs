using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliminator : MonoBehaviour
{
    [SerializeField] int threshold;
    void Awake()
    {
        if (SlimeTracker.slimeScore < threshold)
        {
            gameObject.SetActive(false);
        }
    }
}
