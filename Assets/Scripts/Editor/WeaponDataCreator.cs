using UnityEngine;
using UnityEditor;
using System.IO;

namespace Carpocalypse.Editor
{
    public class WeaponDataCreator : EditorWindow
    {
        [MenuItem("Carpocalypse/Create Default Weapons")]
        public static void CreateDefaultWeapons()
        {
            string folderPath = "Assets/Data/Weapons";

            // Create folder if it doesn't exist
            if (!AssetDatabase.IsValidFolder("Assets/Data"))
            {
                AssetDatabase.CreateFolder("Assets", "Data");
            }
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                AssetDatabase.CreateFolder("Assets/Data", "Weapons");
            }

            // Create Machine Gun
            WeaponData machineGun = ScriptableObject.CreateInstance<WeaponData>();
            machineGun.weaponName = "Machine Gun";
            machineGun.fireRate = 0.15f;
            machineGun.damage = 1;
            machineGun.bulletSpeed = 25f;
            machineGun.bulletLifetime = 2f;
            machineGun.hasInfiniteAmmo = true;
            machineGun.maxAmmo = 100;
            machineGun.bulletsPerShot = 1;
            machineGun.spreadAngle = 0f;
            machineGun.bulletColor = Color.yellow;
            machineGun.bulletScale = 0.3f;
            AssetDatabase.CreateAsset(machineGun, folderPath + "/MachineGun.asset");

            // Create Cannon
            WeaponData cannon = ScriptableObject.CreateInstance<WeaponData>();
            cannon.weaponName = "Cannon";
            cannon.fireRate = 0.8f;
            cannon.damage = 10;
            cannon.bulletSpeed = 15f;
            cannon.bulletLifetime = 3f;
            cannon.hasInfiniteAmmo = false;
            cannon.maxAmmo = 20;
            cannon.bulletsPerShot = 1;
            cannon.spreadAngle = 0f;
            cannon.bulletColor = Color.red;
            cannon.bulletScale = 0.8f;
            AssetDatabase.CreateAsset(cannon, folderPath + "/Cannon.asset");

            // Create Shotgun
            WeaponData shotgun = ScriptableObject.CreateInstance<WeaponData>();
            shotgun.weaponName = "Shotgun";
            shotgun.fireRate = 0.6f;
            shotgun.damage = 2;
            shotgun.bulletSpeed = 20f;
            shotgun.bulletLifetime = 1.5f;
            shotgun.hasInfiniteAmmo = false;
            shotgun.maxAmmo = 30;
            shotgun.bulletsPerShot = 5;
            shotgun.spreadAngle = 15f;
            shotgun.bulletColor = new Color(1f, 0.5f, 0f, 1f); // Orange
            shotgun.bulletScale = 0.25f;
            AssetDatabase.CreateAsset(shotgun, folderPath + "/Shotgun.asset");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("Created 3 weapon assets in " + folderPath);

            // Select the folder in Project window
            Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(folderPath);
        }
    }
}
