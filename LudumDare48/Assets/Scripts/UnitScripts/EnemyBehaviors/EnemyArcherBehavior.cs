using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcherBehavior : EnemyBehavior
{
    public ArrowHazard arrowHazardPrefab;
    public UnitVisuals visuals;
    public Sprite preparingSprite;
    Sprite idleSprite;
    WaitForFixedUpdate waitForFUpdate = new WaitForFixedUpdate();

    protected void Start()
    {
        if (preparingSprite != null && visuals != null)
        {
            idleSprite = visuals.idleSprite;
            TickManager.OnTick.AddListener(OnTick);
        }
    }

    void OnTick()
    {
        StartCoroutine(TickCoroutine());
    }
    IEnumerator TickCoroutine()
    {
        yield return waitForFUpdate;

        if (enemy.actTimer == 1)
        {
            visuals.idleSprite = preparingSprite;
        }
        else
        {
            visuals.idleSprite = idleSprite;
        }
        visuals.ReturnSpriteToNeutral();
    }

    public override void EnemyAct() => StartCoroutine(EnemyActCoroutine());
    IEnumerator EnemyActCoroutine()
    {
        yield return WaitForTime;

        action.Do(enemy, 0);
    }
}
