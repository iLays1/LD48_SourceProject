using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEnemyCharger : FallingEnemy
{
    public int chargeTimes;
    public int chargeSpeed;

    bool charging = false;
    int oActspeed;
    int charges = 0;

    protected override void Awake()
    {
        oActspeed = actSpeed;
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
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

        yield return new WaitForSeconds(0.03f);

        base.EnemyAct();
    }

    void BeginCharge()
    {
        actSpeed = chargeSpeed;
        actTimer = actSpeed;

        charging = true;

        timerText.text = actTimer.ToString();
    }
    void EndCharge()
    {
        actSpeed = oActspeed;
        actTimer = actSpeed;

        charges = 0;
        charging = false;

        timerText.text = actTimer.ToString();
    }
}