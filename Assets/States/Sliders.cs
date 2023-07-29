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
    public GameObject slidersContainer;
    public GameObject handHover;
    public GameObject warning;
    private int warningCount = 0;
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
        slidersContainer.SetActive(false);
        botao.SetActive(false);
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
            Manager.ToPreviousEnd();
        }
    }

    private void executeWarning()
    {
        if(warningCount < 1500)
        {
            // if(!warning.activeInHierarchy)
            // {
            //     warning.SetActive(true);
            //     slidersContainer.SetActive(false);
            //     botao.SetActive(false);
            // }
            warningCount++;
        }
        else 
        {
            if(warning.activeInHierarchy)
            {
                warning.SetActive(false);
                slidersContainer.SetActive(true);
                botao.SetActive(true);
            }
            warningCount = 0;
        }
    }

    public void Transition()
    {
        warning.SetActive(true);
        slidersContainer.SetActive(false);
        botao.SetActive(false);
        Manager.SetFinished(1);
        Manager.ToPreviousEnd();
    }

    public override void Execute(string[] points)
    {
        // readMarker(int.Parse(points[127]));
        Manager.DrawHands(points);
        // for (int i = 0; i < 3; i++) {
        //     sliders[i].SetActive(true);
        // }
        cube.SetActive(true);
        modeloFatiado.SetActive(true);
        executeWarning();
    }
}

