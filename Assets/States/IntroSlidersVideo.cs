using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSlidersVideo : State, ITransitionable
{

    UnityEngine.Video.VideoPlayer video;
    bool _started = false;
    public bool Started
    {
        get => _started;
        set => _started = value;
    }

    public IntroSlidersVideo(HandTracking manager): base(manager) {}

    void Start()
    {
        video = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    public void Transition()
    {
        Manager.ToSliders();
    }

    public override void Execute(string[] points)
    {
        Manager.DrawHands(points);
        Manager.VideoTransition(video, this);
    }
}

