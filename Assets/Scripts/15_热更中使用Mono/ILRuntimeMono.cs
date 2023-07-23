using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILRuntimeMono : MonoBehaviour
{
    public event Action startEvent;
    public event Action updateEvent;
    // Start is called before the first frame update
    void Start()
    {
        startEvent?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        updateEvent?.Invoke();
    }

    public void AddStartEvent(Action newEvent)
    {
        startEvent += newEvent;
    }

    public void RemoveStartEvent(Action newEvent)
    {
        startEvent -= newEvent;
    }
}
