using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneModifier : MonoBehaviour
{
    public Lane targetLane;
    public Color modifierColor;

    public virtual void Initalize()
    {
        transform.position = targetLane.transform.position + (Vector3.down * 2.5f);
        targetLane.SetColor(modifierColor);
        targetLane.modifiers.Add(this);
    }
    public virtual void RemoveModifier()
    {
        targetLane.modifiers.Remove(this);
        targetLane.ResetColor();
        Destroy(gameObject);
    }
}
