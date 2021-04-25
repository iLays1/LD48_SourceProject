using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    TextMeshProUGUI text;
    int kills = 0;
    LaneManager laneManager;

    public GameObject swapperPrefab;
    public int killsPerSwapper = 8;

    public GameObject archerPrefab;
    public int killsPerArcher = 0;

    public GameObject tankPrefab;
    public int killsPerTank = 0;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        FallingEnemy.OnEnemyDeath.AddListener(OnKill);

        text.text = $"{kills}<size=30>kills";

        laneManager = FindObjectOfType<LaneManager>();    
    }

    void OnKill()
    {
        kills++;
        text.text = $"{kills}<size=30>kills";

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
}
