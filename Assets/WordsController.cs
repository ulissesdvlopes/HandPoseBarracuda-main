using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordsController : MonoBehaviour
{
    private bool finalAnimationStarted = false;
    private bool finalAnimationEnded = false;
    private Vector3 positionIncrement = new Vector3(0.0005f,-0.0005f,0);
    private Vector3 scaleIncrement = new Vector3(0.00009f,0.00009f,0.00009f);
    private int counter = 0;
    private const int maxCounter = 100;
    private HandTracking manager;
    private Words words;
    // Start is called before the first frame update
    void Start()
    {
        if (manager == null) 
        {
            // var go = GameObject.FindWithTag("Manager");
            // manager = go.GetComponent<HandTracking>();
            var go = GameObject.FindWithTag("Words");
            words = go.GetComponent<Words>();
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(finalAnimationEnded)
        {
            counter++;
            if(counter > maxCounter)
            {
                print("ACABOU");
                // manager.SetFinished(0);
                // manager.ToPreviousEnd();
                words.Transition();
            }
            return;
        }

        if(finalAnimationStarted)
        {
            transform.position = transform.position + positionIncrement;
            transform.localScale = transform.localScale + scaleIncrement;
            // print(transform.position.y);
            // if(transform.position.y < 1.653) {
            if(transform.localScale.x > 0.049) {
                finalAnimationEnded = true;
            }
        }
        
        foreach(Transform word in transform)
            {
                // print(word.gameObject.tag);
                var rb = word.GetComponent<Rigidbody2D>();
                if(rb != null)
                {
                    if(finalAnimationStarted) {
                        word.gameObject.SetActive(false);
                    }
                    rb.velocity = Vector2.ClampMagnitude(rb.velocity, 0.2f);
                } 
                // else {
                //     if(finalAnimationStarted) {
                //         word.position = word.position + positionIncrement;
                //         if(word.position.y < -77.0f) {
                //             print("END ANIMATION");
                //             positionIncrement = new Vector3(0,0,0);
                //         }
                //     }
                // }
                if(word.gameObject.tag == "lastWord" && !finalAnimationStarted)
                {
                    print("LAST WORD");
                    word.gameObject.SetActive(false);
                    finalAnimationStarted = true;
                }
            }

    }
}
