using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedBehavior : EnemyBehavior
{
    public override void EnemyAct() => StartCoroutine(EnemyActCoroutine());
    IEnumerator EnemyActCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.045f);
        action.Do(enemy, 0);
    }
}
