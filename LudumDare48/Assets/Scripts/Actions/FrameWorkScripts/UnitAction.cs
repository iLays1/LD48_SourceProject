using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAction : MonoBehaviour
{
    [HideInInspector]
    public bool stationaryAction = false;
    public abstract bool Do(FallingUnit user, int dir);
}
