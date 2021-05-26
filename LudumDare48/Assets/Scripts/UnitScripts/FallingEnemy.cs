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

    [HideInInspector]
    public int actSpeed = 3;
    public int actTimer = 3;

    EnemyBehavior behavior;

    protected FallingPlayer player;

    [HideInInspector]
    public bool customDeath = false;

    protected override void Awake()
    {
        base.Awake();

        HpSlider.Create(this);
        timerText.GetComponent<MeshRenderer>().sortingLayerName = "UI";

        player = FindObjectOfType<FallingPlayer>();
        if (player == null) return;

        actSpeed = speed + player.speed;
        if (actSpeed < 1) actSpeed = 1;
        actTimer = actSpeed;

        timerText.text = actTimer.ToString();

        behavior = GetComponent<EnemyBehavior>();
        if (behavior == null) Debug.LogError($"{name} must have an Enemy Behavior Component with the FallingEnemy Component!");
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
            behavior.EnemyAct();
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
    
    public override void Death()
    {
        Destroy(behavior);

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
