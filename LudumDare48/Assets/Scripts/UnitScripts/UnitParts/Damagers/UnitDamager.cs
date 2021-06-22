using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDamager : MonoBehaviour
{
    public FallingUnit u;

    protected ScreenShaker screenShaker;
    FadingText dmgText;
    int dmg;
    WaitForSeconds waitForDamageTextClear = new WaitForSeconds(0.3f);

    public virtual void Awake()
    {
        u = GetComponent<FallingUnit>();
        screenShaker = FindObjectOfType<ScreenShaker>();
    }

    public virtual void TakeAttackDamage(int damage, int dir)
    {
        damage -= u.defense;
        if (damage < 0) damage = 0;

        TakeDamage(damage);
    }

    public virtual void TakeDamage(int damage)
    {
        if (u.visuals != null)
            u.visuals.DamagedAnimation();

        u.hp -= damage;
        DamageText(damage);

        if (u.hp <= 0)
        {
            u.unitAudio?.deathSound.Play();
            screenShaker.Shake(0.2f, 0.2f);
            u.hp = 0;

            u.Death();
            return;
        }

        u.unitAudio?.hitSound.Play();
        screenShaker.Shake(0.10f, 0.08f);
        u.OnHpChange.Invoke();
    }

    void DamageText(int damage)
    {
        if (dmgText == null)
        {
            dmg = damage;
            dmgText = FadingText.Create(transform.position + Vector3.up, Color.red, transform, dmg.ToString());
            StartCoroutine(DamageTextCoroutine());
        }
        else
        {
            dmg += damage;
            dmgText.StartAgain(dmg.ToString(), transform.position + Vector3.up);
        }
    }

    IEnumerator DamageTextCoroutine()
    {
        yield return waitForDamageTextClear;
        dmgText = null;
    }
}
