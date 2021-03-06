using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargerBehavior : EnemyBehavior
{
    public int chargeTimes;
    public int chargeSpeed;
    public UnitVisuals visuals;
    public Sprite chargingSprite;
    Sprite idleSprite;
    bool charging = false;
    int oActspeed;
    int charges = 0;
    
    protected void Start()
    {
        idleSprite = visuals.idleSprite;
        oActspeed = enemy.actSpeed;

        EndCharge();
    }

    public override void EnemyAct() => StartCoroutine(EnemyActCoroutine());
    IEnumerator EnemyActCoroutine()
    {
        if (charging)
        {
            charges++;

            if (charges >= chargeTimes)
            {
                EndCharge();
            }
        }
        else
        {
            BeginCharge();
        }

        yield return WaitForTime;

        BasicAct();
    }

    void BeginCharge()
    {
        enemy.actSpeed = chargeSpeed;
        enemy.actTimer = enemy.actSpeed;

        charging = true;

        visuals.idleSprite = chargingSprite;
        visuals.ReturnSpriteToNeutral();

        enemy.timerText.text = enemy.actTimer.ToString();
    }
    void EndCharge()
    {
        enemy.actSpeed = oActspeed;
        enemy.actTimer = enemy.actSpeed;

        visuals.idleSprite = idleSprite;
        visuals.ReturnSpriteToNeutral();

        charges = 0;
        charging = false;

        enemy.timerText.text = enemy.actTimer.ToString();
    }
}