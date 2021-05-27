using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargerBehavior : EnemyBehavior
{
    public int chargeTimes;
    public int chargeSpeed;

    bool charging = false;
    int oActspeed;
    int charges = 0;
    
    protected void Start()
    {
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

        yield return new WaitForSeconds(0.05f);

        BasicAct();
    }

    void BeginCharge()
    {
        enemy.actSpeed = chargeSpeed;
        enemy.actTimer = enemy.actSpeed;

        charging = true;

        enemy.timerText.text = enemy.actTimer.ToString();
    }
    void EndCharge()
    {
        enemy.actSpeed = oActspeed;
        enemy.actTimer = enemy.actSpeed;

        charges = 0;
        charging = false;

        enemy.timerText.text = enemy.actTimer.ToString();
    }
}