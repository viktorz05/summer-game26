using UnityEngine;

public class WeaponPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject weaponPrefab;
    private Inventory pInventory;
    private Camera pCam;
    public string interactMessage => $"Pickup {weaponPrefab.name} [E]";
    public bool CanInteract()
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract()
    {
        pInventory = FindAnyObjectByType<Inventory>();
        if (pInventory == null)
        {
            Debug.Log("Player inventory not found!");
        }
        pInventory.addWeapon(weaponPrefab);
        Destroy(weaponPrefab);
    }
}
