using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadingText : MonoBehaviour
{
    public static FadingText Create(Vector3 position, string text, float duration = 2.4f)
    {
        var go = (GameObject)Instantiate(Resources.Load("FadingText"));
        
        go.transform.position = position;
        FadingText t = go.GetComponent<FadingText>();
        t.Initalize(text, Color.white, duration);

        return t;
    }

    public TextMeshPro textMesh;

    public void Initalize(string text, Color color, float duration)
    {
        GetComponent<MeshRenderer>().sortingLayerName = "UI";

        textMesh.text = text;
        textMesh.color = color;

        Sequence s = DOTween.Sequence();

        var oScale = transform.localScale;

        transform.localScale = Vector3.zero;

        s.Append(transform.DOMoveY(transform.position.y + 2f, duration / 8));
        s.Join(transform.DOScale(oScale, duration / 6));
        s.Append(transform.DOMoveY(transform.position.y - 0.3f, duration));
        s.Join(transform.DOScale(Vector3.zero, duration * 0.6f));
        s.AppendCallback(() => Destroy(gameObject));
    }
}
