using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealHazard : Hazard
{
    public int amount = 10;
    WaitForSeconds waitForTime = new WaitForSeconds(0.002f);

    protected override void Activate()
    {
        StartCoroutine(HazardCoroutine());
    }
    IEnumerator HazardCoroutine()
    {
        yield return waitForTime;

        Sequence s = DOTween.Sequence();

        s.Append(transform.DOMoveY(20, 0.4f));
        s.AppendCallback(() => Destroy(gameObject));

        if (targetLane.occupant != null)
        {
            targetLane.occupant.Heal(amount);
        }

        RemoveModifier();
    }
}
