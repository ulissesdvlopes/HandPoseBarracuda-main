using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class LineCode : MonoBehaviour
{
 
    LineRenderer lineRenderer;
 
    public Transform origin;
    public Transform destination;
 
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
    }
 
    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, origin.transform.localPosition);
        lineRenderer.SetPosition(1, destination.transform.localPosition);
    }

    public Vector3[] GetPositions() {
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        return positions;
    }

    public float GetWidth() {
        return lineRenderer.startWidth;
    }

}
