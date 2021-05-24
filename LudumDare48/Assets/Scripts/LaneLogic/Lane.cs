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
        laneSpriteRend.color = safeColor;
    }
}
