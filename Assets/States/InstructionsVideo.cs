using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsVideo : State
{
    public InstructionsVideo(HandTracking manager): base(manager) {}

    void Transition(float marker)
    {
        Debug.Log(marker);
        if(marker == 0)
        {
            Manager.ToIntroWords();
        }
        if(marker == 1)
        {
            Manager.ToIntroSliders();
        }
    }

    public override void Execute(string[] points)
    {
        Transition(float.Parse(points[127]));
        Manager.DrawHands(points);
    }
}

