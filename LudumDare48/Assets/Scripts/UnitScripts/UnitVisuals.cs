using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UnitVisuals : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Sprite idleSprite;
    public Sprite attackingSprite;
    //public TrailRenderer trail;
    Color baseColor;
    Animator anim;

    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem hitParticle;

    private void Awake()
    {
        renderer.sprite = idleSprite;
        anim = renderer.GetComponent<Animator>();
        baseColor = renderer.color;
    }
    
    public void FlipLeft()
    {
        renderer.flipX = true;
    }
    public void FlipRight()
    {
        renderer.flipX = false;
    }

    public void AttackAnimation()
    {
        StartCoroutine(AttackAnimCoroutine());
    }
    IEnumerator AttackAnimCoroutine()
    {
        renderer.sprite = attackingSprite;
        yield return new WaitForSeconds(0.3f);
        renderer.sprite = idleSprite;
    }

    public void DamagedAnimation()
    {
        StartCoroutine(DamagedAnimCoroutine());
    }
    IEnumerator DamagedAnimCoroutine()
    {
        hitParticle.Play();

        renderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        renderer.color = baseColor;
    }
    public void DeathAnimation()
    {
        StartCoroutine(DeathAnimCoroutine());
    }
    IEnumerator DeathAnimCoroutine()
    {
        Destroy(anim);
        deathParticle.Play();
        renderer.DOFade(0f, 1.5f);
        renderer.transform.DOMoveY(10f, 3f);
        renderer.transform.DORotate(new Vector3(0,0,360), 2f, RotateMode.FastBeyond360);
        yield return new WaitForSeconds(3f);
        renderer.DOKill();
        Destroy(gameObject);
    }
}
