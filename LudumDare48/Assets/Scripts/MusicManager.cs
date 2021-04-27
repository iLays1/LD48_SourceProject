using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager main;

    public AudioClip song1;
    public AudioClip song2;
    public AudioClip song3;
    public AudioClip song4;
    public AudioClip songMenu;

    public enum Song
    {
        song1,
        song2,
        song3,
        song4,
        menu
    }

    AudioSource source;
    float baseVol;

    public void Awake()
    {
        if (main != null && main != this)
        {
            Destroy(gameObject);
            return;
        }

        source = GetComponent<AudioSource>();
        baseVol = source.volume;

        main = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySong(Song song)
    {
        switch (song)
        {
            case Song.song1:
                source.clip = song1;
                break;
            case Song.song2:
                source.clip = song2;
                break;
            case Song.song3:
                source.clip = song3;
                break;
            case Song.song4:
                source.clip = song4;
                break;
            case Song.menu:
                source.clip = songMenu;
                break;
        }

        FadeIn();
        if (!source.isPlaying)
            source.Play();
    }

    public void FadeOut()
    {
        source.DOFade(0f, 3f);
    }
    public void FadeIn()
    {
        source.DOFade(baseVol, 2f);
    }
}
