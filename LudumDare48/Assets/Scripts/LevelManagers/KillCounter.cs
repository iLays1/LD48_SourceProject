using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class KillCounter : MonoBehaviour
{
    public UnityEvent OnWin = new UnityEvent();
    public TextMeshProUGUI text;

    int kills = 0;
    bool gameOver = false;
    public int killsToWin = 25;
    public FallingEnemy winTarget;

    [Space]
    public FallingEnemy swapperPrefab;
    public int killsPerSwapper = 8;

    public FallingEnemy archerPrefab;
    public int killsPerArcher = 0;

    public FallingEnemy tankPrefab;
    public int killsPerTank = 0;

    private void Awake()
    {
        if (winTarget != null)
            winTarget.OnDeath.AddListener(Win);
        FallingEnemy.OnEnemyDeath.AddListener(OnKill);

        SetText();
    }

    void Win()
    {
        OnWin.Invoke();
        gameOver = true;
    }

    void OnKill()
    {
        kills++;
        SetText();

        if (gameOver) return;

        if(kills >= killsToWin && winTarget == null)
        {
            Win();
            kills = killsToWin;
            SetText();
            return;
        }

        if (killsPerSwapper > 0 && kills % killsPerSwapper == 0)
        {
            ThreatSpawner.SpawnEnemyPrefab(swapperPrefab);
        }
        if (killsPerArcher > 0 && kills % killsPerArcher == 0)
        {
            ThreatSpawner.SpawnEnemyPrefab(archerPrefab);
        }
        if (killsPerTank > 0 && kills % killsPerTank == 0)
        {
            ThreatSpawner.SpawnEnemyPrefab(tankPrefab);
        }
    }

    void SetText()
    {
        if(winTarget != null)
        {
            text.text = $"Kill the Boss!";
        }
        else
        {
            text.text = $"{killsToWin - kills}<size=30>\n kills to win";
        }
    }
}
