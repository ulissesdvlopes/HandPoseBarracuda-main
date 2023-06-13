using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : MonoBehaviour
{
    public GameObject[] toDeactivate;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in toDeactivate) {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
