using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public FallingUnit occupant;
    public SpriteRenderer laneSpriteRend;
    public Color safeColor;

    public List<LaneModifier> modifiers = new List<LaneModifier>();

    private void Awake()
    {
        ResetColor();
    }

    public void SetColor(Color color)
    {
        laneSpriteRend.color = color;
    }
    public void ResetColor()
    {
        laneSpriteRend.color = safeColor;
    }

    public void OnPlayerInteract(FallingPlayer player)
    {
        foreach (var mod in modifiers)
        {
            if (mod is LaneInteractable)
            {
                (mod as LaneInteractable).OnInteract();
            }
        }
    }
}
