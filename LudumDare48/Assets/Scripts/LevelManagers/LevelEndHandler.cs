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
    
    void PlayerWin()
    {
        StartCoroutine(WinCoroutine());
    }
    IEnumerator WinCoroutine()
    {
        Destroy(player);
        Debug.Log("WIN");
        yield return new WaitForSeconds(3f);
        //SceneManager.LoadScene(0);
    }

    void PlayerLose()
    {
        StartCoroutine(LoseCoroutine());
    }
    IEnumerator LoseCoroutine()
    {
        Debug.Log("LOSE");
        yield return new WaitForSeconds(3f);
        //SceneManager.LoadScene(0);
    }
}
