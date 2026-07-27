using UnityEngine;

public class SpeedUpgrade : MonoBehaviour, IConsumable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject upgrade;
    private bool playerNearby = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNearby == true && Input.GetKeyDown(KeyCode.E))
        {
            Consume();
            UIManager.Instance.hideInteraction();
        }

    }

    public void Consume()
    {
        Destroy(upgrade);
    }
}
