using UnityEngine;

namespace Carpocalypse
{
    public enum PedestrianBehavior
    {
        Wander,      // Walks around randomly
        Flee,        // Runs away from player
        Aggressive,  // Attacks player on sight
        Neutral      // Ignores player unless attacked
    }

    [CreateAssetMenu(fileName = "NewPedestrian", menuName = "Carpocalypse/Pedestrian Data")]
    public class PedestrianData : ScriptableObject
    {
        [Header("Info")]
        public string pedestrianName;
        public GameObject prefab;

        [Header("Stats")]
        public int maxHealth = 10;
        public float walkSpeed = 2f;
        public float runSpeed = 5f;

        [Header("Behavior")]
        public PedestrianBehavior defaultBehavior = PedestrianBehavior.Wander;
        public float detectionRange = 10f;
        public float fleeDistance = 15f;
        public float wanderRadius = 10f;
        public float wanderInterval = 3f; // Time between picking new wander points

        [Header("Combat")]
        public bool isArmed = false;
        public int damage = 5;
        public float attackRange = 8f;
        public float fireRate = 1f;
        public float bulletSpeed = 12f;

        [Header("Score")]
        public int scoreValue = 50;
        public int penaltyValue = 0; // Negative score for killing innocent pedestrians

        [Header("Drops")]
        [Range(0f, 1f)]
        public float healthDropChance = 0.05f;
        [Range(0f, 1f)]
        public float ammoDropChance = 0.1f;

        [Header("Visual")]
        public Color color = Color.white;
    }
}
