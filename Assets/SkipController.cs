using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SkipController : MonoBehaviour
{
    private HandTracking managerScript;
    float countMax = 100;
    int countCollider = 0;
    public UnityEvent TransictionFunction;
    bool hover = false;
    public GameObject bar;

    void Start()
    {
    }

    void onEnable()
    {
        countCollider = 0;
        print("onEnable Skip");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(countCollider >= countMax) {
            countCollider = 0;
            TransictionFunction.Invoke();
            return;
        }
        ControlHover();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        hover = true;
        //countCollider++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        hover = false;
    }

    void ControlHover()
    {
        float progress = ((float)countCollider) / 10;
        bar.transform.localScale = new Vector3(progress, 1.0f, 0.01f);
        if(hover) {
            countCollider++;
            // print(countCollider);
            //bar.SetActive(true);
            
            return;
        }
        // if(countCollider > 0) {
        //     countCollider--;
        //     return;
        // }
    }


}
