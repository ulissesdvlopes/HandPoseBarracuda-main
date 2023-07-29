using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVideo : State
{

    int startCount = 0;
    //AudioSource audioData;
    public SpriteRenderer spriteRenderer;
    UnityEngine.Video.VideoPlayer video;

    public StartVideo(HandTracking manager): base(manager) {}

    void Start()
    {
        //audioData = Manager.handHover.GetComponent<AudioSource>();
        spriteRenderer.color = Color.black;
        video = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    void OnEnable()
    {
        // video.Play();
        print("AQUIIIIIIIIIIIIIII");
    }

    void OnDisable()
    {
        // video.Stop();
        print("ALLLLLLIIIIIIIIIIIIIII");
    }

    public override void Execute(string[] points)
    {
        //Test();
        Manager.DrawHands(points);
        
        // functionality
        if (points[0] != "3000") {
            startCount++;
            spriteRenderer.color = Color.white;
            //audioData.Play(0);
            if (startCount > 500) {
                startCount = 0;
                Manager.ToTutorial();
                //Manager.hasStarted = true;
                //Deactivate();
            }

        } else {
            spriteRenderer.color = Color.black;
        }

    }

    void Test()
    {
        Debug.Log("Start video playing!!!");
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Manager.ToIntro();
        }
    }
}
