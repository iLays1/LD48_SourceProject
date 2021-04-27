using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverSound : MonoBehaviour
{
    public AudioSource source;

    private void OnMouseEnter()
    {
        source.Play();
    }
}
