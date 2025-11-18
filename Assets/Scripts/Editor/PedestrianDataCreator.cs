using UnityEngine;
using UnityEditor;

namespace Carpocalypse.Editor
{
    public class PedestrianDataCreator : EditorWindow
    {
        [MenuItem("Carpocalypse/Create Default Pedestrians")]
        public static void CreateDefaultPedestrians()
        {
            string dataFolder = "Assets/Data/Pedestrians";
            string prefabFolder = "Assets/Prefabs/Pedestrians";

            // Create folders
            CreateFolderIfNeeded("Assets/Data", "Pedestrians");
            CreateFolderIfNeeded("Assets/Prefabs", "Pedestrians");

            // Create prefabs first
            GameObject civilianPrefab = CreatePedestrianPrefab("Civilian", new Color(0.2f, 0.6f, 1f), prefabFolder);
            GameObject banditPrefab = CreatePedestrianPrefab("Bandit", new Color(1f, 0.3f, 0.3f), prefabFolder);
            GameObject survivorPrefab = CreatePedestrianPrefab("Survivor", new Color(0.3f, 0.8f, 0.3f), prefabFolder);

            // Create Civilian (unarmed, flees)
            PedestrianData civilian = ScriptableObject.CreateInstance<PedestrianData>();
            civilian.pedestrianName = "Civilian";
            civilian.prefab = civilianPrefab;
            civilian.maxHealth = 10;
            civilian.walkSpeed = 2f;
            civilian.runSpeed = 6f;
            civilian.defaultBehavior = PedestrianBehavior.Flee;
            civilian.detectionRange = 15f;
            civilian.fleeDistance = 20f;
            civilian.wanderRadius = 8f;
            civilian.isArmed = false;
            civilian.scoreValue = 0;
            civilian.penaltyValue = -50; // Penalty for killing civilians
            civilian.color = new Color(0.2f, 0.6f, 1f);
            AssetDatabase.CreateAsset(civilian, dataFolder + "/Civilian.asset");

            // Create Bandit (armed, aggressive)
            PedestrianData bandit = ScriptableObject.CreateInstance<PedestrianData>();
            bandit.pedestrianName = "Bandit";
            bandit.prefab = banditPrefab;
            bandit.maxHealth = 20;
            bandit.walkSpeed = 3f;
            bandit.runSpeed = 5f;
            bandit.defaultBehavior = PedestrianBehavior.Aggressive;
            bandit.detectionRange = 12f;
            bandit.isArmed = true;
            bandit.damage = 5;
            bandit.attackRange = 10f;
            bandit.fireRate = 1.5f;
            bandit.bulletSpeed = 12f;
            bandit.scoreValue = 75;
            bandit.healthDropChance = 0.15f;
            bandit.ammoDropChance = 0.25f;
            bandit.color = new Color(1f, 0.3f, 0.3f);
            AssetDatabase.CreateAsset(bandit, dataFolder + "/Bandit.asset");

            // Create Survivor (unarmed, neutral/wanders)
            PedestrianData survivor = ScriptableObject.CreateInstance<PedestrianData>();
            survivor.pedestrianName = "Survivor";
            survivor.prefab = survivorPrefab;
            survivor.maxHealth = 15;
            survivor.walkSpeed = 2.5f;
            survivor.runSpeed = 5f;
            survivor.defaultBehavior = PedestrianBehavior.Wander;
            survivor.detectionRange = 8f;
            survivor.wanderRadius = 12f;
            survivor.isArmed = false;
            survivor.scoreValue = 25;
            survivor.color = new Color(0.3f, 0.8f, 0.3f);
            AssetDatabase.CreateAsset(survivor, dataFolder + "/Survivor.asset");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("Created 3 pedestrian types in " + dataFolder);
            Debug.Log("Created 3 pedestrian prefabs in " + prefabFolder);

            Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(dataFolder);
        }

        static void CreateFolderIfNeeded(string parent, string folderName)
        {
            string path = parent + "/" + folderName;
            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder(parent, folderName);
            }
        }

        static GameObject CreatePedestrianPrefab(string name, Color color, string folder)
        {
            // Create pedestrian game object
            GameObject pedestrian = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            pedestrian.name = name;
            pedestrian.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            pedestrian.tag = "Untagged";

            // Setup material
            Renderer renderer = pedestrian.GetComponent<Renderer>();
            Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            mat.color = color;
            renderer.material = mat;

            // Add Rigidbody
            Rigidbody rb = pedestrian.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            rb.mass = 1f;

            // Add Health component
            Health health = pedestrian.AddComponent<Health>();

            // Add PedestrianAI component
            PedestrianAI ai = pedestrian.AddComponent<PedestrianAI>();

            // Save as prefab
            string prefabPath = folder + "/" + name + ".prefab";
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(pedestrian, prefabPath);

            // Destroy scene object
            Object.DestroyImmediate(pedestrian);

            return prefab;
        }
    }
}
