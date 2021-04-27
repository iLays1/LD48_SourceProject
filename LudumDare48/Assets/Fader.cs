using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    public Image fadeImage;

    private void Start()
    {
        FadeIn(2f);
    }

    public void FadeIn(float dur)
    {
        fadeImage.DOFade(0f, dur);
    }
    public void FadeOut(float dur)
    {
        fadeImage.DOFade(1f, dur);
    }
}
