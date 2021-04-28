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

    private void Awake()
    {
        TickManager.OnOutOfMoves.AddListener(() => SpawnHazards(hazardsPerOOMs));
        TickManager.OnOutOfMoves.AddListener(() => SpawnEnemies(enemiesPerOOMs));
        SpawnHazards(hazardsPerOOMs);
        SpawnEnemies(enemiesPerOOMs);
    }

    public void SpawnHazards(int count)
    {
        StartCoroutine(SpawnHazardsCoroutine(count));
    }
    IEnumerator SpawnHazardsCoroutine(int count)
    {
        yield return new WaitForSeconds(0.02f);//

        int[] arr = Enumerable.Range(0, LaneManager.instance.lanes.Length).ToArray();
        UtilityCode.Shuffle(arr);

        for (int i = 0; i < count; i++)
        {
            var haz = Instantiate(hazardPrefab);
            haz.targetLane = LaneManager.instance.lanes[arr[i]];
            haz.Initalize();
        }
    }

    public void SpawnEnemies(int count)
    {
        StartCoroutine(SpawnEnemiesCoroutine(count));
    }
    IEnumerator SpawnEnemiesCoroutine(int count)
    {
        yield return new WaitForSeconds(0.02f);

        int[] arr = Enumerable.Range(0, LaneManager.instance.lanes.Length).ToArray();
        UtilityCode.Shuffle(arr);

        for (int i = 0; i < count; i++)
        {
            SpawnEnemyPrefab(enemyPrefab);
        }
    }

    public static void SpawnEnemyPrefab(FallingEnemy prefab)
    {
        int[] arr = Enumerable.Range(0, LaneManager.instance.lanes.Length).ToArray();
        UtilityCode.Shuffle(arr);

        for (int i = 0; i < LaneManager.instance.lanes.Length; i++)
        {
            if (LaneManager.instance.lanes[arr[i]].occupant != null)
            {
                continue;
            }

            var e = Instantiate(prefab);
            e.laneIndex = arr[i];
            e.lastIndex = arr[i];
            e.transform.position = new Vector3(LaneManager.instance.lanes[arr[i]].transform.position.x, 10, 0);
            e.SetLane(arr[i]);
            break;
        }
    }
}
