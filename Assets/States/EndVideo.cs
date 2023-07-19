using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndVideo : State
{

    UnityEngine.Video.VideoPlayer video;
    public EndVideo(HandTracking manager): base(manager) {}
    int counter = 0;
    const int counterLimit = 5000;

    void Start()
    {
        video = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        video.Play();
    }


    public override void Execute(string[] points)
    {
        Manager.DrawHands(points);
        if(counter < counterLimit)
        {
            counter++;
        }
        else
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Manager.ToStart();
        }
        
    }
}
