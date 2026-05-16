using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private GameObject weaponModel;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private WeaponData _weaponData;

    private PlayerMovement playerMovement;
    private ShootModule _shootModule;
    private AmmoModule _ammoModule;
    private ReloadModule _reloadModule;

    private float nextShotTime = 0f;
    private bool isAiming;

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        _shootModule = GetComponent<ShootModule>();
        _ammoModule = GetComponent<AmmoModule>();
        _reloadModule = GetComponent<ReloadModule>();

        if (_shootModule == null) Debug.LogError("Shoot module not assigned!");
        if (_ammoModule == null) Debug.LogError("Ammo module not assigned!");
        if (_reloadModule == null) Debug.LogError("Reload module not assigned!");
        if (_weaponData == null) Debug.LogError("Weapon data not assigned!");
        if (weaponModel == null) Debug.LogError("Weapon model not assigned in the inspector.");

        foreach (var module in GetComponents<IWeaponModule>())
        {
            module.Initialize(_weaponData);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            tryShoot();
        }
        if (Input.GetKeyDown("Reload"))
        {
            tryReload();
        }
    }

    void tryShoot()
    {
        if (_reloadModule.isReloading) return;
        if (!_ammoModule.HasAmmo)
        {
            tryReload();
            return;
        }

        if (Time.time < nextShotTime) return;
        nextShotTime = Time.time + (60f / _weaponData.fireRate);
        _ammoModule.ConsumeRound();
        _shootModule.Shoot();
        Debug.Log("Weapon fired");

    }

    private void tryReload()
    {
        if (_reloadModule.isReloading || _ammoModule.IsClipFull || !_ammoModule.HasReserve) return;
        _reloadModule.startReload();
    }
}