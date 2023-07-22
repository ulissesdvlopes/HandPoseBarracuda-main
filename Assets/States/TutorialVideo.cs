using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialVideo : State
{
    int startCount = 0;
    public SpriteRenderer spriteRenderer;
    bool started = false;
    public GameObject initButton;
    public GameObject instructions;

    public TutorialVideo(HandTracking manager): base(manager) {}

    void Start()
    {
        // spriteRenderer.color = Color.black;
    }

    public override void Execute(string[] points)
    {
        // Manager.DrawHands(points);
        startCount++;
            // spriteRenderer.color = Color.white;
            //audioData.Play(0);
            if (startCount > 2750) {
                Manager.ToIntro();
                // started = true;
                // initButton.SetActive(true);
                // spriteRenderer.gameObject.SetActive(false);
            }

        // if (points[0] != "3000") {
        //     startCount++;
        //     // spriteRenderer.color = Color.white;
        //     //audioData.Play(0);
        //     if (startCount > 1000) {
        //         Manager.ToIntro();
        //         // started = true;
        //         // initButton.SetActive(true);
        //         // spriteRenderer.gameObject.SetActive(false);
        //     }

        // } 
        // else {
        //     spriteRenderer.color = Color.black;
        // }

        // if(started)
        // {
        //     Manager.DrawHands(points);
        // }

    }
}

