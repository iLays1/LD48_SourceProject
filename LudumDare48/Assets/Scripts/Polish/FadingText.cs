using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadingText : MonoBehaviour
{
    public static FadingText Create(Vector3 position, Color color, Transform parent, string text, float duration = 2.4f)
    {
        var go = (GameObject)Instantiate(Resources.Load("FadingText"));
        go.transform.SetParent(parent);

        FadingText t = go.GetComponent<FadingText>();
        t.Initalize(text, color, duration, position);

        return t;
    }

    public TextMeshPro textMesh;
    Sequence s;
    Color color;
    float duration;
    
    public void Initalize(string text, Color color, float duration, Vector3 position)
    {
        this.color = color;
        this.duration = duration;

        transform.position = position;
        GetComponent<MeshRenderer>().sortingLayerName = "UI";

        textMesh.text = text;
        textMesh.color = color;

        s = DOTween.Sequence();

        transform.localScale = Vector3.zero;

        s.Append(transform.DOMoveY(transform.position.y + 1.5f, duration / 8));
        s.Join(transform.DOScale(Vector3.one, duration / 6));
        s.Append(transform.DOScale(Vector3.zero, duration * 0.3f));
        s.Join(transform.DOMoveY(transform.position.y - 0.3f, duration));
        s.AppendCallback(() => Destroy(gameObject));
    }

    public void StartAgain(string text, Vector3 pos)
    {
        s.Kill();
        Initalize(text, color, duration, pos);
    }
}
