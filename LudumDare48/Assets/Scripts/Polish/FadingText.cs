using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadingText : MonoBehaviour
{
    public static FadingText Create(Vector3 position, Color color, Transform parent, string text)
    {
        var go = (GameObject)Instantiate(Resources.Load("FadingText"));
        go.transform.SetParent(parent);

        FadingText t = go.GetComponent<FadingText>();
        t.Initalize(text, color, position);

        return t;
    }

    public TextMeshPro textMesh;
    Sequence s;
    Color color;
    
    public void Initalize(string text, Color color, Vector3 position)
    {
        this.color = color;

        transform.position = position;
        GetComponent<MeshRenderer>().sortingLayerName = "UI";

        textMesh.text = text;
        textMesh.color = color;

        s = DOTween.Sequence();

        transform.localScale = Vector3.one;
        transform.position += Vector3.up * 0.1f;

        s.Append(transform.DOPunchPosition(Vector3.up * 0.1f, 1.2f));
        s.Append(textMesh.DOFade(0f, 0.2f));

        s.AppendCallback(() => Destroy(gameObject));
    }

    public void StartAgain(string text, Vector3 pos)
    {
        s.Kill();
        Initalize(text, color, pos);
    }
}
