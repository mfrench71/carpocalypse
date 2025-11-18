using UnityEngine;
using UnityEditor;

namespace Carpocalypse.Editor
{
    public class BulletPrefabFix : EditorWindow
    {
        [MenuItem("Carpocalypse/Fix Bullet Prefab")]
        public static void FixBulletPrefab()
        {
            string prefabPath = "Assets/Prefabs/Bullet.prefab";

            // Delete old prefab
            if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null)
            {
                AssetDatabase.DeleteAsset(prefabPath);
                Debug.Log("Deleted old Bullet prefab");
            }

            // Create new bullet
            GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bullet.name = "Bullet";
            bullet.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            // Setup Rigidbody
            Rigidbody rb = bullet.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

            // Add Bullet script (from Carpocalypse namespace)
            Bullet bulletScript = bullet.AddComponent<Bullet>();
            bulletScript.speed = 20f;
            bulletScript.lifetime = 3f;
            bulletScript.damage = 1;

            // Create material
            Renderer renderer = bullet.GetComponent<Renderer>();
            Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            mat.color = Color.yellow;
            renderer.material = mat;

            // Save prefab
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(bullet, prefabPath);

            // Destroy temp object
            Object.DestroyImmediate(bullet);

            Debug.Log("Created new Bullet prefab with correct script reference!");

            // Now update ObjectPool to use new prefab
            ObjectPool pool = Object.FindFirstObjectByType<ObjectPool>();
            if (pool != null && pool.pools != null)
            {
                foreach (var p in pool.pools)
                {
                    if (p.tag == "Bullet")
                    {
                        p.prefab = prefab;
                        Debug.Log("Updated ObjectPool with new Bullet prefab");
                        EditorUtility.SetDirty(pool);
                        break;
                    }
                }
            }

            UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();

            Selection.activeObject = prefab;
        }

        [MenuItem("Carpocalypse/Fix Enemy Prefab")]
        public static void FixEnemyPrefab()
        {
            string prefabPath = "Assets/Prefabs/Enemy.prefab";

            // Delete old prefab
            if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath) != null)
            {
                AssetDatabase.DeleteAsset(prefabPath);
                Debug.Log("Deleted old Enemy prefab");
            }

            // Create new enemy
            GameObject enemy = GameObject.CreatePrimitive(PrimitiveType.Cube);
            enemy.name = "Enemy";
            enemy.transform.localScale = new Vector3(1f, 0.5f, 1.5f);
            enemy.tag = "Untagged";

            // Setup Rigidbody
            Rigidbody rb = enemy.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            rb.mass = 10f;

            // Add Health script
            Health health = enemy.AddComponent<Health>();
            // Use reflection or SerializedObject to set private fields
            var so = new SerializedObject(health);
            so.FindProperty("_maxHealth").intValue = 30;
            so.FindProperty("_currentHealth").intValue = 30;
            so.FindProperty("isPlayer").boolValue = false;
            so.FindProperty("scoreValue").intValue = 100;
            so.ApplyModifiedProperties();

            // Add EnemyAI script
            EnemyAI ai = enemy.AddComponent<EnemyAI>();
            ai.moveSpeed = 5f;
            ai.rotationSpeed = 100f;
            ai.stoppingDistance = 2f;
            ai.damageAmount = 10;
            ai.damageRate = 1f;

            // Create material
            Renderer renderer = enemy.GetComponent<Renderer>();
            Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            mat.color = new Color(1f, 0.27f, 0f, 1f); // Orange
            renderer.material = mat;

            // Save prefab
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(enemy, prefabPath);

            // Destroy temp object
            Object.DestroyImmediate(enemy);

            Debug.Log("Created new Enemy prefab with correct script references!");

            Selection.activeObject = prefab;
        }
    }
}
