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

        Debug.Log("WeaponScript initialized successfully");
    }

    void Update()
    {
        if (_weaponData.isAutomatic)
        {
            // Automatic: fire while holding Fire1
            if (Input.GetButton("Fire1"))
            {
                tryShoot();
            }
        }
        else
        {
            // Semi-automatic: fire on Fire1 press
            if (Input.GetButtonDown("Fire1"))
            {
                tryShoot();
            }
        }

        //if (Input.GetKeyDown("R"))
        //{
        //    tryReload();
        //}
    }

    void tryShoot()
    {
        if (_reloadModule.isReloading)
        {
            Debug.Log("Cannot shoot: weapon is reloading");
            return;
        }

        if (!_ammoModule.HasAmmo)
        {
            Debug.Log("No ammo in magazine, attempting reload");
            tryReload();
            return;
        }

        if (Time.time < nextShotTime)
        {
            Debug.Log($"Too fast: next shot available at {nextShotTime}, current time {Time.time}");
            return;
        }

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