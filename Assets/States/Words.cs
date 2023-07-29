using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Words : State
{
    public GameObject[] wordsAssets;
    public GameObject container;
    public GameObject wordsPrefab;
    private GameObject instantiatedWords;
    int[] lastReads;
    int readCount;
    int maxReads;
    int id = 0;
    public GameObject warning;
    private int warningCount = 0;

    public Words(HandTracking manager): base(manager) {}

    void Start()
    {
        maxReads = 200;
        lastReads = new int[maxReads];
        for (int i = 0; i < maxReads; i++)
        {
            lastReads.SetValue(id, i);
        }
        // foreach (GameObject element in wordsAssets) {
        //     element.SetActive(true);
        // }
        
    }

    void OnEnable()
    {
        instantiatedWords = (GameObject) Instantiate(wordsPrefab, new Vector3(0.46f, 2.1f, 0.0f), transform.rotation, container.transform);
        instantiatedWords.SetActive(false);
        // instantiatedWords = (GameObject) Instantiate(wordsPrefab, transform.position, transform.rotation, transform);
        print("OnEnable");
        warning.SetActive(true);
        foreach (GameObject element in wordsAssets) {
            element.SetActive(true);
        }
    }

    void OnDisable()
    {
        // instantiatedWords.SetActive(false);
        // Destroy(instantiatedWords);
    }

    bool WrongMarkerValue(int value)
    {
        return value != id;
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
            Manager.SetFinished(id);
            Manager.ToPreviousEnd();
        }
    }

    private void executeWarning()
    {
        if(warningCount < 1500)
        {
            warningCount++;
        }
        else 
        {
            if(warning.activeInHierarchy)
            {
                warning.SetActive(false);
                instantiatedWords = (GameObject) Instantiate(wordsPrefab, new Vector3(0.46f, 2.1f, 0.0f), transform.rotation, container.transform);
                // slidersContainer.SetActive(true);
                // botao.SetActive(true);
            }
            warningCount = 0;
        }
    }

    public void Transition()
    {
        Destroy(instantiatedWords);
        Manager.SetFinished(id);
        warning.SetActive(true);
        Manager.ToPreviousEnd();
    }

    public override void Execute(string[] points)
    {
        // readMarker(int.Parse(points[127]));
        Manager.DrawHands(points);
        executeWarning();
    }
}

