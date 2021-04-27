using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndHandler : MonoBehaviour
{
    public KillCounter kCounter;
    FallingPlayer player;

    private void Awake()
    {
        kCounter = FindObjectOfType<KillCounter>();
        kCounter.OnWin.AddListener(PlayerWin);
        player = FindObjectOfType<FallingPlayer>();
        player.OnDeath.AddListener(PlayerLose);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PlayerLose();
    }

    void PlayerWin()
    {
        StartCoroutine(WinCoroutine());
    }
    IEnumerator WinCoroutine()
    {
        Destroy(player);
        MusicManager.main.FadeOut();
        LevelUnlockManager.UnlockLevel(SceneManager.GetActiveScene().buildIndex+ 1);
        FindObjectOfType<Fader>().FadeOut(3f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }

    void PlayerLose()
    {
        StartCoroutine(LoseCoroutine());
    }
    IEnumerator LoseCoroutine()
    {
        MusicManager.main.FadeOut();
        FindObjectOfType<Fader>().FadeOut(3f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
