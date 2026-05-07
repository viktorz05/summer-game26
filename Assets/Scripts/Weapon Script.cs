using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponScript : MonoBehaviour
{
    public GameObject weaponModel;
    public Camera playerCamera;
    public PlayerMovement playerMovement;
    public WeaponData weaponData;

    private uint currentAmmo = 0u;
    private float nextShotTime = 0f;
    private bool isReloading = false;
    private bool isAiming = false;

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
            Shoot();
        }
    }

    void tryShoot()
    {
        if (currentAmmo > 0 && !isReloading && !playerMovement.isInteracting)
        {
            return;
        }
        
        if (Time.time >= nextShotTime)
        {
            nextShotTime = Time.time + (1 / weaponData.shootingInterval);
            Shoot();
        }

    }
    void Shoot()
    {
        currentAmmo--;
        RaycastHit hit;
        Ray shootRay = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(shootRay, out hit))
        {
            Transform objectHit = hit.transform;
            Debug.Log("Hit object: " + objectHit.name);

            // Try to call the Target component first (preferred)
            var targetComp = objectHit.GetComponent<Target>();
            if (targetComp != null)
            {
                targetComp.OnHit();
            }
            else if (objectHit.CompareTag("Target") || objectHit.name == "Target")
            {
    
                // No Target component — try manager singleton, else fallback to deactivate only.
                GameObject targetGo = objectHit.gameObject;
                if (ShootingRange.Instance != null)
                {
                    ShootingRange.Instance.HandleTargetHit(targetGo);
                }
                else
                {
                    Debug.LogWarning("No ShootingRange available and no Target component. Deactivating target without respawn.");
                    targetGo.SetActive(false);
                }
            }
        }

        Debug.Log("Weapon fired!");
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