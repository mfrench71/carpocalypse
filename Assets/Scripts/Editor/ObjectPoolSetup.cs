using UnityEngine;
using UnityEditor;

namespace Carpocalypse.Editor
{
    public class ObjectPoolSetup : EditorWindow
    {
        [MenuItem("Carpocalypse/Setup Object Pool")]
        public static void SetupObjectPool()
        {
            // Find ObjectPool in scene
            ObjectPool pool = Object.FindFirstObjectByType<ObjectPool>();

            if (pool == null)
            {
                // Try to find GameManager and add ObjectPool to it
                GameManager gm = Object.FindFirstObjectByType<GameManager>();
                if (gm != null)
                {
                    pool = gm.gameObject.GetComponent<ObjectPool>();
                    if (pool == null)
                    {
                        pool = gm.gameObject.AddComponent<ObjectPool>();
                        Debug.Log("Added ObjectPool to GameManager");
                    }
                }
                else
                {
                    Debug.LogError("No GameManager found in scene! Create one first.");
                    return;
                }
            }

            // Find Bullet prefab
            string[] bulletGuids = AssetDatabase.FindAssets("t:Prefab Bullet", new[] { "Assets/Prefabs" });
            GameObject bulletPrefab = null;

            foreach (string guid in bulletGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab != null && prefab.name == "Bullet")
                {
                    bulletPrefab = prefab;
                    break;
                }
            }

            if (bulletPrefab == null)
            {
                Debug.LogError("Bullet prefab not found in Assets/Prefabs!");
                return;
            }

            // Check if Bullet pool already exists
            bool hasBulletPool = false;
            if (pool.pools == null)
            {
                pool.pools = new System.Collections.Generic.List<ObjectPool.Pool>();
            }

            foreach (var p in pool.pools)
            {
                if (p.tag == "Bullet")
                {
                    hasBulletPool = true;
                    p.prefab = bulletPrefab;
                    p.size = 50;
                    Debug.Log("Updated existing Bullet pool");
                    break;
                }
            }

            if (!hasBulletPool)
            {
                ObjectPool.Pool bulletPool = new ObjectPool.Pool
                {
                    tag = "Bullet",
                    prefab = bulletPrefab,
                    size = 50
                };
                pool.pools.Add(bulletPool);
                Debug.Log("Added Bullet pool (size: 50)");
            }

            EditorUtility.SetDirty(pool);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(pool.gameObject.scene);

            Selection.activeGameObject = pool.gameObject;
            Debug.Log("Object Pool setup complete!");
        }
    }
}
