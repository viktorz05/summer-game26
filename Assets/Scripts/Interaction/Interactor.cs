using System;
using TMPro;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    public float interactRange;

    [SerializeField]
    public TextMeshProUGUI interactionText;

    private Camera playerCam;

    public event Action<RaycastHit> OnHit;
    private KeyCode interactKey = KeyCode.F;
    private IInteractable currentInteractable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInteraction();
    }

    private void CheckInteraction()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Ray r = new Ray(playerCam.transform.position, playerCam.transform.forward);
            if (Physics.Raycast(r, out var hit, interactRange))
            {
                hit.collider.GetComponent<IInteractable>()?.OnInteract();
                OnHit?.Invoke(hit);
            }
        }
    }
}
