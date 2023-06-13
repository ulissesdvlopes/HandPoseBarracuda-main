using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    public GameObject rotation;
    public GameObject coloring;
    public GameObject progressing;
    public GameObject[] model;
    public GameObject[] modelPivot;
    public GameObject modelComplete;
    public GameObject modelCompletePivot;
    public GameObject modelSliceComplete;
    public GameObject modelBreakComplete;
    public GameObject[] modelBreak;
    public GameObject bar;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //Get Values
        float rotateValue = rotation.transform.localPosition[0] * 5;
        float colorValue = coloring.transform.localPosition[0] * 5;
        float progressingValue = progressing.transform.localPosition[0] * 5;
        
        
        if (rotation.transform.localPosition[0] < 0) {
            rotation.transform.localPosition = new Vector3(0, rotation.transform.localPosition.y, rotation.transform.localPosition.z);
        }
        if (rotation.transform.localPosition[0]  > 0.2f) {
            rotation.transform.localPosition  = new Vector3(0.2f, rotation.transform.localPosition.y, rotation.transform.localPosition.z);
        }
        if (coloring.transform.localPosition[0]  < 0) {
            coloring.transform.localPosition  = new Vector3(0, coloring.transform.localPosition.y, coloring.transform.localPosition.z);
        }
        if (coloring.transform.localPosition[0]  > 0.2f) {
            coloring.transform.localPosition  = new Vector3(0.2f, coloring.transform.localPosition.y, coloring.transform.localPosition.z);
        }
        if (progressing.transform.localPosition[0]  < 0) {
            progressing.transform.localPosition  = new Vector3(0, progressing.transform.localPosition.y, progressing.transform.localPosition.z);
        }
        if (progressing.transform.localPosition[0]  > 0.2f) {
            progressing.transform.localPosition  = new Vector3(0.2f, progressing.transform.localPosition.y, progressing.transform.localPosition.z);
        }
        
        /*if (bar.transform.localScale.x == 0.1 || bar.transform.localScale.x == 0) {
            if (progressingValue == 0) {
                modelCompletePivot.SetActive(true);
                modelSliceComplete.SetActive(false);
            } else if (progressingValue > 0) {
                modelCompletePivot.SetActive(false);
                modelSliceComplete.SetActive(true);
            }
        }*/
        //Audio
        var rotateAudio = rotation.GetComponent<AudioSource>();
        var colorAudio = coloring.GetComponent<AudioSource>();
        var progressingAudio = progressing.GetComponent<AudioSource>();
        rotateAudio.volume = (float)rotateValue;
        colorAudio.volume = (float)colorValue;
        progressingAudio.volume = (float)progressingValue;

        //Complete
        modelCompletePivot.transform.rotation = Quaternion.Euler(0, rotateValue * 360, 0);
        var modelCompleteRenderer = modelComplete.GetComponent<Renderer>();
        modelCompleteRenderer.material.SetColor("_Color", Color.HSVToRGB((float)colorValue, 0.7f, 1.0f));

        //Break
        modelBreakComplete.transform.rotation = Quaternion.Euler(0, rotateValue * 360, 0);
        for (int i = 0; i < 103; i++) {
            var modelBreakRenderer = modelBreak[i].GetComponent<Renderer>();
            modelBreakRenderer.material.SetColor("_Color", Color.HSVToRGB((float)colorValue, 0.7f, 1.0f));
        }

        //Slices
        modelSliceComplete.transform.rotation = Quaternion.Euler(0, rotateValue * 360, 0);
         for (int i  = 0; i < 24; i++) {
            var modelRenderer = model[i].GetComponent<Renderer>();
            modelRenderer.material.SetColor("_Color", Color.HSVToRGB((float)colorValue, 0.7f, 1.0f));
         }
        for (int i  = 0; i < 15; i++) {
            if (progressingValue * i * 360 >= 720) {
                modelPivot[i].transform.rotation = Quaternion.Euler(0, rotateValue * 360 + 720, 0);
            } else {
                modelPivot[i].transform.rotation = Quaternion.Euler(0, rotateValue * 360 + progressingValue * (i + 1) * 360, 0);
            }
            //model[i].transform.rotation = Quaternion.Euler(0, rotateValue *  * 720, 0);
            //modelRenderer.material.SetFloat("_Progression", (float)proAlter);
        }
    }
}
//Color.HSVToRGB((float)hueAlter, 0.6f, 1.0f)