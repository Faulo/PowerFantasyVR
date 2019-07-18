using PFVR.Player;
using PFVR.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneActivator : MonoBehaviour
{
    public Gesture gesture;
    public GameObject stone;

    // Start is called before the first frame update
    void Start()
    {
        GestureConnector.onGestureUnlock += ActivateStone;
    }

    private void ActivateStone(Gesture gesture)
    {
        if(this.gesture.Equals(gesture))
        {
            stone.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
