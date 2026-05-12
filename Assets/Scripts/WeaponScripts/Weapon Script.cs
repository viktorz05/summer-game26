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

    private uint currentAmmo = 0u;
    private float nextShotTime = 0f;
    private bool isReloading;
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
            nextShotTime = Time.time + (1 / weaponData.fireRate);
            Ray playerRay = playerCamera.ScreenPointToRay(Input.mousePosition);
            _shootModule.Shoot(playerRay);
        }

    }

    

    private void tryReload()
    {
        if (!isReloading && currentAmmo < weaponData.magazineSize)
        {
            StartCoroutine(Reload());
        }
    }
    
    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(weaponData.reloadSpeed);
        currentAmmo = weaponData.magazineSize;
        isReloading = false;
    }
}