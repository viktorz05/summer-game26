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
    private bool isReloading;
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

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            tryShoot();
        }
    }

    void tryShoot()
    {
        if (currentAmmo <= 0 || isReloading || playerMovement.isInteracting)
        {
            Debug.Log("Can't shoot rn");
            return;
        }
        
        if (Time.time >= nextShotTime)
        {
            nextShotTime = Time.time + (1 / _weaponData.fireRate);
            _shootModule.Shoot();
        }

    }

    

    private void tryReload()
    {
        if (!isReloading && currentAmmo < _weaponData.magazineSize)
        {
            StartCoroutine(Reload());
        }
    }
    
    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(_weaponData.reloadSpeed);
        currentAmmo = _weaponData.magazineSize;
        isReloading = false;
    }
}