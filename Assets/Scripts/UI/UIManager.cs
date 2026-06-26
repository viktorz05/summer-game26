using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Health")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Ammo")]
    [SerializeField] private Slider ammoBar;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI reloadText; // temporary

    [Header("Interaction")]
    [SerializeField] private TextMeshProUGUI interactionText;


    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null  && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //healthText.text = healthBar.value.ToString();
    }

    public void setAmmo(int ammo, int max) 
    {
        ammoText.text = $"{ammo} / {max}";
    }

    public void setHealth(int health)
    {
        healthBar.value = health;
    }
    public void showInteraction(string text)
    {
        interactionText.text = text;
        interactionText.gameObject.SetActive(true);
    }

    public void hideInteraction()
    {
        interactionText.gameObject.SetActive(false);
    }

    public void showReload(string text)
    {
        reloadText.text = text;
        reloadText.gameObject.SetActive(true);
    }

    public void hideReload()
    {
        reloadText.gameObject.SetActive(false);
    }
}
