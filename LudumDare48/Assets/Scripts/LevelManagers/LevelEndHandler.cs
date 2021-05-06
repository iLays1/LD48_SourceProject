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
        FadingText.Create(Vector3.zero, Color.yellow, null, "WIN", 6f);

        yield return new WaitForSeconds(6f);
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
        FadingText.Create(Vector3.zero, Color.grey, null, "Failure", 6f);

        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(0);
    }
}
