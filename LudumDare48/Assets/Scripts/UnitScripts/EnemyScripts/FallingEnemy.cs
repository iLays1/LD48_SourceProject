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

    [HideInInspector]
    public int actSpeed = 3;
    protected int actTimer = 3;

    protected FallingPlayer player;

    [HideInInspector]
    public bool customDeath = false;

    protected override void Awake()
    {
        base.Awake();

        timerText.GetComponent<MeshRenderer>().sortingLayerName = "UI";

        player = FindObjectOfType<FallingPlayer>();
        if (player == null) return;

        actSpeed = speed + player.speed;
        if (actSpeed < 1) actSpeed = 1;
        actTimer = actSpeed;

        timerText.text = actTimer.ToString();
    }

    protected override void Start()
    {
        base.Start();

        TickManager.OnTick.AddListener(Tick);
        actTimer = actSpeed;
        FacePlayer();
    }

    void Tick()
    {
        actTimer--;
        
        if (actTimer <= 0)
        {
            FacePlayer();
            actTimer = actSpeed;
            EnemyAct();
        }

        timerText.text = actTimer.ToString();
    }

    public void FacePlayer()
    {
        if (player == null) return;

        var dir = GetDirFrom(player);

        if (dir == -1)
            FlipLeft();
        if (dir == 1)
            FlipRight();
    }

    public virtual void EnemyAct() => StartCoroutine(EnemyActCoroutine());
    IEnumerator EnemyActCoroutine()
    {
        yield return new WaitForSeconds(0.03f); 
        
        if (facingDir == -1)
        {
            if (visuals != null)
                visuals.FlipLeft();
            if (lanes[laneIndex - 1].occupant != null && lanes[laneIndex - 1].occupant is FallingPlayer)
            {
                attackAction.Do(this, - 1);
            }
            else
            {
                MoveLeft();
            }
        }
        if (facingDir == 1)
        {
            if (visuals != null)
                visuals.FlipRight();

            if (lanes[laneIndex + 1].occupant != null && lanes[laneIndex + 1].occupant is FallingPlayer)
            {
                attackAction.Do(this, + 1);
            }
            else
            {
                MoveRight();
            }
        }
    }

    public override void Death()
    {
        StopAllCoroutines();
        OnEnemyDeath.Invoke();
        Destroy(timerText.gameObject);

        if (customDeath)
        {
            transform.DOComplete();
            OnDeath.Invoke();
            return;
        }

        base.Death();
    }
}
