using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon info")]
    public string weaponName;
    public bool isAutomatic;

    [Header("Ammo")]
    public uint magazineSize;
    public uint reserveSize;

    [Header("Reload")]
    public float reloadSpeed;

    [Header("Fire")]
    public uint damage;
    public float fireRate;
    public float range;
    public uint burstSize;
    public float bulletSpread;
    public float recoil;
    public LayerMask hitLayer;

    [Header("ADS")]
    public float adsSpeed;
}
