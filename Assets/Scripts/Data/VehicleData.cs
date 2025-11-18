using UnityEngine;

namespace Carpocalypse
{
    [CreateAssetMenu(fileName = "NewVehicle", menuName = "Carpocalypse/Vehicle Data")]
    public class VehicleData : ScriptableObject
    {
        [Header("Info")]
        public string vehicleName;
        public Sprite vehicleIcon;
        public GameObject prefab;

        [Header("Movement")]
        public float moveSpeed = 10f;
        public float rotationSpeed = 200f;
        public float acceleration = 5f;

        [Header("Health")]
        public int maxHealth = 100;
        public float damageResistance = 0f; // 0-1, percentage reduction

        [Header("Combat")]
        public int ramDamage = 10;
        public float ramCooldown = 1f;

        [Header("Special")]
        public bool hasBoost = false;
        public float boostMultiplier = 2f;
        public float boostDuration = 2f;
        public float boostCooldown = 5f;

        [Header("Unlock")]
        public bool isUnlocked = true;
        public int unlockCost = 0;
    }
}
