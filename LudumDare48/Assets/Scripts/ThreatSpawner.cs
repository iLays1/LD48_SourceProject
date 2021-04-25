using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThreatSpawner : MonoBehaviour
{
    public Hazard hazardPrefab;
    public FallingEnemy enemyPrefab;
    LaneManager laneManager;

    public int hazardsPerOOMs;
    public int enemiesPerOOMs;

    private void Awake()
    {
        laneManager = FindObjectOfType<LaneManager>();
        TickManager.OnOutOfMoves.AddListener(() => SpawnHazards(hazardsPerOOMs));
        TickManager.OnOutOfMoves.AddListener(() => SpawnEnemies(enemiesPerOOMs));
        SpawnHazards(3);
    }

    public void SpawnHazards(int count)
    {
        int[] arr = Enumerable.Range(0, laneManager.lanes.Length).ToArray();
        UtilityCode.Shuffle(arr);

        for (int i = 0; i < count; i++)
        {
            var haz = Instantiate(hazardPrefab);
            haz.targetLane = laneManager.lanes[arr[i]];
            haz.Initalize();
        }
    }
    public void SpawnEnemies(int count)
    {
        int[] arr = Enumerable.Range(0, laneManager.lanes.Length).ToArray();
        UtilityCode.Shuffle(arr);
        
        for (int i = 0; i < count; i++)
        {
            if (laneManager.lanes[arr[i]].occupant != null)
            {
                continue;
            }

            var e = Instantiate(enemyPrefab);
            e.laneIndex = arr[i];
            e.lastIndex = arr[i];
            e.transform.position = new Vector3(laneManager.lanes[arr[i]].transform.position.x, 10, 0);
        }
    }
}
