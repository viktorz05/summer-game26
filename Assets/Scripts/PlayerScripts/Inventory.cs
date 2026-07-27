using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] private GameObject[] weapons;
    public int currentWeaponIndex { get; private set; } = 0;

    [Header("Equipment")]
    [SerializeField] private GameObject[] equipment;

    [Header("Camera")]
    [SerializeField] private Camera playerCamera;
    public int currentEquipmentIndex { get; private set; } = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCamera = Camera.main;
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == 0);
        }
    }

    public GameObject getEquipment() => 
        equipment[currentEquipmentIndex] != null && equipment.Length > 0
        ? equipment[currentEquipmentIndex] : null;

    public void addWeapon(GameObject weapon)
    {
        currentWeaponIndex = currentWeaponIndex++ % weapons.Length;
        if (weapons[currentWeaponIndex] != null)
        {
            removeWeapon(currentWeaponIndex);
        }
        weapons[currentWeaponIndex] = weapon;
        equipWeapon(currentWeaponIndex);
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

}
