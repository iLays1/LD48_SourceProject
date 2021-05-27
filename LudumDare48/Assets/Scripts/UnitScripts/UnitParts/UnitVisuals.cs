using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UnitVisuals : MonoBehaviour
{
    public SpriteRenderer rend;
    public Sprite idleSprite;
    public Sprite attackingSprite;
    //public TrailRenderer trail;
    Color baseColor;
    Animator anim;

    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem hitParticle;

    private void Awake()
    {
        if (rend == null) rend = GetComponentInChildren<SpriteRenderer>();

        rend.sprite = idleSprite;
        anim = rend.GetComponent<Animator>();
        baseColor = rend.color;
    }
    
    public void FlipLeft()
    {
        rend.flipX = true;
    }
    public void FlipRight()
    {
        rend.flipX = false;
    }

    public void AttackAnimation()
    {
        StopCoroutine(AttackAnimCoroutine());
        StartCoroutine(AttackAnimCoroutine());
    }
    IEnumerator AttackAnimCoroutine()
    {
        rend.sprite = attackingSprite;
        yield return new WaitForSeconds(0.15f);
        rend.sprite = idleSprite;
    }

    public void DamagedAnimation()
    {
        StartCoroutine(DamagedAnimCoroutine());
    }
    IEnumerator DamagedAnimCoroutine()
    {
        hitParticle.Play();

        rend.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        rend.color = baseColor;
    }
    public void DeathAnimation()
    {
        StartCoroutine(DeathAnimCoroutine());
    }
    IEnumerator DeathAnimCoroutine()
    {
        Destroy(anim);
        deathParticle.Play();
        rend.DOFade(0f, 1.5f);
        rend.transform.DOMoveY(10f, 3f);
        rend.transform.DORotate(new Vector3(0,0,360), 2f, RotateMode.FastBeyond360);
        yield return new WaitForSeconds(3f);
        rend.DOKill();
        Destroy(gameObject);
    }
}
