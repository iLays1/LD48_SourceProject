using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAction : ScriptableObject
{
    public string displayName;
    public Sprite icon;

    [HideInInspector]
    public bool stationaryAction = false;
    public abstract bool Do(FallingUnit user, int dir);
}
