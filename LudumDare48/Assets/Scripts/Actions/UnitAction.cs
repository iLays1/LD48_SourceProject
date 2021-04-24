using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAction : MonoBehaviour
{
    protected LaneManager laneManager;

    private void Awake()
    {
        laneManager = FindObjectOfType<LaneManager>();
    }

    public abstract void Do(FallingUnit user, int dir);
}
