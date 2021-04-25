using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class FallingEnemy : FallingUnit
{
    public static UnityEvent OnEnemyDeath = new UnityEvent();

    public TextMeshPro timerText;
    public UnitAction attackAction;
    public int actSpeed = 3;
    int actTimer = 3;
    FallingPlayer player;

    protected override void Awake()
    {
        base.Awake();
        actTimer = actSpeed;
        timerText.GetComponent<MeshRenderer>().sortingLayerName = "UI";
        player = FindObjectOfType<FallingPlayer>();
        TickManager.OnTick.AddListener(Tick);
    }

    void Tick()
    {
        actTimer--;

        if(actTimer <= 0)
        {
            actTimer = actSpeed;
            EnemyAct();
        }

        timerText.text = actTimer.ToString();
    }

    public virtual void EnemyAct() => StartCoroutine(EnemyActCoroutine());
    IEnumerator EnemyActCoroutine()
    {
        yield return new WaitForSeconds(0.04f); 

        var r = (laneIndex < player.laneIndex) ? 1 : 0;
        
        if (r == 0)
        {
            if (visuals != null)
                visuals.FlipLeft();
            if (lanes[laneIndex - 1].occupant != null && lanes[laneIndex - 1].occupant is FallingPlayer)
            {
                attackAction.Do(this, -1);
            }
            else
            {
                MoveLeft();
            }
        }
        if (r == 1)
        {
            if (visuals != null)
                visuals.FlipRight();

            if (lanes[laneIndex + 1].occupant != null && lanes[laneIndex + 1].occupant is FallingPlayer)
            {
                attackAction.Do(this, +1);
            }
            else
            {
                MoveRight();
            }
        }
    }

    protected override void Death()
    {
        OnEnemyDeath.Invoke();
        Destroy(timerText.gameObject);
        base.Death();
    }
}
