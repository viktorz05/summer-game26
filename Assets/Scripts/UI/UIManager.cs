using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private static UIManager _instance;
    public Slider healthBar;
    public Text healthText;
    public Slider ammoBar;
    public Text ammoText;

    public static UIManager Instance
    {
        get 
        {
            if (_instance == null) 
            {
                Debug.Log("UI manager not found");
            }
            return _instance;
        }
    }
    private void Awake()
    {
       _instance = this; 
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = healthBar.value.ToString();
    }

    public void setAmmo(int ammo) 
    {
        ammoText.text = ammo.ToString();
    }

    public void setHealth(int health)
    {
        healthBar.value = health;
    }
}
