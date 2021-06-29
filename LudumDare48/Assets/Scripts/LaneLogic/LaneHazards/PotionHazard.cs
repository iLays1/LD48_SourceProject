using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHazard : LaneModifier
{
    public int heal = 10;
    WaitForSeconds waitForTime = new WaitForSeconds(0.0001f);
    int ticks = 2;

    private void Start()
    {
        TickManager.OnTick.AddListener(Activate);
    }

    public override void Initalize()
    {
        transform.position = targetLane.transform.position + (Vector3.up * 1f);
        targetLane.modifiers.Add(this);
        targetLane.UpdateColor();
    }

    void Activate()
    {
        ticks--;
        if (ticks > 0) return;

        Sequence s = DOTween.Sequence();

        s.Append(transform.DOMoveY(-20, 0.3f));
        s.AppendCallback(() => Destroy(gameObject));

        if (targetLane.occupant != null)
        {
            targetLane.occupant.Heal(heal);
        }

        RemoveModifier();
    }
}
