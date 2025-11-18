using UnityEngine;

namespace Carpocalypse
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "Carpocalypse/Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [Header("Weapon Info")]
        public string weaponName;
        public Sprite weaponIcon;

        [Header("Shooting")]
        public float fireRate = 0.3f;
        public int damage = 1;
        public float bulletSpeed = 20f;
        public float bulletLifetime = 3f;

        [Header("Ammo")]
        public bool hasInfiniteAmmo = true;
        public int maxAmmo = 100;

        [Header("Spread")]
        public int bulletsPerShot = 1;
        public float spreadAngle = 0f;

        [Header("Visual")]
        public Color bulletColor = Color.yellow;
        public float bulletScale = 1f;
    }
}
