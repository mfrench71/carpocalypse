using UnityEngine;
using System.Collections.Generic;

namespace Carpocalypse
{
    public class WeaponSystem : MonoBehaviour
    {
        [Header("Weapon Settings")]
        public List<WeaponData> availableWeapons = new List<WeaponData>();
        public Transform firePoint;

        [Header("Runtime State")]
        [SerializeField] private int currentWeaponIndex = 0;

        // Runtime ammo tracking (doesn't modify ScriptableObject)
        private Dictionary<WeaponData, int> ammoInventory = new Dictionary<WeaponData, int>();
        private float nextFireTime = 0f;

        public WeaponData CurrentWeapon => availableWeapons.Count > 0 ? availableWeapons[currentWeaponIndex] : null;

        void Start()
        {
            // Create fire point if it doesn't exist
            if (firePoint == null)
            {
                GameObject fp = new GameObject("FirePoint");
                fp.transform.parent = transform;
                fp.transform.localPosition = new Vector3(0f, 0.5f, 1f); // Raised Y to 0.5
                firePoint = fp.transform;
            }

            // Initialize ammo inventory from weapon data
            foreach (var weapon in availableWeapons)
            {
                if (weapon != null)
                {
                    ammoInventory[weapon] = weapon.maxAmmo;
                }
            }
        }

        void Update()
        {
            if (CurrentWeapon == null)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.LogWarning("WeaponSystem: No weapon equipped! Add weapons to availableWeapons list.");
                }
                return;
            }

            // Check for game over state
            if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            {
                return; // Don't shoot during game over
            }

            // Shooting
            if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
            {
                if (CanShoot())
                {
                    Shoot();
                    nextFireTime = Time.time + CurrentWeapon.fireRate;

                    if (!CurrentWeapon.hasInfiniteAmmo)
                    {
                        ammoInventory[CurrentWeapon]--;
                    }
                }
                else
                {
                    Debug.Log("WeaponSystem: Cannot shoot - CanShoot() returned false. Ammo: " + GetCurrentAmmo());
                }
            }

            // Weapon switching (1, 2, 3 keys)
            for (int i = 0; i < availableWeapons.Count && i < 9; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    SwitchWeapon(i);
                }
            }
        }

        private bool CanShoot()
        {
            if (CurrentWeapon == null) return false;
            if (CurrentWeapon.hasInfiniteAmmo) return true;
            return ammoInventory.ContainsKey(CurrentWeapon) && ammoInventory[CurrentWeapon] > 0;
        }

        public int GetCurrentAmmo()
        {
            if (CurrentWeapon == null) return 0;
            if (CurrentWeapon.hasInfiniteAmmo) return -1; // -1 indicates infinite
            return ammoInventory.ContainsKey(CurrentWeapon) ? ammoInventory[CurrentWeapon] : 0;
        }

        private void Shoot()
        {
            if (CurrentWeapon == null || firePoint == null)
            {
                Debug.LogError("WeaponSystem.Shoot: CurrentWeapon or firePoint is null!");
                return;
            }
            if (ObjectPool.Instance == null)
            {
                Debug.LogError("WeaponSystem.Shoot: ObjectPool.Instance is null!");
                return;
            }

            Debug.Log("WeaponSystem: Shooting " + CurrentWeapon.weaponName);

            // Shoot multiple bullets if spreadshot
            for (int i = 0; i < CurrentWeapon.bulletsPerShot; i++)
            {
                // Calculate spread
                float spread = 0f;
                if (CurrentWeapon.bulletsPerShot > 1)
                {
                    spread = Random.Range(-CurrentWeapon.spreadAngle, CurrentWeapon.spreadAngle);
                }

                Quaternion rotation = firePoint.rotation * Quaternion.Euler(0f, spread, 0f);

                // Spawn bullet from pool
                GameObject bullet = ObjectPool.Instance.SpawnFromPool("Bullet", firePoint.position, rotation);

                if (bullet == null)
                {
                    Debug.LogError("WeaponSystem: Failed to spawn bullet from pool!");
                    continue;
                }

                Debug.Log("WeaponSystem: Spawned bullet at " + firePoint.position);

                // Configure bullet
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.damage = CurrentWeapon.damage;
                    bulletScript.speed = CurrentWeapon.bulletSpeed;
                    bulletScript.lifetime = CurrentWeapon.bulletLifetime;
                    bulletScript.isPlayerBullet = true; // Player fired this
                    bulletScript.SetShooter(gameObject); // Ignore collision with player
                    Debug.Log("WeaponSystem: Configured bullet - speed: " + bulletScript.speed + ", lifetime: " + bulletScript.lifetime);
                }
                else
                {
                    Debug.LogError("WeaponSystem: Bullet has no Bullet script! Prefab may have missing script.");
                }

                // Set bullet color and scale
                Renderer renderer = bullet.GetComponent<Renderer>();
                if (renderer != null && renderer.material != null)
                {
                    renderer.material.color = CurrentWeapon.bulletColor;
                }

                bullet.transform.localScale = Vector3.one * CurrentWeapon.bulletScale;
            }
        }

        public void SwitchWeapon(int index)
        {
            if (index >= 0 && index < availableWeapons.Count)
            {
                currentWeaponIndex = index;
                Debug.Log("Switched to: " + CurrentWeapon.weaponName);
            }
        }

        public void AddWeapon(WeaponData weapon)
        {
            if (weapon == null) return;

            if (!availableWeapons.Contains(weapon))
            {
                availableWeapons.Add(weapon);
                ammoInventory[weapon] = weapon.maxAmmo;
                currentWeaponIndex = availableWeapons.Count - 1;
                Debug.Log("Picked up: " + weapon.weaponName);
            }
            else
            {
                // Refill ammo if weapon already owned
                ammoInventory[weapon] = weapon.maxAmmo;
                Debug.Log("Refilled ammo for: " + weapon.weaponName);
            }
        }

        public void AddAmmo(WeaponData weapon, int amount)
        {
            if (weapon == null || !ammoInventory.ContainsKey(weapon)) return;
            ammoInventory[weapon] = Mathf.Min(ammoInventory[weapon] + amount, weapon.maxAmmo);
        }
    }
}
