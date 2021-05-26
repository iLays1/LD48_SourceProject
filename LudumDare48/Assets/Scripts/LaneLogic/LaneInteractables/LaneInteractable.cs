using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneInteractable : LaneModifier
{
    public string debugString;

    private void Start()
    {
        Initalize();
    }

    public virtual void OnInteract()
    {
        Debug.Log(debugString);
    }
}
