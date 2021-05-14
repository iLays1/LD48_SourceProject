using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThreatSpawner : MonoBehaviour
{
    public Hazard hazardPrefab;
    public FallingEnemy enemyPrefab;

    public int hazardsPerOOMs;
    public int enemiesPerOOMs;

    public int maxPopulation = 4;
    int population = 0;

    bool spawning = true;

    private void Awake()
    {
        TickManager.ActivateHazards.AddListener(() => SpawnHazards(hazardsPerOOMs));
        TickManager.ActivateHazards.AddListener(() => SpawnEnemies(enemiesPerOOMs));

        SpawnHazards(hazardsPerOOMs);
        SpawnEnemies(enemiesPerOOMs);

        LevelEndHandler.OnLevelWin.AddListener(() => spawning = false);
    }

    public void ChangeHazardsPerOOMs(int value) => hazardsPerOOMs = value;
    public void ChangeEnemiesPerOOMs(int value) => enemiesPerOOMs = value;
    public void ChangeBaseEnemy(FallingEnemy newEnemyPrefab) => enemyPrefab = newEnemyPrefab;
    
    public void SpawnHazards(int count)
    {
        if (!spawning) return;
        StartCoroutine(SpawnHazardsCoroutine(count));
    }
    IEnumerator SpawnHazardsCoroutine(int count)
    {
        yield return new WaitForSeconds(0.02f);

        if (spawning)
        {
            int[] arr = Enumerable.Range(0, LaneManager.instance.lanes.Length).ToArray();
            UtilityCode.Shuffle(arr);

            for (int i = 0; i < count; i++)
            {
                var haz = Instantiate(hazardPrefab);
                haz.targetLane = LaneManager.instance.lanes[arr[i]];
                haz.Initalize();
            }
        }
    }

    public void SpawnEnemies(int count)
    {
        if (!spawning) return;
        StartCoroutine(SpawnEnemiesCoroutine(count));
    }
    IEnumerator SpawnEnemiesCoroutine(int count)
    {
        yield return new WaitForSeconds(0.02f);

        if (spawning)
        {
            int[] arr = Enumerable.Range(0, LaneManager.instance.lanes.Length).ToArray();
            UtilityCode.Shuffle(arr);

            for (int i = 0; i < count; i++)
            {
                if(population < maxPopulation)
                {
                    population++;
                    var e = SpawnEnemyPrefab(enemyPrefab);
                    e.OnDeath.AddListener(() => population--);
                }
            }
        }
    }

    //this is for buttons and UnityEvents
    public void SpawnSpecificEnemy(FallingEnemy prefab)
    {
        SpawnEnemyPrefab(prefab);
    }
    public static FallingEnemy SpawnEnemyPrefab(FallingEnemy prefab)
    {
        int[] arr = Enumerable.Range(0, LaneManager.instance.lanes.Length).ToArray();
        UtilityCode.Shuffle(arr);

        for (int i = 0; i < LaneManager.instance.lanes.Length; i++)
        {
            if (LaneManager.instance.lanes[arr[i]].occupant != null)
            {
                continue;
            }

            FallingEnemy e = Instantiate(prefab);
            e.laneIndex = arr[i];
            e.lastIndex = arr[i];
            e.transform.position = new Vector3(LaneManager.instance.lanes[arr[i]].transform.position.x, 10, 0);
            e.SetLane(arr[i]);
            e.FacePlayer();
            return e;
        }
        return null;
    }
}
