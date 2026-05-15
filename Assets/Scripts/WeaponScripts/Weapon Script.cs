using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponScript : MonoBehaviour
{
    public GameObject weaponModel;
    public Camera playerCamera;
    public PlayerMovement playerMovement;
    public WeaponData weaponData;
    private ShootModule _shootModule;
    private AmmoModule _ammoModule;
    private ReloadModule _reloadModule;

    private uint currentAmmo = 100u;
    private float nextShotTime = 0f;
    private bool isAiming;

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        if (weaponModel == null)
            Debug.LogError("Weapon model not assigned in the inspector.");
        currentAmmo = weaponData.magazineSize;

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
        if (currentAmmo <= 0 || _reloadModule.isReloading || playerMovement.isInteracting)
        {
            Debug.Log("Can't shoot rn");
            return;
        }

        if (Time.time >= nextShotTime)
        {
            nextShotTime = Time.time + (1 / weaponData.fireRate);
            _shootModule.Shoot();
        }

    }

    private void tryReload()
    {
        if (!_reloadModule.isReloading && currentAmmo < weaponData.magazineSize)
        {
            _reloadModule.Reload();
        }
    }
}