using UnityEngine;

namespace Carpocalypse
{
    [RequireComponent(typeof(Health))]
    public class PedestrianAI : MonoBehaviour
    {
        [Header("Data")]
        public PedestrianData pedestrianData;

        [Header("Runtime State")]
        [SerializeField] private PedestrianBehavior currentBehavior;
        [SerializeField] private bool isAggro = false;

        private Transform player;
        private Rigidbody rb;
        private Health health;

        private Vector3 wanderTarget;
        private float nextWanderTime;
        private float nextFireTime;
        private Vector3 spawnPosition;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            health = GetComponent<Health>();
            spawnPosition = transform.position;

            if (rb == null)
            {
                Debug.LogError("PedestrianAI requires a Rigidbody!");
                return;
            }

            // Find player
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }

            // Initialize from data
            if (pedestrianData != null)
            {
                currentBehavior = pedestrianData.defaultBehavior;
                health.SetMaxHealth(pedestrianData.maxHealth);
                health.scoreValue = pedestrianData.scoreValue;

                // Set visual
                Renderer rend = GetComponent<Renderer>();
                if (rend != null && rend.material != null)
                {
                    rend.material.color = pedestrianData.color;
                }
            }

            // Set initial wander target
            SetNewWanderTarget();
        }

        void FixedUpdate()
        {
            if (rb == null || pedestrianData == null) return;

            float distanceToPlayer = player != null ? Vector3.Distance(transform.position, player.position) : float.MaxValue;

            // Check if player is in detection range
            bool playerDetected = distanceToPlayer <= pedestrianData.detectionRange;

            // Update behavior based on state
            UpdateBehavior(playerDetected, distanceToPlayer);

            // Execute current behavior
            switch (currentBehavior)
            {
                case PedestrianBehavior.Wander:
                    Wander();
                    break;
                case PedestrianBehavior.Flee:
                    Flee();
                    break;
                case PedestrianBehavior.Aggressive:
                    Attack(distanceToPlayer);
                    break;
                case PedestrianBehavior.Neutral:
                    Wander();
                    break;
            }
        }

        void UpdateBehavior(bool playerDetected, float distanceToPlayer)
        {
            // If attacked, become aggressive or flee
            if (isAggro)
            {
                if (pedestrianData.isArmed)
                {
                    currentBehavior = PedestrianBehavior.Aggressive;
                }
                else
                {
                    currentBehavior = PedestrianBehavior.Flee;
                }
                return;
            }

            // Default behavior based on data
            if (!playerDetected)
            {
                // Return to default behavior when player is out of range
                if (currentBehavior == PedestrianBehavior.Flee && distanceToPlayer > pedestrianData.fleeDistance)
                {
                    currentBehavior = pedestrianData.defaultBehavior;
                }
                return;
            }

            // Player detected - react based on default behavior
            switch (pedestrianData.defaultBehavior)
            {
                case PedestrianBehavior.Flee:
                    currentBehavior = PedestrianBehavior.Flee;
                    break;
                case PedestrianBehavior.Aggressive:
                    currentBehavior = PedestrianBehavior.Aggressive;
                    break;
                // Wander and Neutral don't change behavior on detection
            }
        }

        void Wander()
        {
            // Check if we need a new wander target
            if (Time.time >= nextWanderTime || Vector3.Distance(transform.position, wanderTarget) < 1f)
            {
                SetNewWanderTarget();
            }

            // Move towards wander target
            MoveTowards(wanderTarget, pedestrianData.walkSpeed);
        }

        void Flee()
        {
            if (player == null) return;

            // Run away from player
            Vector3 fleeDirection = (transform.position - player.position).normalized;
            Vector3 fleeTarget = transform.position + fleeDirection * 5f;

            MoveTowards(fleeTarget, pedestrianData.runSpeed);
        }

        void Attack(float distanceToPlayer)
        {
            if (player == null || !pedestrianData.isArmed) return;

            // Move towards player if too far
            if (distanceToPlayer > pedestrianData.attackRange * 0.8f)
            {
                MoveTowards(player.position, pedestrianData.walkSpeed);
            }
            else
            {
                // Stop and shoot
                rb.linearVelocity = Vector3.zero;
            }

            // Face player
            Vector3 lookDir = (player.position - transform.position).normalized;
            lookDir.y = 0;
            if (lookDir != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDir);
            }

            // Shoot if in range
            if (distanceToPlayer <= pedestrianData.attackRange && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + pedestrianData.fireRate;
            }
        }

        void MoveTowards(Vector3 target, float speed)
        {
            Vector3 direction = (target - transform.position).normalized;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                // Move
                rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

                // Rotate to face movement direction
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.fixedDeltaTime));
            }
        }

        void SetNewWanderTarget()
        {
            // Pick random point within wander radius of spawn position
            Vector2 randomPoint = Random.insideUnitCircle * pedestrianData.wanderRadius;
            wanderTarget = spawnPosition + new Vector3(randomPoint.x, 0, randomPoint.y);
            nextWanderTime = Time.time + pedestrianData.wanderInterval;
        }

        void Shoot()
        {
            if (ObjectPool.Instance == null) return;

            Vector3 firePos = transform.position + transform.forward * 0.5f + Vector3.up * 0.5f;
            GameObject bullet = ObjectPool.Instance.SpawnFromPool("Bullet", firePos, transform.rotation);

            if (bullet != null)
            {
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.damage = pedestrianData.damage;
                    bulletScript.speed = pedestrianData.bulletSpeed;
                    bulletScript.lifetime = 2f;
                    bulletScript.isPlayerBullet = false; // This is an enemy bullet
                }

                // Make bullet red for enemy bullets
                Renderer rend = bullet.GetComponent<Renderer>();
                if (rend != null && rend.material != null)
                {
                    rend.material.color = Color.red;
                }
            }
        }

        // Called when pedestrian takes damage
        public void OnDamaged()
        {
            isAggro = true;
        }

        // Public method to trigger aggro
        public void SetAggro(bool aggro)
        {
            isAggro = aggro;
        }

        void OnCollisionEnter(Collision collision)
        {
            // Check if hit by player vehicle
            if (collision.gameObject.CompareTag("Player"))
            {
                // Take ram damage
                health.TakeDamage(50);
            }
        }
    }
}
