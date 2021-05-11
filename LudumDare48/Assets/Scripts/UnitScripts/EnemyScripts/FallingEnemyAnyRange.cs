using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEnemyAnyRange : FallingEnemy
{
    FallingPlayer p;

    protected override void Awake()
    {
        base.Awake();
        p = FindObjectOfType<FallingPlayer>();
    }
    
    public override void EnemyAct() => StartCoroutine(EnemyActCoroutine());
    IEnumerator EnemyActCoroutine()
    {
        yield return new WaitForSeconds(0.04f);
        attackAction.Do(this, 0);
    }
}
