using UnityEngine;
using UnityEditor;

namespace Carpocalypse.Editor
{
    public class ArenaSetup : EditorWindow
    {
        [MenuItem("Carpocalypse/Create Arena Boundary")]
        public static void CreateArenaBoundary()
        {
            // Check if arena already exists
            ArenaBoundary existing = Object.FindFirstObjectByType<ArenaBoundary>();
            if (existing != null)
            {
                Debug.LogWarning("Arena boundary already exists in scene!");
                Selection.activeGameObject = existing.gameObject;
                return;
            }

            // Create arena object
            GameObject arena = new GameObject("Arena");
            arena.transform.position = Vector3.zero;

            ArenaBoundary boundary = arena.AddComponent<ArenaBoundary>();
            boundary.arenaWidth = 100f;
            boundary.arenaLength = 100f;
            boundary.wallHeight = 5f;
            boundary.wallThickness = 2f;
            boundary.showWalls = true;
            boundary.wallColor = new Color(0.4f, 0.4f, 0.4f, 1f);

            // Create the walls
            boundary.CreateBoundaries();

            // Select the new arena
            Selection.activeGameObject = arena;

            Debug.Log("Created arena boundary (100x100). Adjust size in Inspector.");

            // Mark scene dirty
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(arena.scene);
        }

        [MenuItem("Carpocalypse/Resize Ground to Arena")]
        public static void ResizeGroundToArena()
        {
            // Find arena
            ArenaBoundary arena = Object.FindFirstObjectByType<ArenaBoundary>();
            if (arena == null)
            {
                Debug.LogError("No ArenaBoundary found! Create one first.");
                return;
            }

            // Find ground (assuming it's named "Ground" or has a Plane mesh)
            GameObject ground = GameObject.Find("Ground");
            if (ground == null)
            {
                // Try to find any plane
                MeshFilter[] meshFilters = Object.FindObjectsByType<MeshFilter>(FindObjectsSortMode.None);
                foreach (var mf in meshFilters)
                {
                    if (mf.sharedMesh != null && mf.sharedMesh.name == "Plane")
                    {
                        ground = mf.gameObject;
                        break;
                    }
                }
            }

            if (ground == null)
            {
                Debug.LogError("No ground object found!");
                return;
            }

            // Unity plane is 10x10 by default, scale to match arena
            float scaleX = arena.arenaWidth / 10f;
            float scaleZ = arena.arenaLength / 10f;

            ground.transform.position = arena.transform.position;
            ground.transform.localScale = new Vector3(scaleX, 1f, scaleZ);

            Debug.Log("Resized ground to match arena: " + arena.arenaWidth + "x" + arena.arenaLength);

            EditorUtility.SetDirty(ground);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(ground.scene);
        }
    }
}
