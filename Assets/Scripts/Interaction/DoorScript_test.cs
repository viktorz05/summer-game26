using UnityEngine;
using TMPro;

public class DoorScript_test : MonoBehaviour, IInteractable
{
    public GameObject door;
    [SerializeField] private UIManager uiManager;
    private bool playerNearby;

    string IInteractable.interactMessage => throw new System.NotImplementedException();

    private void Start()
    {
        if (door == null)
        {
            Debug.LogError("Door GameObject is not assigned.");
        }
    }
    //private void Update()
    //{
    //    if (playerNearby == true && Input.GetKeyDown(KeyCode.E))
    //    {
    //        OpenDoor();
    //        UIManager.Instance.hideInteraction();
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        playerNearby = true;
    //        UIManager.Instance.showInteraction("Press E to Open Door");
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        playerNearby = false;
    //        UIManager.Instance.hideInteraction();
    //    }
    //}
    public void OnInteract()
    {

        UIManager.Instance.showInteraction("Press E to Open Door");
        OpenDoor();
        UIManager.Instance.hideInteraction();
    }

    public void OpenDoor()
    {
        if (door != null)
        {
            door.SetActive(false);
        }
    }

    bool IInteractable.CanInteract()
    {
        throw new System.NotImplementedException();
    }

    void IInteractable.OnInteract()
    {
        throw new System.NotImplementedException();
    }
}
