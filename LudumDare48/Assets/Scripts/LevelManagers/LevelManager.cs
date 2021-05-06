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

    public UnityEvent OnObjectiveComplete = new UnityEvent();
    [SerializeField] TextMeshProUGUI killText;

    [Space]
    [SerializeField] int kills = 0;
    [SerializeField] int killsToWin = 25;

    [Space]
    [SerializeField] FallingEnemy bossPrefab;
    [SerializeField] int bossStartPos = 1;

    FallingEnemy winTarget;

    [Space]
    [SerializeField] EnemySpawnSet enemySet;
    [SerializeField] ActionsOnKills[] actions;

    ThreatSpawner threatSpawner;
    bool gameOver = false;

    FallingPlayer player;

    [Space]
    [SerializeField] Gradient textGradient;

    private void Awake()
    {
        player = FindObjectOfType<FallingPlayer>();
        threatSpawner = GetComponent<ThreatSpawner>();
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

            SetText();
            return;
        }
    }
    IEnumerator SpawnEnemyPrefabDelayedCoroutine(EnemySpawnSet.EnemySpawningParameter param)
    {
        yield return new WaitForSeconds(0.02f);
        ThreatSpawner.SpawnEnemyPrefab(param.enemyPrefab);
    }

    public void SpawnBossSequence()
    {
        StartCoroutine(SpawnBossSequenceCoroutine());
    }
    IEnumerator SpawnBossSequenceCoroutine()
    {
        player.isActive = false;
        yield return new WaitForSeconds(2f);

        SpawnBoss(bossStartPos);

        yield return new WaitForSeconds(0.3f);
        player.isActive = true;

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
            killText.color = Color.white;
            killText.text = $"Defeat the Boss";
        }
        else
        {
            if (kills > killsToWin) kills = killsToWin;
            killText.text = $"{killsToWin - kills}<size=20>\n Kills left";

            killText.color = textGradient.Evaluate((float)kills/killsToWin);
        }
    }

    public void ChangeSpawnParams(EnemySpawnSet enemySet)
    {
        this.enemySet = enemySet;
    }
}
