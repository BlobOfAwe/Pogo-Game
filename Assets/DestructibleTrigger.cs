using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered Trigger");
        GetComponentInParent<WhiteboxDestructibleObject>().TriggerDestroy(collision);
    }
}