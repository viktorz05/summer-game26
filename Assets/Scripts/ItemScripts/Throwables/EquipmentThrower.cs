using System;
using Unity.Mathematics;
using UnityEngine;

public class EquipmentThrower : MonoBehaviour, IThrowable
{
    [Header("Equipment prefab")] 
    [SerializeField] private GameObject equipmentPrefab;

    [Header("Player camera")] 
    [SerializeField] private Camera playerCam;

    [Header("Throw config")] 
    [SerializeField] private Transform throwPosition;
    [SerializeField] private Vector3 throwDirection;

    private Inventory inventory;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.G;
    [SerializeField] public float throwForce;
    private bool readyToThrow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerCam = GetComponent<Camera>();
        inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.Log("Couldnt find inventory!");
        }
        readyToThrow = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToThrow && Input.GetKeyDown(throwKey))
        {
            Throw(); 
        }
    }

    public void Throw()
    {
        readyToThrow = false;
        GameObject projectilePrefab = inventory.getEquipment();
        if (projectilePrefab == null)
        {
            Debug.Log("Player has no equipment!");
            return;
        }
        Vector3 spawnPosition = throwPosition.position + playerCam.transform.forward;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, playerCam.transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 finalThrowDirection = (playerCam.transform.forward + throwDirection).normalized;
        rb.AddForce(finalThrowDirection *  throwForce, ForceMode.Impulse);
        readyToThrow = true;
    }
}
