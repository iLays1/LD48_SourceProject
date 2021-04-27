using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitAction : MonoBehaviour
{
    public abstract void Do(FallingUnit user, int dir);
}
