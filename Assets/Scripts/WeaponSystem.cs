using UnityEngine;
using System.Collections.Generic;

public class WeaponSystem : MonoBehaviour
{
    [Header("Weapon Settings")]
    public List<WeaponData> availableWeapons = new List<WeaponData>();
    public WeaponData currentWeapon;
    public Transform firePoint;

    private float nextFireTime = 0f;

    void Start()
    {
        // Create fire point if it doesn't exist
        if (firePoint == null)
        {
            GameObject fp = new GameObject("FirePoint");
            fp.transform.parent = transform;
            fp.transform.localPosition = new Vector3(0f, 0f, 1f);
            firePoint = fp.transform;
        }

        // Set default weapon
        if (currentWeapon == null && availableWeapons.Count > 0)
        {
            currentWeapon = availableWeapons[0];
        }
    }

    void Update()
    {
        // Shooting
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime && currentWeapon != null)
        {
            if (currentWeapon.hasInfiniteAmmo || currentWeapon.currentAmmo > 0)
            {
                Shoot();
                nextFireTime = Time.time + currentWeapon.fireRate;

                if (!currentWeapon.hasInfiniteAmmo)
                {
                    currentWeapon.currentAmmo--;
                }
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

    void Shoot()
    {
        if (currentWeapon == null || firePoint == null) return;

        // Shoot multiple bullets if spreadshot
        for (int i = 0; i < currentWeapon.bulletsPerShot; i++)
        {
            // Calculate spread
            float spread = 0f;
            if (currentWeapon.bulletsPerShot > 1)
            {
                spread = Random.Range(-currentWeapon.spreadAngle, currentWeapon.spreadAngle);
            }

            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0f, spread, 0f);

            // Spawn bullet from pool
            GameObject bullet = ObjectPool.Instance.SpawnFromPool("Bullet", firePoint.position, rotation);

            if (bullet != null)
            {
                // Configure bullet
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.damage = currentWeapon.damage;
                    bulletScript.speed = currentWeapon.bulletSpeed;
                    bulletScript.lifetime = currentWeapon.bulletLifetime;
                }

                // Set bullet color and scale
                Renderer renderer = bullet.GetComponent<Renderer>();
                if (renderer != null && renderer.material != null)
                {
                    renderer.material.color = currentWeapon.bulletColor;
                }

                bullet.transform.localScale = Vector3.one * currentWeapon.bulletScale;
            }
        }
    }

    public void SwitchWeapon(int index)
    {
        if (index >= 0 && index < availableWeapons.Count)
        {
            currentWeapon = availableWeapons[index];
            Debug.Log("Switched to: " + currentWeapon.weaponName);
        }
    }

    public void AddWeapon(WeaponData weapon)
    {
        if (!availableWeapons.Contains(weapon))
        {
            availableWeapons.Add(weapon);
            currentWeapon = weapon;
            Debug.Log("Picked up: " + weapon.weaponName);
        }
    }
}
