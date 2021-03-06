using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHazard : Hazard
{
    public int damage = 10;
    WaitForSeconds waitForTime = new WaitForSeconds(0.0001f);

    public override void Initalize()
    {
        base.Initalize();
    }

    protected override void Activate()
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
