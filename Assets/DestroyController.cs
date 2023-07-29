using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyController : MonoBehaviour
{
    float countMax = 1000;
    float count = 0;
    float countCracked = 0;
    float countCrackedMax = 300;
    bool concluded = false;
    bool isGoing = false;
    bool rotated = false;
    bool active = false;
    bool cracked = false;
    bool onlyOnce = false;
    int countCollider = 0;
    public GameObject bar;
    public GameObject slider;
    public GameObject slider2;
    public GameObject model;
    public GameObject modelSliced;
    public GameObject destroyedModel;
    public SpriteRenderer spriteRenderer;
    public Sprite oldSprite;
    public Sprite newSprite;
    // public GameObject Manager;
    public GameObject SlidersGO;
    private Sliders SlidersController;
    AudioSource audioData;

    Vector3 originalPosition;
    Quaternion originalRotation;


    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        // HT = Manager.GetComponent<HandTracking>();
        SlidersController = SlidersGO.GetComponent<Sliders>();
        var piece = destroyedModel.transform.GetChild(0).GetChild(0);
        originalPosition = piece.position;
        originalRotation = piece.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (cracked) {
            countCracked++;
            // print(countCracked);
        }
        if (countCracked >= countCrackedMax) {

            countCracked = 0;
            count = 0;
            concluded = false;
            isGoing = false;
            rotated = false;
            active = false;
            cracked = false;
            //model.SetActive(true);
            modelSliced.SetActive(true);
            destroyedModel.SetActive(false);
            resetDestroyedModel();
            // HT.SetFinished(1);
            // HT.ToPreviousEnd();
            SlidersController.Transition();

            onlyOnce = false;
        }
        if (active) {
            spriteRenderer.sprite = newSprite; 
            if (!onlyOnce) {
                audioData.Play(0);
                onlyOnce = true;
            }
        } else {
            spriteRenderer.sprite = oldSprite;
        }
        if (countCollider == 0) { 
            concluded = false;
            count = 0;
        }
        if (count > 0 && !concluded) {
            bar.SetActive(true);
            float progress = count / 30000;
            bar.transform.localScale = new Vector3((float)progress, 0.03f, 0.01f);
        } else {
            bar.SetActive(false);
        }
        //if (isGoing && slider2.transform.localPosition[0] < -0.12) {
            //slider2.transform.localPosition = new Vector3(slider2.transform.localPosition.x + 0.0005f, slider2.transform.localPosition.y, slider2.transform.localPosition.z);
        //} else if (isGoing && slider2.transform.localPosition[0] > -0.13) {
            //slider2.transform.localPosition = new Vector3(slider2.transform.localPosition.x - 0.0005f, slider2.transform.localPosition.y, slider2.transform.localPosition.z);
        //} else if (isGoing) {
            //rotated = true;
        //}
        float valueSlider = slider.transform.localPosition[0] * 5;
        if (isGoing && valueSlider > 0) {
            slider.transform.localPosition = new Vector3(0, slider.transform.localPosition.y, slider.transform.localPosition.z);
        } else if(isGoing && valueSlider == 0) {
            destroyedModel.SetActive(true);
            model.SetActive(false);
            modelSliced.SetActive(false);
            isGoing = false;
            rotated = false;
            cracked = true;
        }
    }

    void resetDestroyedModel()
    {
        foreach(Transform cont in destroyedModel.transform)
        {
            var piece = cont.GetChild(0);
            piece.position = originalPosition;
            piece.rotation = originalRotation;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        countCollider++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        active = false;
        countCollider--;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        active = true;
        if (count < countMax) {
            count++;
        } else {
            isGoing = true;
            if (!isGoing) {
                print("Deu certo!");
                concluded = true;
                cracked = true;
            }
            
        }
    }


}
