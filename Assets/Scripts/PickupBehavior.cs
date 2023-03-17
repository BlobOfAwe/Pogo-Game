using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    [SerializeField] int stickerID;
    private StickerManager stickerManager;


    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            stickerManager = col.GetComponentInParent<StickerManager>();

            try 
            { 
                stickerManager.stickers[stickerID].SetActive(true);
                gameObject.SetActive(false);
            }
            catch { Debug.LogError("No sticker found with specified ID"); }
        }
    }
}
