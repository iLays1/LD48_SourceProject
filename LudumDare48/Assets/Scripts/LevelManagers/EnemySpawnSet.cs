using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemySpawnSet : ScriptableObject
{
    [System.Serializable]
    public class EnemySpawningParameter
    {
        public FallingEnemy enemyPrefab;
        public int killsPerSpawn = 8;
    }

    public EnemySpawningParameter[] spawnParameters;
}
