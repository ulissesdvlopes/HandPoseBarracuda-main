using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousEnd : State
{

    UnityEngine.Video.VideoPlayer video;
    int[] lastReads;
    int readCount;
    int maxReads;

    public PreviousEnd(HandTracking manager): base(manager) {}

    void Start()
    {
        print("PREVIOUS END START");
        video = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        video.Play();
        maxReads = 100;
        lastReads = new int[maxReads];
        for (int i = 0; i < maxReads; i++)
        {
            lastReads.SetValue(2, i);
        }
    }

    bool WrongMarkerValue(int value)
    {
        return value == 3000;
    }

    void readMarker(int marker)
    {
        //print(marker);
        lastReads.SetValue(marker, readCount);
        readCount++;
        if(readCount >= maxReads)
        {
            readCount = 0;
        }
        if(Array.TrueForAll(lastReads, WrongMarkerValue))
        {
            if(Manager.HasFinished())
            {
                Manager.ToEndVideo();
            }
            else
            {
                Manager.ToInstructions();
            }
            
        }
    }

    public override void Execute(string[] points)
    {
        readMarker(int.Parse(points[127]));
        // print(video.isPlaying);
    }
}
