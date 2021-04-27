using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingUnit : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnDamaged = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnDeath = new UnityEvent();

    public int hp;
    public int attackPower;

    public int laneIndex;
    public int lastIndex;

    public AudioSource hitSound;
    public AudioSource deathSound;
    public AudioSource attackSound;
    public AudioSource moveSound;

    protected LaneManager laneManager;
    protected Lane[] lanes { get { return laneManager.lanes; } }

    public UnitVisuals visuals;

    ScreenShaker screenShaker;

    protected virtual void Awake()
    {
        HpSlider.Create(this);
        screenShaker = FindObjectOfType<ScreenShaker>();
        laneManager = FindObjectOfType<LaneManager>();
    }

    private void Start()
    {
        lastIndex = laneIndex;
        SetLane(laneIndex);
        transform.position = new Vector3(laneManager.lanes[laneIndex].transform.position.x, 10, 0);
    }

    public virtual void TakeDamage(int damage)
    {
        if (visuals != null)
            visuals.DamagedAnimation();

        hp -= damage;

        if(hp <= 0)
        {
            deathSound.Play();
            screenShaker.Shake(0.2f, 0.1f);
            hp = 0;
            Death();
            return;
        }

        hitSound.Play();
        FadingText.Create(transform.position, transform, damage.ToString());
        screenShaker.Shake(0.1f, 0.05f);
        OnDamaged.Invoke();
    }

    protected virtual void Death()
    {
        OnDeath.Invoke();
        visuals.DeathAnimation();
        transform.DOComplete();
        Destroy(this);
    }

    public void MoveLeft()
    {
        if (visuals != null)
            visuals.FlipLeft();

        moveSound.Play();
        SetLane(laneIndex - 1);
    }
    public void MoveRight()
    {
        if (visuals != null)
            visuals.FlipRight();

        moveSound.Play();
        SetLane(laneIndex + 1);
    }

    public virtual void SetLane(int index)
    {
        if (index < 0) index = 0;
        if (index > lanes.Length - 1) index = lanes.Length - 1;

        if (lanes[index].occupant == null)
        {
            lanes[laneIndex].occupant = null;
            lanes[index].occupant = this;

            laneIndex = index;

            transform.DOKill();
            transform.DOMove(laneManager.lanes[index].transform.position, 0.2f);
        }
        else
        {
            if(index > laneIndex || index == lanes.Length - 1)
            {
                transform.DOPunchPosition(Vector3.right * 0.5f, 0.1f);
            }
            if(index < laneIndex || index == 0)
            {
                transform.DOPunchPosition(Vector3.left * 0.5f, 0.1f);
            }
        }
    }
}
