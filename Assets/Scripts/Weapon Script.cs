using System.Collections;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject weaponModel;
    public Camera playerCamera;
    public PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        if (weaponModel == null)
            Debug.LogError("Weapon model not assigned in the inspector.");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
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
}