using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public Lane targetLane;
    public int damage = 10;
    public void Initalize()
    {
        transform.position = targetLane.transform.position + (Vector3.down * 2.5f);
        TickManager.OnOutOfMoves.AddListener(Activate);
        LevelEndHandler.OnLevelWin.AddListener(DestroyHazard);
        targetLane.laneSpriteRend.color = targetLane.hazardColor;
    }

    void Activate()
    {
        StartCoroutine(HazardCoroutine());
    }
    IEnumerator HazardCoroutine()
    {
        yield return new WaitForSeconds(0.002f);

        Sequence s = DOTween.Sequence();

        s.Append(transform.DOMoveY(20, 0.4f));
        s.AppendCallback(() => Destroy(gameObject));

        if (targetLane.occupant != null)
            targetLane.occupant.TakeDamage(damage);

        targetLane.laneSpriteRend.color = targetLane.safeColor;
    }

    public void DestroyHazard()
    {
        targetLane.laneSpriteRend.color = targetLane.safeColor;
        Destroy(gameObject);
    }
}
