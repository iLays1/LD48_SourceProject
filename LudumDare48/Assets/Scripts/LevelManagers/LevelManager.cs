using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class ActionsOnKills
    {
        [HideInInspector]
        public bool active = true;
        public int killsTillAction;
        public UnityEvent action;
    }

    public bool isActive = true;

    public UnityEvent OnObjectiveComplete = new UnityEvent();
    public UnityEvent OnUpdateUI = new UnityEvent();

    [Space]
    public int kills = 0;
    public int killsToWin = 25;
    [SerializeField] EnemySpawnSet enemySet;

    [HideInInspector] public FallingEnemy winTarget;

    [Space]
    [SerializeField] FallingEnemy bossPrefab;
    [SerializeField] int bossStartPos = 1;
    
    [Space]
    [SerializeField] ActionsOnKills[] actions;

    ThreatSpawner threatSpawner;
    bool gameOver = false;
    FallingPlayer player;
    BossSpawnSequencer bossSpawner;
    WaitForSeconds waitToSpawn = new WaitForSeconds(0.02f);

    private void Awake()
    {
        player = FindObjectOfType<FallingPlayer>();
        threatSpawner = GetComponent<ThreatSpawner>();
        bossSpawner = GetComponent<BossSpawnSequencer>();
        FallingEnemy.OnEnemyDeath.AddListener(OnKill);
    }

    private void Start()
    {
        if (killsToWin <= 0)
            SpawnBoss(bossStartPos);

        UpdateUI();
    }

    void OnKill()
    {
        if (!isActive) return;

        kills++;
        UpdateUI();

        if (gameOver) return;

        foreach (var param in enemySet.spawnParameters)
        {
            if (param.killsPerSpawn > 0 && kills % param.killsPerSpawn == 0)
            {
                StartCoroutine(SpawnEnemyPrefabDelayedCoroutine(param));
            }
        }

        foreach (var param in actions)
        {
            if (param.killsTillAction <= kills && param.active)
            {
                param.active = false;
                param.action.Invoke();
            }
        }

        if (kills >= killsToWin && winTarget == null)
        {
            kills = killsToWin;

            gameOver = true;

            if (bossPrefab != null)
            {
                SpawnBossSequence();
            }
            else
            {
                Win();
            }

            UpdateUI();
            return;
        }
    }
    IEnumerator SpawnEnemyPrefabDelayedCoroutine(EnemySpawnSet.EnemySpawningParameter param)
    {
        yield return waitToSpawn;
        ThreatSpawner.SpawnEnemyPrefab(param.enemyPrefab);
    }

    public void SpawnBossSequence()
    {
        bossSpawner.SpawnBossSequence(bossStartPos);
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
                    i--;
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

        //boss.customDeath = true;

        UpdateUI();
    }

    public void UpdateUI()
    {
        OnUpdateUI.Invoke();
    }

    void Win()
    {
        if(winTarget != null)
            winTarget.OnDeath.RemoveListener(Win);

        gameOver = true;
        OnObjectiveComplete.Invoke();
    }
    
    public void ChangeSpawnParams(EnemySpawnSet enemySet)
    {
        this.enemySet = enemySet;
    }
}
