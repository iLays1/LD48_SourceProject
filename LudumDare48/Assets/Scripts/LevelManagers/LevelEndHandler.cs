using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelEndHandler : MonoBehaviour
{
    public static UnityEvent OnLevelWin = new UnityEvent();
    public static UnityEvent OnLevelLose = new UnityEvent();

    public KillCounter kCounter;
    FallingPlayer player;

    private void Awake()
    {
        kCounter = FindObjectOfType<KillCounter>();
        kCounter.OnObjectiveComplete.AddListener(PlayerWin);
        player = FindObjectOfType<FallingPlayer>();
        player.OnDeath.AddListener(PlayerLose);
    }
    
    void PlayerWin()
    {
        StartCoroutine(WinCoroutine());
    }
    IEnumerator WinCoroutine()
    {
        OnLevelWin.Invoke();
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
        OnLevelLose.Invoke();
        Debug.Log("LOSE");
        yield return new WaitForSeconds(3f);
        //SceneManager.LoadScene(0);
    }
}
