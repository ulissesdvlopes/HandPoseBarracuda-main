using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroWordsVideo : State, ITransitionable
{

    UnityEngine.Video.VideoPlayer video;
    bool _started = false;
    public bool Started
    {
        get => _started;
        set => _started = value;
    }

    public IntroWordsVideo(HandTracking manager): base(manager) {}

    void Start()
    {
        video = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    void onEnable()
    {
        
    }

    public void Transition()
    {
        this.Started = false;
        //video.Stop();
        Manager.ToWords();
    }

    public override void Execute(string[] points)
    {
        Manager.DrawHands(points);
        Manager.VideoTransition(video, this);
    }
}

