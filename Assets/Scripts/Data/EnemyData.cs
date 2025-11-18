using UnityEngine;

namespace Carpocalypse
{
    public enum EnemyType
    {
        Chaser,
        Shooter,
        Tank,
        Bomber
    }

    [CreateAssetMenu(fileName = "NewEnemy", menuName = "Carpocalypse/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        [Header("Info")]
        public string enemyName;
        public EnemyType enemyType;
        public GameObject prefab;

        [Header("Stats")]
        public int maxHealth = 30;
        public float moveSpeed = 5f;
        public float rotationSpeed = 100f;

        [Header("Combat")]
        public int contactDamage = 10;
        public float damageRate = 1f;
        public float stoppingDistance = 5f;

        [Header("Ranged (if applicable)")]
        public bool canShoot = false;
        public float fireRate = 1f;
        public int bulletDamage = 5;
        public float bulletSpeed = 15f;
        public float attackRange = 10f;

        [Header("Score")]
        public int scoreValue = 100;

        [Header("Drops")]
        [Range(0f, 1f)]
        public float healthDropChance = 0.1f;
        [Range(0f, 1f)]
        public float ammoDropChance = 0.2f;
        [Range(0f, 1f)]
        public float weaponDropChance = 0.05f;
    }
}
