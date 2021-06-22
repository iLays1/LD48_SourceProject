using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelEndHandler : MonoBehaviour
{
    public static UnityEvent OnLevelWin = new UnityEvent();
    public static UnityEvent OnLevelLose = new UnityEvent();

    public LevelManager levelManager;
    FallingPlayer player;

    WaitForSeconds waitForReset = new WaitForSeconds(4f);

    private void Awake()
    {
        levelManager.OnObjectiveComplete.AddListener(PlayerWin);
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
        foreach(var e in FindObjectsOfType<FallingEnemy>())
        {
            e.Death();
        }
        Destroy(player);

        Debug.Log("WIN");

        yield return waitForReset;
        SceneManager.LoadScene(0);
    }

    void PlayerLose()
    {
        StartCoroutine(LoseCoroutine());
    }
    IEnumerator LoseCoroutine()
    {
        OnLevelLose.Invoke();

        Debug.Log("LOSE");

        yield return waitForReset;
        SceneManager.LoadScene(0);
    }
}
