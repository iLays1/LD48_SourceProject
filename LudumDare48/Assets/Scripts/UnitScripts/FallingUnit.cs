using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingUnit : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnHpChange = new UnityEvent();
    [HideInInspector] public UnityEvent OnDeath = new UnityEvent();

    protected Lane[] lanes { get { return LaneManager.instance.lanes; } }
    public Lane myLane { get { return LaneManager.instance.lanes[laneIndex]; } }
    protected ScreenShaker screenShaker;

    [Header("Stats")]
    public int maxHp;
    public int attackPower;
    public int defense;
    public int speed;

    [HideInInspector] public int hp;

    [Space]
    public int laneIndex;
    [HideInInspector] public int lastIndex;

    public UnitAudio unitAudio;
    public UnitVisuals visuals;

    [HideInInspector] public int facingDir = 1;
    FadingText dmgText;
    int dmg;
    
    protected virtual void Awake()
    {
        hp = maxHp;
        screenShaker = FindObjectOfType<ScreenShaker>();
    }

    protected virtual void Start()
    {
        lastIndex = laneIndex;
        SetLane(laneIndex);
        transform.position = new Vector3(myLane.transform.position.x, 10, 0);
    }

    public virtual void Heal(int amount)
    {
        hp += amount;
        FadingText.Create(transform.position + Vector3.up, Color.green, transform, amount.ToString());

        if (hp > maxHp)
            hp = maxHp;

        //unitAudio?.healSound.Play();
        OnHpChange.Invoke();
    }

    public virtual void TakeAttackDamage(int damage)
    {
        damage -= defense;
        if (damage < 0) damage = 0;

        TakeDamage(damage);
    }

    public virtual void TakeDamage(int damage)
    {
        if (visuals != null)
            visuals.DamagedAnimation();

        hp -= damage;
        DamageText(damage);

        if (hp <= 0)
        {
            unitAudio?.deathSound.Play();
            screenShaker.Shake(0.2f, 0.2f);
            hp = 0;

            Death();
            return;
        }

        unitAudio?.hitSound.Play();
        screenShaker.Shake(0.10f, 0.08f);
        OnHpChange.Invoke();
    }

    void DamageText(int damage)
    {
        if (dmgText == null)
        {
            dmg = damage;
            dmgText = FadingText.Create(transform.position + Vector3.up, Color.red, transform, dmg.ToString());
            //StartCoroutine(dmgTextCoroutine());
        }
        else
        {
            dmg += damage;
            dmgText.StartAgain(dmg.ToString(), transform.position + Vector3.up);
        }
    }

    IEnumerator dmgTextCoroutine()
    {
        yield return new WaitForSeconds(1f);
        dmgText = null;
    }

    public virtual void Death()
    {
        visuals.DeathAnimation();
        transform.DOComplete();
        OnDeath.Invoke();
        Destroy(this);
    }

    public virtual void MoveLeft()
    {
        if (visuals != null)
            FlipLeft();
        
        SetLane(laneIndex - 1, true);
    }
    public virtual void MoveRight()
    {
        if (visuals != null)
            FlipRight();

        SetLane(laneIndex + 1, true);
    }

    protected void FlipLeft()
    {
        facingDir = -1;
        visuals.FlipLeft();
    }
    protected void FlipRight()
    {
        facingDir = 1;
        visuals.FlipRight();
    }

    public int GetDirFrom(FallingUnit unit)
    {
        //1 means the target is to the Right, -1 is Left
        return unit.laneIndex > laneIndex ? 1 : -1;
    }
    
    public virtual void SetLane(int index, bool tick = false)
    {
        if (index < 0) index = 0;
        if (index > lanes.Length - 1) index = lanes.Length - 1;

        if (lanes[index].occupant == null)
        {
            myLane.occupant = null;
            lanes[index].occupant = this;

            laneIndex = index;

            unitAudio?.moveSound.Play();

            transform.DOKill();
            transform.DOMove(myLane.transform.position, 0.2f);
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
