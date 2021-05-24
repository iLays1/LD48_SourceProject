using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public FallingUnit occupant;
    public SpriteRenderer laneSpriteRend;
    public Color safeColor;
    
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

    }
}
