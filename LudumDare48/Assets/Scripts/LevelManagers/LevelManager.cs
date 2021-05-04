using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public UnityEvent OnObjectiveComplete = new UnityEvent();
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] int kills = 0;
    [SerializeField] int killsToWin = 25;

    [SerializeField] FallingEnemy bossPrefab;
    [SerializeField] int bossStartPos = 1;

    FallingEnemy winTarget;

    [Space]
    [SerializeField] EnemySpawningParameters[] spawnParameters;

    bool gameOver = false;

    private void Awake()
    {
        FallingEnemy.OnEnemyDeath.AddListener(OnKill);
    }

    private void Start()
    {
        if (killsToWin <= 0)
            SpawnBoss(bossStartPos);

        SetText();
    }

    void OnKill()
    {
        kills++;
        SetText();

        if (gameOver) return;

        foreach (var param in spawnParameters)
        {
            if (param.killsPerSpawn > 0 && kills % param.killsPerSpawn == 0)
            {
                ThreatSpawner.SpawnEnemyPrefab(param.enemyPrefab);
            }
        }

        if (kills >= killsToWin && winTarget == null)
        {
            kills = killsToWin;

            gameOver = true;

            if (bossPrefab != null)
            {
                SpawnBoss(bossStartPos);
            }
            else
            {
                Win();
            }

            SetText();
            return;
        }
    }

    public void SpawnBoss(int i)
    {
        var boss = Instantiate(bossPrefab);
        winTarget = boss;
        winTarget.OnDeath.AddListener(Win);

        var lanes = LaneManager.instance.lanes;
        bool spotFound = false;

        while (!spotFound)
        {
            if (lanes[i].occupant != null)
            {
                if (!(lanes[i].occupant is FallingPlayer))
                {
                    lanes[i].occupant.Death();
                    lanes[i].occupant = null;
                    spotFound = true;
                }
                else
                {
                    //is player
                    i++;
                }
            }
            else
            {
                spotFound = true;
            }
        }
        
        boss.laneIndex = i;
        boss.lastIndex = i;
        boss.transform.position = new Vector3(lanes[i].transform.position.x, 10, 0);
        boss.SetLane(i);

        SetText();
    }

    void Win()
    {
        winTarget.OnDeath.RemoveListener(Win);
        gameOver = true;
        OnObjectiveComplete.Invoke();
    }

    void SetText()
    {
        if(winTarget != null)
        {
            text.text = $"Kill the Boss!";
        }
        else
        {
            if (kills > killsToWin) kills = killsToWin;
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
