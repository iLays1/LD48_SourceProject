using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class KillCounter : MonoBehaviour
{
    public UnityEvent OnWin = new UnityEvent();

    TextMeshProUGUI text;
    int kills = 0;
    LaneManager laneManager;
    bool gameOver = false;
    public int killsToWin = 25;
    public FallingEnemy winTarget;

    [Space]
    public GameObject swapperPrefab;
    public int killsPerSwapper = 8;

    public GameObject archerPrefab;
    public int killsPerArcher = 0;

    public GameObject tankPrefab;
    public int killsPerTank = 0;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        if (winTarget != null)
            winTarget.OnDeath.AddListener(Win);
        FallingEnemy.OnEnemyDeath.AddListener(OnKill);

        SetText();

        laneManager = FindObjectOfType<LaneManager>();    
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
            SpawnEnemyPrefab(swapperPrefab);
        }
        if (killsPerArcher > 0 && kills % killsPerArcher == 0)
        {
            SpawnEnemyPrefab(archerPrefab);
        }
        if (killsPerTank > 0 && kills % killsPerTank == 0)
        {
            SpawnEnemyPrefab(tankPrefab);
        }
    }

    void SpawnEnemyPrefab(GameObject prefab)
    {
        int[] arr = Enumerable.Range(0, laneManager.lanes.Length).ToArray();
        UtilityCode.Shuffle(arr);

        for (int i = 0; i < laneManager.lanes.Length; i++)
        {
            if (laneManager.lanes[arr[i]].occupant != null)
            {
                continue;
            }

            var go = Instantiate(prefab);
            var e = go.GetComponent<FallingEnemy>();
            e.laneIndex = arr[i];
            e.lastIndex = arr[i];
            e.transform.position = new Vector3(laneManager.lanes[arr[i]].transform.position.x, 10, 0);
            break;
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
