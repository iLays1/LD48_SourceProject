using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchFeedbackGraphic : MonoBehaviour
{
    private void Start()
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();

        Sequence s = DOTween.Sequence();
        Vector3 scale = transform.localScale;
        transform.localScale = Vector3.zero;

        s.Append(rend.DOFade(0f,0.5f));
        s.Join(transform.DOScale(scale, 0.3f));
        s.AppendCallback(() => Destroy(gameObject));
    }
}
