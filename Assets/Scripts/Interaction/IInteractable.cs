using UnityEngine;

public interface IInteractable
{
   public string interactMessage { get; }
   public bool CanInteract();
   public void OnInteract(); 
}
