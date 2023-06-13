using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    public GameObject volumeButton;
    double volumeValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        volumeValue = (volumeButton.transform.position.y + 0.25) * 2;
        AudioListener.volume = (float)volumeValue;
    }
}
