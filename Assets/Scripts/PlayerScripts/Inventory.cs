using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour, IInteractable
{
    [Header("Weapons")]
    [SerializeField] private GameObject[] weapons;
    public int currentWeaponIndex { get; private set; } = 0;

    [Header("Equipment")]
    [SerializeField] private GameObject[] equipment;
    public int currentEquipmentIndex { get; private set; } = 0;

    string IInteractable.interactMessage => weaponMessage;

    [SerializeField]
    public string weaponMessage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == 0);
        }
    }

    public GameObject getEquipment() => 
        equipment[currentEquipmentIndex] != null && equipment.Length > 0
        ? equipment[currentEquipmentIndex] : null;

    void addWeapon(GameObject weapon)
    {
        currentWeaponIndex = currentWeaponIndex++ % weapons.Length;
        if (weapons[currentWeaponIndex] != null)
        {
            removeWeapon(currentWeaponIndex);
        }
        weapons[currentWeaponIndex] = weapon;
    }

    void removeWeapon(int index)
    {
        weapons[index] = null;
    }

    private void equipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;
        if (weapons[index] == null) return;

        weapons[currentWeaponIndex].SetActive(false);
        currentWeaponIndex = index;
        weapons[currentWeaponIndex].SetActive(true);
    }

    public void equipNext() => equipWeapon((currentWeaponIndex + 1) % weapons.Length);
    public void equipPrevious() => equipWeapon((currentWeaponIndex - 1 + weapons.Length) % weapons.Length);

    public void OnInteract()
    {
    }

    bool IInteractable.CanInteract()
    {
        throw new System.NotImplementedException();
    }
}
