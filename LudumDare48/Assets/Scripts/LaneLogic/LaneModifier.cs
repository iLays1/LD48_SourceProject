using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneModifier : MonoBehaviour
{
    public Lane targetLane;
    public Color modifierColor;
    public SpriteRenderer laneModIcon;

    public virtual void Initalize()
    {
        transform.position = targetLane.transform.position + (Vector3.up * 5f);
        targetLane.modifiers.Add(this);
        targetLane.UpdateColor();
    }
    public virtual void RemoveModifier()
    {
        laneModIcon.color = Color.clear;
        targetLane.modifiers.Remove(this);
        targetLane.UpdateColor();
    }
}
