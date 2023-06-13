using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndVideo : State
{

    UnityEngine.Video.VideoPlayer video;
    public EndVideo(HandTracking manager): base(manager) {}
    int counter = 0;
    int counterLimit = 6000;

    void Start()
    {
        video = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        video.Play();
    }


    public override void Execute(string[] points)
    {
        if(counter < counterLimit)
        {
            counter++;
        }
        else
        {
            Manager.TheEnd();
        }
        
    }
}
