using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UnitVisuals : MonoBehaviour
{
    public SpriteRenderer rend;
    public Sprite idleSprite;
    public Sprite actingSprite;
    public float actTime = 0.24f;
    //public TrailRenderer trail;
    Color baseColor;
    Animator anim;

    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem hitParticle;

    WaitForSeconds waitForActTime;
    WaitForSeconds waitForDamagedAnim = new WaitForSeconds(0.2f);

    private void Awake()
    {
        if (rend == null) rend = GetComponentInChildren<SpriteRenderer>();

        waitForActTime = new WaitForSeconds(actTime);

        ReturnSpriteToNeutral();
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

    public void ActAnimation()
    {
        StopCoroutine(ActAnimCoroutine());
        StartCoroutine(ActAnimCoroutine());
    }
    IEnumerator ActAnimCoroutine()
    {
        rend.sprite = actingSprite;
        yield return waitForActTime;
        ReturnSpriteToNeutral();
    }

    public void MoveAnim()
    {
        rend.transform.DOComplete();
        rend.transform.DOPunchPosition(Vector3.up * 0.35f, 0.18f).SetEase(Ease.Flash);
    }

    public void DamagedAnimation()
    {
        StartCoroutine(DamagedAnimCoroutine());
    }
    IEnumerator DamagedAnimCoroutine()
    {
        hitParticle.Play();

        rend.color = Color.red;
        yield return waitForDamagedAnim;
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
        rend.DOFade(0f, 0.4f);
        rend.transform.DORotate(new Vector3(0, 0, 360), 2f, RotateMode.FastBeyond360);
        yield return new WaitForSecondsRealtime(3f);
        rend.DOKill();
        Destroy(gameObject);
    }

    public void ReturnSpriteToNeutral()
    {
        rend.sprite = idleSprite;
    }
}
