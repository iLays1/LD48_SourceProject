using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitModifier : MonoBehaviour
{
    public abstract void Apply(FallingUnit unit);
    public abstract void Remove(FallingUnit unit);
}
