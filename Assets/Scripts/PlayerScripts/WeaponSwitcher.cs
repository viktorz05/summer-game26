using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private InputAction scrollAction;
    private Inventory inventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inventory = GetComponent<Inventory>();
        if (inventory == null) Debug.LogError("Inventory not assigned!");
        scrollAction =
             inputActions
             .FindActionMap("Player")
             .FindAction("ScrollWeapons");
        if (scrollAction == null)
        {
            Debug.Log($"Input action {scrollAction} not set in the editor!");
        }
    }

    private void OnEnable()
    {
        scrollAction.Enable();
        scrollAction.performed += OnScroll;
    }

    private void OnDisable()
    {
        scrollAction.performed -= OnScroll;
        scrollAction.Disable();
    }
    // Update is called once per frame
    void OnScroll(InputAction.CallbackContext ctx)
    {
        float scrollValue = ctx.ReadValue<Vector2>().y;
        int direction = scrollValue > 0f ? 1 : -1;
        
        if (direction > 0)
        {
            inventory.equipNext();
            Debug.Log("Equipped weapon 0");
        }
        else
        {
            inventory.equipPrevious();
            Debug.Log("Equipped weapon 1");
        }
    }
}
