using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHazard : LaneModifier
{
    public int damage = 10;
    WaitForSeconds waitForTime = new WaitForSeconds(0.0001f);

    private void Start()
    {
        TickManager.OnTick.AddListener(Activate);
    }

    public override void Initalize()
    {
        transform.position = targetLane.transform.position + (Vector3.up * 3f);
        targetLane.modifiers.Add(this);
        targetLane.UpdateColor();
    }

    void Activate()
    {
        Sequence s = DOTween.Sequence();

        s.Append(transform.DOMoveY(-20, 0.3f));
        s.AppendCallback(() => Destroy(gameObject));

        if (targetLane.occupant != null)
        {
            targetLane.occupant.damager.TakeDamage(damage);
        }

        RemoveModifier();
    }
}
