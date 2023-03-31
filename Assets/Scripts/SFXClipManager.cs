using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXClipManager : MonoBehaviour
{
    [Header("Movement")]
    public AudioClip boost;
    public AudioClip dash;
    public AudioClip jumpPrep;
    public AudioClip jump;
    public AudioClip landing;
    public AudioClip slamImpact;

    [Header("HP")]
    public AudioClip damage;
    public AudioClip death;
    public AudioClip respawn;

    [Header("Enemy")]
    public AudioClip enemyStun;
    public AudioClip shooter;
    public AudioClip slimeAmbient;
    public AudioClip snalienAmbient;
    public AudioClip snapper;

    [Header("Level")]
    public AudioClip levelComplete;
    public AudioClip checkpoint;
    public AudioClip collect;
    public AudioClip woodBreaking;

}
