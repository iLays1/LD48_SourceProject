using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEnemySwapper : FallingEnemy
{
    FallingPlayer p;

    protected override void Awake()
    {
        base.Awake();
        p = FindObjectOfType<FallingPlayer>();
    }

    private void Update()
    {
        var dir = p.laneIndex > laneIndex ? 1 : -1;

        if (dir == -1)
            visuals.FlipLeft();
        if (dir == 1)
            visuals.FlipRight();
    }

    public override void EnemyAct() => StartCoroutine(EnemyActCoroutine());
    IEnumerator EnemyActCoroutine()
    {
        yield return new WaitForSeconds(0.04f);
        attackAction.Do(this, 0);
    }
}
