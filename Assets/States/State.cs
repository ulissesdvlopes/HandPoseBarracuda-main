using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public HandTracking Manager;

    public State(HandTracking manager)
    {
        Manager = manager;
    }

    public virtual void Execute(string[] points)
    {
    }
}
