using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon info attributes")]
    public string weaponName;
    public uint ID;

    [Header("Reload attributes")]
    public uint magazineSize;
    public float reloadSpeed;

    [Header("Fire attributes")]
    public uint damage;
    public uint fireRate;
    public float shootingInterval;
}
