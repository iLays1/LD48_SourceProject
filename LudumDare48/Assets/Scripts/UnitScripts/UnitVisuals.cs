using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UnitVisuals : MonoBehaviour
{
    public SpriteRenderer render;
    public Sprite idleSprite;
    public Sprite attackingSprite;
    //public TrailRenderer trail;
    Color baseColor;
    private void Awake()
    {
        render.sprite = idleSprite;
        baseColor = render.color;
    }
    
    public void FlipLeft()
    {
        render.flipX = true;
    }
    public void FlipRight()
    {
        render.flipX = false;
    }

    public void AttackAnimation()
    {
        StartCoroutine(AttackAnimCoroutine());
    }
    IEnumerator AttackAnimCoroutine()
    {
        render.sprite = attackingSprite;
        yield return new WaitForSeconds(0.3f);
        render.sprite = idleSprite;
    }

    public void DamagedAnimation()
    {
        StartCoroutine(DamagedAnimCoroutine());
    }
    IEnumerator DamagedAnimCoroutine()
    {
        render.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        render.color = baseColor;
    }
    public void DeathAnimation()
    {
        StartCoroutine(DeathAnimCoroutine());
    }
    IEnumerator DeathAnimCoroutine()
    {
        render.DOFade(0f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        render.DOKill();
        Destroy(gameObject);
    }
}
