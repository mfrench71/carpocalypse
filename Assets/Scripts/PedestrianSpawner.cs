using UnityEngine;
using System.Collections.Generic;

namespace Carpocalypse
{
    public class PedestrianSpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
        public List<PedestrianData> pedestrianTypes = new List<PedestrianData>();
        public int maxPedestrians = 20;
        public float spawnInterval = 2f;
        public float spawnRadius = 30f;
        public float minDistanceFromPlayer = 10f;

        [Header("Armed Ratio")]
        [Range(0f, 1f)]
        public float armedChance = 0.2f;

        [Header("Runtime")]
        [SerializeField] private int currentCount = 0;

        private List<GameObject> activePedestrians = new List<GameObject>();
        private float nextSpawnTime;
        private Transform player;

        void Start()
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }

            nextSpawnTime = Time.time + 1f; // Delay first spawn
        }

        void Update()
        {
            // Clean up destroyed pedestrians
            activePedestrians.RemoveAll(p => p == null);
            currentCount = activePedestrians.Count;

            // Spawn new pedestrians
            if (Time.time >= nextSpawnTime && currentCount < maxPedestrians)
            {
                SpawnPedestrian();
                nextSpawnTime = Time.time + spawnInterval;
            }
        }

        void SpawnPedestrian()
        {
            if (pedestrianTypes.Count == 0) return;

            // Find spawn position
            Vector3 spawnPos = GetValidSpawnPosition();
            if (spawnPos == Vector3.zero) return;

            // Select pedestrian type
            PedestrianData selectedData = SelectPedestrianType();
            if (selectedData == null || selectedData.prefab == null) return;

            // Spawn
            GameObject pedestrian = Instantiate(selectedData.prefab, spawnPos, Quaternion.identity);
            pedestrian.transform.parent = transform;

            // Setup pedestrian
            PedestrianAI ai = pedestrian.GetComponent<PedestrianAI>();
            if (ai != null)
            {
                ai.pedestrianData = selectedData;
            }

            activePedestrians.Add(pedestrian);
            currentCount = activePedestrians.Count;

            GameEvents.TriggerPedestrianSpawned(pedestrian);
            GameEvents.TriggerPedestrianCountChanged(currentCount);
        }

        PedestrianData SelectPedestrianType()
        {
            // Separate armed and unarmed types
            List<PedestrianData> armed = new List<PedestrianData>();
            List<PedestrianData> unarmed = new List<PedestrianData>();

            foreach (var data in pedestrianTypes)
            {
                if (data.isArmed)
                    armed.Add(data);
                else
                    unarmed.Add(data);
            }

            // Decide if this one should be armed
            bool spawnArmed = Random.value < armedChance && armed.Count > 0;

            if (spawnArmed && armed.Count > 0)
            {
                return armed[Random.Range(0, armed.Count)];
            }
            else if (unarmed.Count > 0)
            {
                return unarmed[Random.Range(0, unarmed.Count)];
            }
            else if (pedestrianTypes.Count > 0)
            {
                return pedestrianTypes[Random.Range(0, pedestrianTypes.Count)];
            }

            return null;
        }

        Vector3 GetValidSpawnPosition()
        {
            int maxAttempts = 10;

            for (int i = 0; i < maxAttempts; i++)
            {
                // Random position in spawn radius
                Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
                Vector3 spawnPos = transform.position + new Vector3(randomPoint.x, 0, randomPoint.y);

                // Check distance from player
                if (player != null)
                {
                    float distToPlayer = Vector3.Distance(spawnPos, player.position);
                    if (distToPlayer < minDistanceFromPlayer)
                        continue;
                }

                // Raycast to find ground
                RaycastHit hit;
                if (Physics.Raycast(spawnPos + Vector3.up * 10f, Vector3.down, out hit, 20f))
                {
                    return hit.point + Vector3.up * 0.5f;
                }

                // If no ground, use flat spawn position
                return spawnPos;
            }

            return Vector3.zero;
        }

        public void ClearAllPedestrians()
        {
            foreach (var pedestrian in activePedestrians)
            {
                if (pedestrian != null)
                {
                    Destroy(pedestrian);
                }
            }
            activePedestrians.Clear();
            currentCount = 0;
            GameEvents.TriggerPedestrianCountChanged(0);
        }

        void OnDrawGizmosSelected()
        {
            // Draw spawn radius
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);

            // Draw min player distance
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, minDistanceFromPlayer);
        }
    }
}
