using UnityEngine;
using TMPro;

public class DoorScript_test : MonoBehaviour, IInteractable
{
    public GameObject door;
    string interactMessage => doorMessage;

    string IInteractable.interactMessage => interactMessage;

    [SerializeField]
    public string doorMessage;

    private void Start()
    {
        if (door == null)
        {
            Debug.LogError("Door GameObject is not assigned.");
        }
    }
    public void OnInteract()
    {

        OpenDoor();
    }

    public void OpenDoor()
    {
        if (door != null)
        {
            door.SetActive(false);
        }
    }

    public bool CanInteract()
    {
        throw new System.NotImplementedException();
    }

}
