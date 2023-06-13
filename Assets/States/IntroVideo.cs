using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroVideo : State, ITransitionable
{
    public IntroVideo(HandTracking manager): base(manager) {}
    UnityEngine.Video.VideoPlayer video;
    bool _started = false;
    public bool Started
    {
        get => _started;
        set => _started = value;
    }

    void Start()
    {
        video = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    public void Transition()
    {
        Manager.ToInstructions();
    }

    public override void Execute(string[] points)
    {
        //Debug.Log("Intro agora");
        Manager.DrawHands(points);
        Manager.VideoTransition(video, this);
    }
}

