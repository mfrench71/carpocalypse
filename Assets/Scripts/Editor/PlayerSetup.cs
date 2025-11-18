using UnityEngine;
using UnityEditor;
using System.IO;

namespace Carpocalypse.Editor
{
    public class PlayerSetup : EditorWindow
    {
        [MenuItem("Carpocalypse/Setup Player With Weapons")]
        public static void SetupPlayer()
        {
            // Find player in scene
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogError("No object with 'Player' tag found in scene!");
                return;
            }

            // Add WeaponSystem if not present
            WeaponSystem weaponSystem = player.GetComponent<WeaponSystem>();
            if (weaponSystem == null)
            {
                weaponSystem = player.AddComponent<WeaponSystem>();
                Debug.Log("Added WeaponSystem to Player");
            }

            // Find and assign weapons
            string weaponsPath = "Assets/Data/Weapons";
            if (AssetDatabase.IsValidFolder(weaponsPath))
            {
                string[] weaponGuids = AssetDatabase.FindAssets("t:WeaponData", new[] { weaponsPath });

                weaponSystem.availableWeapons.Clear();

                foreach (string guid in weaponGuids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    WeaponData weapon = AssetDatabase.LoadAssetAtPath<WeaponData>(path);
                    if (weapon != null)
                    {
                        weaponSystem.availableWeapons.Add(weapon);
                        Debug.Log("Added weapon: " + weapon.weaponName);
                    }
                }

                if (weaponSystem.availableWeapons.Count == 0)
                {
                    Debug.LogWarning("No WeaponData assets found in " + weaponsPath);
                    Debug.LogWarning("Run 'Carpocalypse > Create Default Weapons' first!");
                }
                else
                {
                    Debug.Log("Player setup complete with " + weaponSystem.availableWeapons.Count + " weapons!");
                }
            }
            else
            {
                Debug.LogWarning("Weapons folder not found at " + weaponsPath);
                Debug.LogWarning("Run 'Carpocalypse > Create Default Weapons' first!");
            }

            // Mark scene dirty so changes are saved
            EditorUtility.SetDirty(player);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(player.scene);
        }
    }
}
