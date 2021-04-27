using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverExpand : MonoBehaviour
{
    public float growFactor;
    public float growSpeed;
    Vector3 oScale;

    private void Awake()
    {
        oScale = transform.localScale;
    }

    private void OnMouseEnter()
    {
        transform.DOScale(oScale * growFactor, growSpeed);
    }
    private void OnMouseExit()
    {
        transform.DOScale(oScale, growSpeed);
    }
}
