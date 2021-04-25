using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public Lane targetLane;
    LaneManager lm;

    public void Initalize()
    {
        lm = FindObjectOfType<LaneManager>();
        targetLane = lm.lanes[Random.Range(0,lm.lanes.Length)];
        transform.position = targetLane.transform.position + (Vector3.down * 5f);
        TickManager.OnOutOfMoves.AddListener(Activate);
    }

    void Activate()
    {
        Sequence s = DOTween.Sequence();

        s.Append(transform.DOMoveY(10, 0.3f));
        s.AppendCallback(() => Destroy(gameObject));

        if (targetLane.occupant != null)
            targetLane.occupant.TakeDamage(10);
    }
}
