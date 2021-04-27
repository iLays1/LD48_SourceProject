using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSongSelector : MonoBehaviour
{
    public MusicManager.Song song;

    private void Start()
    {
        MusicManager.main.PlaySong(song);
    }
}
