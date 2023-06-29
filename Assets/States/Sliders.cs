using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliders : State
{
    public GameObject[] slidersAssets;
    public GameObject modeloFatiado;
    public GameObject[] sliders;
    public GameObject botao;
    public GameObject cube;
    public GameObject handHover;
    int[] lastReads;
    int readCount;
    int maxReads;

    public Sliders(HandTracking manager): base(manager) {}

    void Start()
    {
        maxReads = 200;
        lastReads = new int[maxReads];
        for (int i = 0; i < maxReads; i++)
        {
            lastReads.SetValue(1, i);
        }
        
        foreach (GameObject element in slidersAssets) {
            element.SetActive(true);
        }
        // for (int i = 0; i < 3; i++) {
        //     sliders[i].SetActive(true);
        // }
        // cube.SetActive(true);
        // modeloFatiado.SetActive(true);
        
    }

    bool WrongMarkerValue(int value)
    {
        return value != 1;
    }

    void readMarker(int marker)
    {
        //print(marker);
        lastReads.SetValue(marker, readCount);
        readCount++;
        if(readCount >= maxReads)
        {
            readCount = 0;
        }
        if(Array.TrueForAll(lastReads, WrongMarkerValue))
        {
            Manager.SetFinished(1);
            Manager.ToInstructions();
        }
    }

    public override void Execute(string[] points)
    {
        // readMarker(int.Parse(points[127]));
        Manager.DrawHands(points);
        for (int i = 0; i < 3; i++) {
            sliders[i].SetActive(true);
        }
        cube.SetActive(true);
        botao.SetActive(true);
        modeloFatiado.SetActive(true);
    }
}

