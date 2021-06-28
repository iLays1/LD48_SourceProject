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

    public void UpdateColor()
    {
        ResetColor();
        if(modifiers.Count > 0)
        {
            foreach (var mod in modifiers)
            {
                SetColor(mod.modifierColor);
            }
        }
    }

    void SetColor(Color color)
    {
        laneSpriteRend.color = color;
    }
    void ResetColor()
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
