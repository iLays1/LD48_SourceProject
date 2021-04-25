using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEnemySwapper : FallingEnemy
{
    public override void EnemyAct() => StartCoroutine(EnemyActCoroutine());
    IEnumerator EnemyActCoroutine()
    {
        yield return new WaitForSeconds(0.42f);
        attackAction.Do(this, 0);
    }
}
