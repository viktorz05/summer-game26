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
    private KeyCode interactKey = KeyCode.E;
    private IInteractable currentInteractable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentInteractable();
        UpdateInteractionText();
        CheckInteractionInput();
    }

    private void UpdateCurrentInteractable()
    {
        Ray r = new Ray(playerCam.transform.position, playerCam.transform.forward);
        Physics.Raycast(r, out var hit, interactRange);
        currentInteractable = hit.collider?.GetComponent<IInteractable>();
    }

    private void UpdateInteractionText()
    {
        if (currentInteractable == null)
        {
            interactionText.text = string.Empty;
            return;
        }
        interactionText.text = currentInteractable.interactMessage;
    }

    private void CheckInteractionInput()
    {
        if (Input.GetKeyDown(interactKey) && currentInteractable != null) 
        {
            currentInteractable.OnInteract();
        }
    }
}
