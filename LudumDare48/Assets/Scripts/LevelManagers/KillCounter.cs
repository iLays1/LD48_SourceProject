using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class KillCounter : MonoBehaviour
{
    public UnityEvent OnObjectiveComplete = new UnityEvent();
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] int kills = 0;
    bool gameOver = false;
    [SerializeField] int killsToWin = 25;
    [SerializeField] FallingEnemy winTarget;

    [Space]
    [SerializeField] EnemySpawningParameters[] spawnParameters;
    
    private void Awake()
    {
        if (winTarget != null)
            winTarget.OnDeath.AddListener(Win);
        FallingEnemy.OnEnemyDeath.AddListener(OnKill);

        SetText();
    }

    void Win()
    {
        OnObjectiveComplete.Invoke();
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

        foreach(var param in spawnParameters)
        {
            if (param.killsPerSpawn > 0 && kills % param.killsPerSpawn == 0)
            {
                ThreatSpawner.SpawnEnemyPrefab(param.enemyPrefab);
            }
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

    [System.Serializable]
    public class EnemySpawningParameters
    {
        public FallingEnemy enemyPrefab;
        public int killsPerSpawn = 8;
    }
}
