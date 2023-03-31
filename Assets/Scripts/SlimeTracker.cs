using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlimeTracker : MonoBehaviour
{
    // Start is called before the first frame update
    public static SlimeTracker Instance;
    private int slimeCount;
    public TextMeshProUGUI slimeCountText;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        slimeCountText.text = "SlimesHunted: " + slimeCount;
    }

    public void CounterIncrement()
    {
        slimeCount++;
    }
}
