using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVideo : State
{

    int startCount = 0;
    //AudioSource audioData;
    public SpriteRenderer spriteRenderer;

    public StartVideo(HandTracking manager): base(manager) {}

    void Start()
    {
        //audioData = Manager.handHover.GetComponent<AudioSource>();
        spriteRenderer.color = Color.black;
    }

    public override void Execute(string[] points)
    {
        Test();
        
        // functionality
        if (points[0] != "3000") {
            startCount++;
            spriteRenderer.color = Color.white;
            //audioData.Play(0);
            if (startCount > 500) {

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
