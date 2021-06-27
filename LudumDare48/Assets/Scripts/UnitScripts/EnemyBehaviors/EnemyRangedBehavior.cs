using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedBehavior : EnemyBehavior
{
    public UnitVisuals visuals;
    public Sprite preparingSprite;
    Sprite idleSprite;

    protected void Start()
    {
        if(preparingSprite != null && visuals != null)
        {
            idleSprite = visuals.idleSprite;
            TickManager.OnTick.AddListener(OnTick);
        }
    }

    void OnTick()
    {
        if(enemy.actTimer == 2)
        {
            visuals.idleSprite = preparingSprite;
            visuals.ReturnSpriteToNeutral();
        }
        else
        {
            visuals.idleSprite = idleSprite;
            visuals.ReturnSpriteToNeutral();
        }
    }

    public override void EnemyAct() => StartCoroutine(EnemyActCoroutine());
    IEnumerator EnemyActCoroutine()
    {
        yield return WaitForTime;
        action.Do(enemy, 0);
    }
}
