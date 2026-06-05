using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    public int currentIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == 0);
        }
    }
    void addWeapon(GameObject weapon)
    {
        currentIndex = currentIndex++ % weapons.Length;
        if (weapons[currentIndex] != null)
        {
            removeWeapon(currentIndex);
        }
        weapons[currentIndex] = weapon;
    }

    void removeWeapon(int index)
    {
        weapons[index] = null;
    }

    private void equipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;
        if (weapons[index] == null) return;

        weapons[currentIndex].SetActive(false);
        currentIndex = index;
        weapons[currentIndex].SetActive(true);
    }

    public void equipNext() => equipWeapon((currentIndex + 1) % weapons.Length);
    public void equipPrevious() => equipWeapon((currentIndex - 1 + weapons.Length) % weapons.Length);

}
