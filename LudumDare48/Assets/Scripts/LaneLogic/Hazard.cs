using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hazard : MonoBehaviour
{
    public Lane targetLane;
    public Color hazardColor;

    public void Initalize()
    {
        transform.position = targetLane.transform.position + (Vector3.down * 2.5f);
        TickManager.ActivateHazards.AddListener(Activate);
        LevelEndHandler.OnLevelWin.AddListener(DestroyHazard);
        targetLane.laneSpriteRend.color = hazardColor;
    }

    protected abstract void Activate();

    public void DestroyHazard()
    {
        targetLane.laneSpriteRend.color = targetLane.safeColor;
        Destroy(gameObject);
    }
}
