using System;
using UnityEngine;

namespace Carpocalypse
{
    /// <summary>
    /// Central event hub for game-wide events.
    /// Use this to decouple systems - publishers don't need to know about subscribers.
    /// </summary>
    public static class GameEvents
    {
        // Game State Events
        public static event Action OnGameStart;
        public static event Action OnGameOver;
        public static event Action OnGamePause;
        public static event Action OnGameResume;

        // Score Events
        public static event Action<int> OnScoreChanged;
        public static event Action<int> OnScoreAdded;

        // Player Events
        public static event Action<int, int> OnPlayerHealthChanged; // current, max
        public static event Action OnPlayerDeath;
        public static event Action<Vector3> OnPlayerSpawned;

        // Enemy Events
        public static event Action<GameObject> OnEnemySpawned;
        public static event Action<GameObject, int> OnEnemyKilled; // enemy, scoreValue
        public static event Action<int> OnWaveStarted;
        public static event Action<int> OnWaveCompleted;

        // Weapon Events
        public static event Action<WeaponData> OnWeaponSwitched;
        public static event Action<WeaponData> OnWeaponPickedUp;
        public static event Action<int> OnAmmoChanged; // current ammo (-1 for infinite)

        // Pickup Events
        public static event Action<GameObject> OnPickupCollected;

        // Pedestrian Events
        public static event Action<GameObject> OnPedestrianSpawned;
        public static event Action<GameObject, int> OnPedestrianKilled; // pedestrian, scoreValue
        public static event Action<int> OnPedestrianCountChanged; // current count

        // Trigger methods
        public static void TriggerGameStart() => OnGameStart?.Invoke();
        public static void TriggerGameOver() => OnGameOver?.Invoke();
        public static void TriggerGamePause() => OnGamePause?.Invoke();
        public static void TriggerGameResume() => OnGameResume?.Invoke();

        public static void TriggerScoreChanged(int score) => OnScoreChanged?.Invoke(score);
        public static void TriggerScoreAdded(int points) => OnScoreAdded?.Invoke(points);

        public static void TriggerPlayerHealthChanged(int current, int max) => OnPlayerHealthChanged?.Invoke(current, max);
        public static void TriggerPlayerDeath() => OnPlayerDeath?.Invoke();
        public static void TriggerPlayerSpawned(Vector3 position) => OnPlayerSpawned?.Invoke(position);

        public static void TriggerEnemySpawned(GameObject enemy) => OnEnemySpawned?.Invoke(enemy);
        public static void TriggerEnemyKilled(GameObject enemy, int scoreValue) => OnEnemyKilled?.Invoke(enemy, scoreValue);
        public static void TriggerWaveStarted(int wave) => OnWaveStarted?.Invoke(wave);
        public static void TriggerWaveCompleted(int wave) => OnWaveCompleted?.Invoke(wave);

        public static void TriggerWeaponSwitched(WeaponData weapon) => OnWeaponSwitched?.Invoke(weapon);
        public static void TriggerWeaponPickedUp(WeaponData weapon) => OnWeaponPickedUp?.Invoke(weapon);
        public static void TriggerAmmoChanged(int ammo) => OnAmmoChanged?.Invoke(ammo);

        public static void TriggerPickupCollected(GameObject pickup) => OnPickupCollected?.Invoke(pickup);

        public static void TriggerPedestrianSpawned(GameObject pedestrian) => OnPedestrianSpawned?.Invoke(pedestrian);
        public static void TriggerPedestrianKilled(GameObject pedestrian, int scoreValue) => OnPedestrianKilled?.Invoke(pedestrian, scoreValue);
        public static void TriggerPedestrianCountChanged(int count) => OnPedestrianCountChanged?.Invoke(count);

        /// <summary>
        /// Clear all event subscriptions. Call when reloading scenes.
        /// </summary>
        public static void ClearAll()
        {
            OnGameStart = null;
            OnGameOver = null;
            OnGamePause = null;
            OnGameResume = null;
            OnScoreChanged = null;
            OnScoreAdded = null;
            OnPlayerHealthChanged = null;
            OnPlayerDeath = null;
            OnPlayerSpawned = null;
            OnEnemySpawned = null;
            OnEnemyKilled = null;
            OnWaveStarted = null;
            OnWaveCompleted = null;
            OnWeaponSwitched = null;
            OnWeaponPickedUp = null;
            OnAmmoChanged = null;
            OnPickupCollected = null;
            OnPedestrianSpawned = null;
            OnPedestrianKilled = null;
            OnPedestrianCountChanged = null;
        }
    }
}
