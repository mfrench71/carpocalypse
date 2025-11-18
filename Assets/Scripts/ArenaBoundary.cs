using UnityEngine;

namespace Carpocalypse
{
    public class ArenaBoundary : MonoBehaviour
    {
        [Header("Arena Size")]
        public float arenaWidth = 100f;
        public float arenaLength = 100f;
        public float wallHeight = 5f;
        public float wallThickness = 2f;

        [Header("Visual")]
        public bool showWalls = true;
        public Material wallMaterial;
        public Color wallColor = new Color(0.3f, 0.3f, 0.3f, 1f);

        private GameObject[] walls = new GameObject[4];

        void Start()
        {
            CreateBoundaries();
        }

        public void CreateBoundaries()
        {
            // Clear existing walls
            foreach (var wall in walls)
            {
                if (wall != null) DestroyImmediate(wall);
            }

            // Create parent for walls
            Transform wallParent = transform;

            // North wall
            walls[0] = CreateWall("North Wall",
                new Vector3(0, wallHeight / 2f, arenaLength / 2f),
                new Vector3(arenaWidth + wallThickness * 2, wallHeight, wallThickness));

            // South wall
            walls[1] = CreateWall("South Wall",
                new Vector3(0, wallHeight / 2f, -arenaLength / 2f),
                new Vector3(arenaWidth + wallThickness * 2, wallHeight, wallThickness));

            // East wall
            walls[2] = CreateWall("East Wall",
                new Vector3(arenaWidth / 2f, wallHeight / 2f, 0),
                new Vector3(wallThickness, wallHeight, arenaLength));

            // West wall
            walls[3] = CreateWall("West Wall",
                new Vector3(-arenaWidth / 2f, wallHeight / 2f, 0),
                new Vector3(wallThickness, wallHeight, arenaLength));
        }

        GameObject CreateWall(string name, Vector3 position, Vector3 scale)
        {
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.name = name;
            wall.transform.parent = transform;
            wall.transform.localPosition = position;
            wall.transform.localScale = scale;
            wall.layer = LayerMask.NameToLayer("Default");

            // Set visual
            Renderer renderer = wall.GetComponent<Renderer>();
            if (renderer != null)
            {
                if (wallMaterial != null)
                {
                    renderer.material = wallMaterial;
                }
                else
                {
                    renderer.material.color = wallColor;
                }

                renderer.enabled = showWalls;
            }

            // Make it static for optimization
            wall.isStatic = true;

            return wall;
        }

        void OnDrawGizmosSelected()
        {
            // Draw arena bounds in editor
            Gizmos.color = Color.yellow;

            Vector3 center = transform.position;
            Vector3 size = new Vector3(arenaWidth, wallHeight, arenaLength);

            Gizmos.DrawWireCube(center + Vector3.up * wallHeight / 2f, size);
        }

        // Public method to check if position is inside arena
        public bool IsInsideArena(Vector3 position)
        {
            Vector3 localPos = position - transform.position;
            return Mathf.Abs(localPos.x) < arenaWidth / 2f &&
                   Mathf.Abs(localPos.z) < arenaLength / 2f;
        }

        // Get random position inside arena
        public Vector3 GetRandomPositionInArena()
        {
            float x = Random.Range(-arenaWidth / 2f + 5f, arenaWidth / 2f - 5f);
            float z = Random.Range(-arenaLength / 2f + 5f, arenaLength / 2f - 5f);
            return transform.position + new Vector3(x, 0, z);
        }
    }
}
