using UnityEngine;
using System.Collections.Generic;

namespace Carpocalypse
{
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public EnemyData enemyData;
        public int count;
        public float spawnDelay = 0.5f;
    }

    [CreateAssetMenu(fileName = "NewWave", menuName = "Carpocalypse/Wave Data")]
    public class WaveData : ScriptableObject
    {
        [Header("Wave Info")]
        public string waveName;
        public int waveNumber;

        [Header("Enemies")]
        public List<EnemySpawnInfo> enemies = new List<EnemySpawnInfo>();

        [Header("Timing")]
        public float timeBetweenSpawns = 1f;
        public float delayBeforeNextWave = 3f;

        [Header("Bonuses")]
        public int completionBonus = 500;

        public int TotalEnemyCount
        {
            get
            {
                int total = 0;
                foreach (var spawn in enemies)
                {
                    total += spawn.count;
                }
                return total;
            }
        }
    }
}
